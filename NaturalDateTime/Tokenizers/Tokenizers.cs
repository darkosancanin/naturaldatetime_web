using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace NaturalDateTime
{
    public class Tokenizers
    {
		private static List<ITokenizer> _tokenizers = new List<ITokenizer>();
		
		static Tokenizers()
		{
			var tokenizerType = typeof(ITokenizer);
			var classesThatImplementTokenizerInterface = typeof(ITokenizer).Assembly.GetTypes()
				.Where(p => !p.IsAbstract && p.IsClass && tokenizerType.IsAssignableFrom(p));
			foreach(var type in classesThatImplementTokenizerInterface){
				_tokenizers.Add((ITokenizer)Activator.CreateInstance(type));	
			}
		}

        public static List<ITokenizer> GetAll()
        {
			return _tokenizers;
        }
    }
}
