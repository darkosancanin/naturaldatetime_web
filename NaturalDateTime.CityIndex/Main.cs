using System;
using System.Diagnostics;

namespace NaturalDateTime.CityIndex
{
    class Program
    {
        static void Main(string[] args)
        {
			ApplicationSettings.Initialise(System.Configuration.ConfigurationManager.AppSettings["PathToCityIndex"], String.Empty);
            PrintMenu();
            var input = Console.ReadLine();
            while (input != "exit")
            {
                if (input == "1") SearchTheIndex();
                if (input == "2") AddDocumentsToIndex();
				if (input == "3") GenerateSitemaps();
                PrintMenu();
                input = Console.ReadLine();
            }
        }
		
		static void PrintMenu()
        {
            Console.WriteLine();
            Console.WriteLine("Select from one of the following: ");
            Console.WriteLine("1. Search");
            Console.WriteLine("2. Add Documents to Index");
			Console.WriteLine("3. Generate Sitemaps");
        }
		
		private static void GenerateSitemaps(){
			var sitemapGenerator = new SitemapGenerator();
			sitemapGenerator.GenerateSitemaps();
		}
		
		private static void AddDocumentsToIndex(){
			var cityIndexer = new CityIndexer();
			cityIndexer.AddGeonamesDataFileToCityIndex();
		}
		
		private static void SearchTheIndex()
        {
			var citySearcher = new CitySearcher();
            Console.WriteLine();
            Console.Write("Enter the search term: ");
            var cityName = Console.ReadLine();
            while (cityName != "exit")
            {
				var stopwatch = new Stopwatch();
				stopwatch.Start();
                citySearcher.SearchTheIndex(cityName);
				Console.WriteLine("Time Taken: " + stopwatch.ElapsedMilliseconds + "ms");
				Console.WriteLine();
                Console.Write("Enter the search term: ");
                cityName = Console.ReadLine();
            }
        }
    }
}
