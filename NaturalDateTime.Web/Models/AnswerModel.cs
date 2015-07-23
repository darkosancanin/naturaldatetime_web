using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
using System.Collections;
using NaturalDateTime.Domain;

namespace NaturalDateTime.Web.Models
{
	public class AnswerModel
    {
		public string AnswerText { get; set; }
        public string Note { get; set; }
        public IList<DebugInformation> DebugInformation { get; set; }

        public bool UnderstoodQuestion { get; set; }

        public AnswerModel(Answer answer)
		{
            AnswerText = answer.AnswerText;
            Note = answer.Note;
            DebugInformation = answer.DebugInformation;
            UnderstoodQuestion = answer.UnderstoodQuestion;
        }
    }
}