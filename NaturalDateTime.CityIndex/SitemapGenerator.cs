using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.QueryParsers;

namespace NaturalDateTime.CityIndex
{
	public class SitemapGenerator
	{
		private SiteMapQuestion[] _siteMapQuestions = new [] { 
			new SiteMapQuestion("sitemap_time_in_city{0}.xml", "Time_in_"),
			new SiteMapQuestion("sitemap_what_is_the_time_in_city{0}.xml", "What_is_the_time_in_"),
			new SiteMapQuestion("sitemap_current_time_in_city{0}.xml", "Current_time_in_"),
			new SiteMapQuestion("sitemap_daylight_saving_time_in_city{0}.xml", "Daylight_saving_time_in_"),
			new SiteMapQuestion("sitemap_when_does_daylight_saving_time_begin_in_city{0}.xml", "When_does_daylight_saving_time_start_in_"),
			new SiteMapQuestion("sitemap_when_does_daylight_saving_time_end_in_city{0}.xml", "When_does_daylight_saving_time_end_in_") 
		};
		
		private Lucene.Net.Store.Directory IndexDirectory{
			get { return FSDirectory.Open(ApplicationSettings.CityIndexDirectory); }
		}
		
		private string GetSitemapFullPath(string filename){
			return Path.Combine(Path.Combine(ApplicationSettings.CityIndexDirectory.FullName, "Sitemaps"), filename);
		}

        public void GenerateSitemaps()
        {
            var queryParser = GetQueryParser();
            var query = queryParser.Parse("a* b* c* d* e* f* g* h* i* j* k* l* m* n* o* p* q* r* s* t* u* v* w* x* y* z*");
			var searcher = GetIndexSearcher();
			int maximumRecords = 3337421;
			var topScoreDocCollector = Lucene.Net.Search.TopFieldCollector.Create(GetSort(), maximumRecords, true, true, true, false);
            searcher.Search(query, topScoreDocCollector);
            var results = topScoreDocCollector.TopDocs().ScoreDocs;
            int totalHits = topScoreDocCollector.TotalHits;
            Console.WriteLine(totalHits + " total hits.");
            var hitsToDisplay = (totalHits >= maximumRecords) ? maximumRecords : totalHits;
			var cities = new List<City>();
            for (int i = 0; i < hitsToDisplay; i++)
            {
                var docId = results[i].Doc;
                Document doc = searcher.Doc(docId);
				var city = new City(doc);
				cities.Add(city);
            }
			
			int maximumRecordsPerFile = 50000;
			var numberOfFiles = Math.Ceiling((double)hitsToDisplay/(double)maximumRecordsPerFile);
			
			foreach(var siteMapQuestion in _siteMapQuestions){
				for(int fileNumber = 1; fileNumber <= numberOfFiles; fileNumber++){
					var batchOfCities = cities.Skip(maximumRecordsPerFile * (fileNumber - 1)).Take(maximumRecordsPerFile).ToList();
					WriteSitemap(String.Format(siteMapQuestion.FileNameFormat, fileNumber), siteMapQuestion.QuestionPortionOfUrl, batchOfCities);
				}
			}

			WriteSitemapIndex(numberOfFiles);
			Console.WriteLine("Finished generating sitemaps.");
        }
		
		private void WriteSitemap(string filename, string questionPortionOfUrl, List<City> cities){
			var filePath = GetSitemapFullPath(filename);
			XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);
	        writer.Formatting = Formatting.Indented;
	        writer.WriteStartDocument();
	        writer.WriteStartElement("urlset");
	        writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
	        foreach (var city in cities)
	        {
	            writer.WriteStartElement("url");
				var cityName = GetUrlEncodedCityName(city);
				writer.WriteElementString("loc", String.Format("http://www.naturaldateandtime.com/q/{0}{1}", questionPortionOfUrl, cityName));
	            writer.WriteEndElement();
	        }
	        writer.WriteEndElement();
	        writer.WriteEndDocument();
	        writer.Flush();
	        writer.Close();
		}
		
		private void WriteSitemapIndex(double numberOfFiles){
			var filePath = GetSitemapFullPath("sitemap.xml");
			XmlTextWriter writer = new XmlTextWriter(filePath, Encoding.UTF8);
	        writer.Formatting = Formatting.Indented;
	        writer.WriteStartDocument();
	        writer.WriteStartElement("sitemapindex");
	        writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
	        foreach (var siteMapQuestion in _siteMapQuestions)
	        {
				for(int i = 1; i <= numberOfFiles; i++)
				{
	            	writer.WriteStartElement("sitemap");
					writer.WriteElementString("loc", "http://www.naturaldateandtime.com/" + String.Format(siteMapQuestion.FileNameFormat, i));
	            	writer.WriteEndElement();
				}
	        }
	        writer.WriteEndElement();
	        writer.WriteEndDocument();
	        writer.Flush();
	        writer.Close();
		}
		
		private string GetUrlEncodedCityName(City city){
			var cityName = city.AsciiName ?? city.Name; 
			cityName = cityName.Replace(",", "%2C").Replace("'", "%27").Replace("’", "%27").Replace(" ", "_");	
			return cityName;
		}
		
		private QueryParser GetQueryParser()
        {
            var analyzer = new Lucene.Net.Analysis.Standard.StandardAnalyzer(Lucene.Net.Util.Version.LUCENE_30);
            var queryParser = new QueryParser(Lucene.Net.Util.Version.LUCENE_30, CityFieldNames.Name, analyzer);
            return queryParser;
        }

        private IndexSearcher GetIndexSearcher()
        {
            var searcher = new IndexSearcher(FSDirectory.Open(ApplicationSettings.CityIndexDirectory), true);
            return searcher;
        }
		
		private Sort GetSort()
        {
            var sort = new Sort(new[] { new SortField(CityFieldNames.Population, SortField.LONG, true), SortField.FIELD_SCORE });
            return sort;
        }
	}
	
	public class SiteMapQuestion{
		public string FileNameFormat { get;set; }
		public string QuestionPortionOfUrl { get;set; }
		
		public SiteMapQuestion(string fileNameFormat, string questionPortionOfUrl){
			FileNameFormat = fileNameFormat;
			QuestionPortionOfUrl = questionPortionOfUrl;
		}
	}
}

