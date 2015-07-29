﻿using NaturalDateTime.Exceptions;
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
            var questionHandlerResolver = new QuestionHandlerResolver();
            var questionHandler = questionHandlerResolver.FindQuestionHandlerThatCanAnswerTheQuestion(question);

            try
            {
                if (questionHandler != null)
                {
                    question.ResolveTokenValues();
                    answer = questionHandler.GetAnswer(question);
                }
                else
                    answer = new Answer(question, false, false, ErrorMessages.DidNotUnderstandQuestion);
            }
            catch(InvalidTokenValueException ex)
            {
                var errorMessage = ErrorMessages.DidNotUnderstandQuestion;
                var understoodQuestion = questionHandler != null && questionHandler.UnderstoodQuestion;      
                if (understoodQuestion) errorMessage = ex.Message;
                answer = new Answer(question, understoodQuestion, false, errorMessage);
            }

            if (includeDebugInformation)
            {
                answer.AddDebugInformation(question.DebugInformation);
                answer.AddDebugInformation("Processing Time", String.Format("{0} ms", stopWatch.ElapsedMilliseconds.ToString()));
                answer.AddDebugInformation("Tokens", answer.Question.FormatTextWithTokens());
                answer.AddDebugInformation("Token Structure", answer.Question.FormatTokenStructure());
            }
            
            return answer;
        }
    }
}
