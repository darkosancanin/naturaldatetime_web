using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalDateTime.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            ApplicationSettings.Initialise(System.Configuration.ConfigurationManager.AppSettings["PathToCityIndex"], String.Empty);
            System.Console.WriteLine();
            System.Console.Write("Enter the question: ");
            var userQuestion = System.Console.ReadLine();
            while (userQuestion != "exit")
            {
                var question = new Question(userQuestion);
                var questionProcessorResolver = new QuestionProcessorResolver();
                var questionProcessor = questionProcessorResolver.ResolveOrNull(question);
                Answer answer;
                var elapsedTime = 0L;
                if (questionProcessor != null)
                {
                    var stopwatch = new Stopwatch();
                    stopwatch.Start();
                    answer = questionProcessor.GetAnswer(question);
                    elapsedTime = stopwatch.ElapsedMilliseconds;
                }
                else
                {
                    answer = new Answer(question, false, false, ErrorMessages.DidNotUnderstandQuestion);
                }

                System.Console.WriteLine("Answer: " + answer.AnswerText);
                System.Console.WriteLine();
                System.Console.WriteLine("Note: " + answer.Note);
                System.Console.WriteLine();
                System.Console.WriteLine("Tokenized Question: " + question.FormatTextWithTokens());
                System.Console.WriteLine();
                System.Console.WriteLine("Token Structure: " + question.FormatTokenStructure());
                System.Console.WriteLine();
                System.Console.WriteLine("Time Taken: " + elapsedTime + "ms");
                System.Console.WriteLine();
                System.Console.Write("Enter the search term: ");
                userQuestion = System.Console.ReadLine();
            }
        }
    }
}
