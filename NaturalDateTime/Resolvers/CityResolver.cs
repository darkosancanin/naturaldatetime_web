using System;
using System.Linq;
using System.Collections.Generic;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.QueryParsers;
using Lucene.Net.Search;
using Lucene.Net.Search.Spans;
using Lucene.Net.Store;
using NaturalDateTime.Domain;

namespace NaturalDateTime
{
	public class CityResolver
	{
        public CityResolverResult Resolve(CityToken cityToken)
        {
            var queryParser = GetQueryParser();
            var searcher = GetIndexSearcher();
            var sort = GetSort();

            var possibleCityDetails = cityToken.GetPotentialCityDetails();
            foreach (var possibleCityDetail in possibleCityDetails)
            {
                var topScoreDocCollector = TopFieldCollector.Create(sort, 1, true, false, false, false);
				var countryCode = string.Empty;
				if(!string.IsNullOrEmpty(possibleCityDetail.CountryName)){
					countryCode = CountryCodes.LookupCountryCode(possibleCityDetail.CountryName);
					if(string.IsNullOrEmpty(countryCode)) continue;
				}
                var queryText = GetQueryText(possibleCityDetail.CityName, countryCode, possibleCityDetail.AdministrativeDivisionName);

                var query = queryParser.Parse(queryText);
                searcher.Search(query, topScoreDocCollector);
                var results = topScoreDocCollector.TopDocs().ScoreDocs;

                if (topScoreDocCollector.TotalHits > 0)
                {
                    var docId = results[0].Doc;
                    var document = searcher.Doc(docId);
                    var city = new City(document);
                    return new CityResolverResult(city);
                }
            }

            return new CityResolverResult(CityResolverResultStatus.FAILED);
        }

        private string GetQueryText(string cityName, string countryCode, string administrativeDivision)
        {
            var queryText = String.Format("({1}:\"{0}\" OR {2}:\"{0}\" OR {3}:\"{0}\")", QueryParser.Escape(cityName), CityFieldNames.Name, CityFieldNames.AlternateNames, CityFieldNames.AsciiName);
            
			if (!string.IsNullOrEmpty(administrativeDivision))
                queryText += String.Format(" AND ({1}:\"{0}\" OR {2}:\"{0}\" OR {3}:\"{0}\")", QueryParser.Escape(administrativeDivision), CityFieldNames.AdministrativeDivisionName, CityFieldNames.AdministrativeDivisionAsciiName, CityFieldNames.AdministrativeDivisionNameAcronym);

			if (!string.IsNullOrEmpty(countryCode))
                queryText += String.Format(" AND {0}:\"{1}\"", CityFieldNames.CountryCode, countryCode);
            
			return queryText;
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
	
	public enum CityResolverStatus
	{
		SUCCESSFUL,
		FAILED
	}
}

