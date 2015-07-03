using System;
using System.IO;

namespace NaturalDateTime
{
	public static class ApplicationSettings
	{
		public static string FullPathToCityIndex { get; set; } 
		public static DirectoryInfo CityIndexDirectory { get; set; }
		public static DirectoryInfo GeonameDataFilesDirectory { get; set; }
		public static string AnswerLogConnectionString { get; set; }
		public static string FullPathToAnswerLogDatabase { get; set; }

		public static void Initialise(string fullPathToCityIndex, string fullPathToAnswerLogDatabase){
			FullPathToCityIndex = fullPathToCityIndex;
			CityIndexDirectory = new DirectoryInfo(fullPathToCityIndex);
			GeonameDataFilesDirectory = new DirectoryInfo(Path.Combine(fullPathToCityIndex, "GeonameDataFiles"));
			AnswerLogConnectionString = "Data Source=" + fullPathToAnswerLogDatabase;
			FullPathToAnswerLogDatabase = fullPathToAnswerLogDatabase;
		}
		
		public static string WebClientName {
			get { return "web"; }	
		}
		
		public static string WebApplicationVersion
        {
			get { return "2.0"; }	
		}
	}
}

