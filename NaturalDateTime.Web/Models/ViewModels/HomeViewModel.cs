using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NaturalDateTime.Web.Models.ViewModels
{
    public class HomeViewModel
    {
        public string QuestionText { get; set; }
        public string AnswerText { get; set; }
        public string Note { get; set; }
        public bool DebugInfoEnabled { get; set; }

        public HomeViewModel(bool debugInfoEnabled)
        {
            DebugInfoEnabled = debugInfoEnabled;
        }

        public HomeViewModel(string questionText, string answerText, string note) 
        {
            QuestionText = questionText;
            AnswerText = answerText;
            Note = note;
        }
    }
}