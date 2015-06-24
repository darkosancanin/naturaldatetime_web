using System;
using NodaTime;
using System.Globalization;
using System.Collections.Generic;

namespace NaturalDateTime.Web.Models
{
    public class QuestionLogResultSet
    {
        public int TotalResults { get; set; }
        public IList<QuestionLog> QuestionLogs { get; set; }

        public QuestionLogResultSet(int totalResults, IList<QuestionLog> questionLogs)
        {
            TotalResults = totalResults;
            QuestionLogs = questionLogs;
        }
    }
}

