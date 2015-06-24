using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Lucene.Net.Documents;
using Lucene.Net.Index;
using Lucene.Net.Search;
using Lucene.Net.Store;
using Lucene.Net.Util;
using Lucene.Net.QueryParsers;

namespace NaturalDateTime.CityIndex
{
	public class CitySearcher
	{
		private Lucene.Net.Store.Directory IndexDirectory{
			get { return FSDirectory.Open(ApplicationSettings.CityIndexDirectory); }
		}

        public void SearchTheIndex(string cityName)
        {
            var queryParser = GetQueryParser();
            var query = queryParser.Parse(String.Format("({1}:\"{0}\" OR {2}:\"{0}\")", cityName, CityFieldNames.Name, CityFieldNames.AlternateNames));
			var searcher = GetIndexSearcher();
			var topScoreDocCollector = Lucene.Net.Search.TopFieldCollector.Create(GetSort(), 100, true, true, true, false);
            searcher.Search(query, topScoreDocCollector);
            var results = topScoreDocCollector.TopDocs().ScoreDocs;
            int totalHits = topScoreDocCollector.TotalHits;
            Console.WriteLine(totalHits + " total hits.");
            int maxHits = 5;
            var hitsToDisplay = (totalHits >= maxHits) ? maxHits : totalHits;
            for (int i = 0; i < hitsToDisplay; i++)
            {
                var docId = results[i].Doc;
                Document doc = searcher.Doc(docId);
				var city = new City(doc);
				Console.WriteLine(city.FormattedName);
            }
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
}

