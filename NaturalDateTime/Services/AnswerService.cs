using NaturalDateTime.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime.Services
{
    public class AnswerService
    {
        public Answer GetAnswer(string questionText) {
            return GetAnswer(questionText, false);
        }

        public Answer GetAnswer(string questionText, bool includeDebugInformation)
        {
            var stopWatch = new Stopwatch();
            stopWatch.Start();
            var question = new Question(questionText);
            Answer answer = null;
            var questionProcessorResolver = new QuestionProcessorResolver();
            var questionProcessor = questionProcessorResolver.ResolveOrNull(question);

            try
            {
                if (questionProcessor != null)
                    answer = questionProcessor.GetAnswer(question);
                else
                    answer = new Answer(question, false, false, ErrorMessages.DidNotUnderstandQuestion);
            }
            catch(InvalidTokenValueException ex)
            {
                var errorMessage = ErrorMessages.DidNotUnderstandQuestion;
                var understoodQuestion = questionProcessor != null && questionProcessor.UnderstoodQuestion;      
                if (understoodQuestion) errorMessage = ex.Message;
                answer = new Answer(question, understoodQuestion, false, errorMessage);
            }

            if (includeDebugInformation)
            {
                answer.AddDebugInformation(question.DebugInformation);
                answer.AddDebugInformation("Processing Time", String.Format("{0} ms", stopWatch.ElapsedMilliseconds.ToString()));
                answer.AddDebugInformation("Tokens", answer.Question.FormatTextWithTokens());
            }
            
            return answer;
        }
    }
}
