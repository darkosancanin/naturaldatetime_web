using NaturalDateTime.Services;
using NodaTime;
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
                System.Console.WriteLine();
                var answerService = new AnswerService();
                var answer = answerService.GetAnswer(userQuestion, true);
                foreach (var debugInformation in answer.DebugInformation)
                    System.Console.WriteLine(String.Format("{0}: {1}", debugInformation.Name, debugInformation.Value));
                System.Console.WriteLine();
                System.Console.WriteLine("Answer: " + answer.AnswerText);
                System.Console.WriteLine();
                if (!String.IsNullOrEmpty(answer.Note))
                {
                    System.Console.WriteLine("Note: " + answer.Note);
                    System.Console.WriteLine();
                }
                System.Console.WriteLine();
                System.Console.Write("Enter the search term: ");
                userQuestion = System.Console.ReadLine();
            }
        }
    }
}
