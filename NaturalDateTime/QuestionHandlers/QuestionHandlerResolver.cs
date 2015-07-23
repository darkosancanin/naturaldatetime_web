using System;
using System.Linq;
using System.Collections.Generic;

namespace NaturalDateTime
{
	public class QuestionHandlerResolver
	{
		private static readonly IList<IQuestionHandler> _questionHandlers = new List<IQuestionHandler>();
		
		static QuestionHandlerResolver()
		{
			var questionHandlerType = typeof(IQuestionHandler);
			var classesThatImplementQuestionHandlerInterface = AppDomain.CurrentDomain.GetAssemblies().ToList()
				.SelectMany(s => s.GetTypes())
				.Where(p => !p.IsAbstract && p.IsClass && questionHandlerType.IsAssignableFrom(p));
			foreach(var type in classesThatImplementQuestionHandlerInterface){
				_questionHandlers.Add((IQuestionHandler)Activator.CreateInstance(type));	
			}
		}
		
		public IQuestionHandler FindQuestionHandlerThatCanAnswerTheQuestion(Question question)
		{
			foreach(var questionHandler in _questionHandlers){
				if(questionHandler.CanAnswerQuestion(question))
					return questionHandler;
			}
			return null;
		}
	}
}

