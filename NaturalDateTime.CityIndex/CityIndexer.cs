// Geonames data can be downloaded from: http://download.geonames.org/export/dump/

using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.QueryParsers;

namespace NaturalDateTime.CityIndex
{
	public class CityIndexer
	{
		private Lucene.Net.Store.Directory IndexDirectory{
			get { return FSDirectory.Open(ApplicationSettings.CityIndexDirectory); }
		}
		
		public void AddGeonamesDataFileToCityIndex()
        {
			Console.WriteLine();
			
            var stopWatch = new Stopwatch();
            stopWatch.Start();
			
			var analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            using (var indexWriter = new IndexWriter(IndexDirectory, analyzer, true, IndexWriter.MaxFieldLength.UNLIMITED))
            {
                var adminDivisionLookup = GetAdministrativeDivisionsLookup();
                var allCountriesGeonameDataFile = Path.Combine(ApplicationSettings.GeonameDataFilesDirectory.FullName, "allCountries.txt");
                Console.WriteLine("Adding cities from allCountries.txt to the index...");
                using (var fileStreamReader = new StreamReader(File.OpenRead(allCountriesGeonameDataFile), Encoding.UTF8))
                {
                    string line;
                    int count = 1;
                    while ((line = fileStreamReader.ReadLine()) != null)
                    {
                        string[] fields = line.Split('\t');
                        if (fields.Count() < 10) continue;
                        var geonameId = int.Parse(fields[0]);
                        var name = fields[1];
                        var asciiName = fields[2];
                        var alternateNames = fields[3];
                        var latitude = decimal.Parse(fields[4]);
                        var longitude = decimal.Parse(fields[5]);
                        var featureClass = fields[6];
                        if (!featureClass.ToLower().StartsWith("p") && !featureClass.ToLower().StartsWith("a")) continue;
                        var countryCode = fields[8];
                        if (string.IsNullOrEmpty(countryCode)) continue;
                        var countryName = CountryCodeResolver.ResolveCountryCodeToCountryName(countryCode);
                        var admin1Code = fields[10];
                        var administrativeDivisionName = string.Empty;
                        var administrativeDivisionNameAcronym = string.Empty;
                        var administrativeDivisionAsciiName = string.Empty;
                        var adminCodeKey = countryCode + "." + admin1Code;
                        if (!string.IsNullOrEmpty(admin1Code) && adminDivisionLookup.ContainsKey(adminCodeKey))
                        {
                            var administrativeDivision = adminDivisionLookup[adminCodeKey];
                            administrativeDivisionName = administrativeDivision.Name;
                            administrativeDivisionAsciiName = administrativeDivision.AsciiName;
                            if (Regex.IsMatch(admin1Code, @"\d"))
                            {
                                var adminDivisionNameWords = administrativeDivisionName.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                                if (adminDivisionNameWords.Length > 1)
                                    administrativeDivisionNameAcronym = new string(adminDivisionNameWords.Select(s => s[0]).ToArray()).ToUpper();
                            }
                            else
                            {
                                administrativeDivisionNameAcronym = admin1Code;
                            }
                        }
                        long population = 0;
                        long.TryParse(fields[14], out population);
                        var timezone = fields[17];

                        var city = CreateLuceneDocument(geonameId, name, asciiName, alternateNames, latitude, longitude, countryCode, countryName, administrativeDivisionName, administrativeDivisionNameAcronym, administrativeDivisionAsciiName, timezone, population);
                        indexWriter.AddDocument(city);
                        count++;

                        if (count % 100000 == 0) Console.WriteLine("Number of cities indexed: " + count);
                    }
                }

                indexWriter.Optimize();
            }
            stopWatch.Stop();
            Console.WriteLine("Finished adding cities to the index in " + stopWatch.Elapsed.TotalSeconds + " seconds.");
            
        }
		
		public Document CreateLuceneDocument(int geonameId, string name, string asciiName, string alternateNames, decimal latitude, decimal longitude, string countryCode, string countryName, string administrativeDivisionName, string administrativeDivisionNameAcronym, string administrativeDivisionAsciiName, string timezone, long population){
			var document = new Document();
            document.Add(new Field(CityFieldNames.Id, geonameId.ToString(), Field.Store.YES, Field.Index.NO));
            document.Add(new Field(CityFieldNames.Name, name, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(CityFieldNames.AlternateNames, alternateNames, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(CityFieldNames.Latitude, latitude.ToString(), Field.Store.YES, Field.Index.NO));
            document.Add(new Field(CityFieldNames.Longitude, longitude.ToString(), Field.Store.YES, Field.Index.NO));
            document.Add(new Field(CityFieldNames.CountryCode, countryCode, Field.Store.YES, Field.Index.ANALYZED));
			document.Add(new Field(CityFieldNames.AdministrativeDivisionName, administrativeDivisionName, Field.Store.YES, Field.Index.ANALYZED));
			document.Add(new Field(CityFieldNames.AdministrativeDivisionNameAcronym, administrativeDivisionNameAcronym, Field.Store.YES, Field.Index.ANALYZED));
			document.Add(new Field(CityFieldNames.AdministrativeDivisionAsciiName, administrativeDivisionAsciiName, Field.Store.YES, Field.Index.ANALYZED));
            document.Add(new Field(CityFieldNames.CountryName, countryName, Field.Store.YES, Field.Index.NO));
            document.Add(new Field(CityFieldNames.Timezone, timezone, Field.Store.YES, Field.Index.NO));
			document.Add(new NumericField(CityFieldNames.Population, Field.Store.YES, true).SetLongValue(population));
            return document;
		}
		
		public Dictionary<string, AdministrativeDivision> GetAdministrativeDivisionsLookup(){
			var lookup = new Dictionary<string, AdministrativeDivision>();
			var adminCodesFilePath = Path.Combine(ApplicationSettings.GeonameDataFilesDirectory.FullName, "admin1CodesASCII.txt");
            Console.WriteLine("Creating the admin codes lookup...");
            using (var fileStreamReader = new StreamReader(File.OpenRead(adminCodesFilePath), Encoding.UTF8))
            {
                string line;
                while ((line = fileStreamReader.ReadLine()) != null)
                {
					string[] fields = line.Split('\t');
                    if (fields.Count() < 4) continue;
					var adminCode = fields[0];
            		var name = fields[1];
            		var asciiName = fields[2];
					var administrativeDivision = new AdministrativeDivision(name, asciiName, adminCode);
                    lookup.Add(adminCode, administrativeDivision);
                }
            }
			return lookup;
		}
	}
	
	public class AdministrativeDivision {
		public string Name { get; set; }
		public string AsciiName { get; set; }
		public string Code { get; set; }
		
		public AdministrativeDivision(string name, string asciiName, string code){
			Name = name;
			AsciiName = asciiName;
			Code = code;
		}
	}
}

