using System;
using System.Linq;
using System.Collections.Generic;

namespace NaturalDateTime
{
	public class QuestionProcessorResolver
	{
		private static readonly IList<IQuestionProcessor> _questionProcessors = new List<IQuestionProcessor>();
		
		static QuestionProcessorResolver()
		{
			var questionProcessorType = typeof(IQuestionProcessor);
			var classesThatImplementQuestionProcessorInterface = AppDomain.CurrentDomain.GetAssemblies().ToList()
				.SelectMany(s => s.GetTypes())
				.Where(p => !p.IsAbstract && p.IsClass && questionProcessorType.IsAssignableFrom(p));
			foreach(var type in classesThatImplementQuestionProcessorInterface){
				_questionProcessors.Add((IQuestionProcessor)Activator.CreateInstance(type));	
			}
		}
		
		public IQuestionProcessor ResolveOrNull(Question question)
		{
			foreach(var questionProcessor in _questionProcessors){
				if(questionProcessor.CanAnswerQuestion(question))
					return questionProcessor;
			}
			return null;
		}
	}
}

