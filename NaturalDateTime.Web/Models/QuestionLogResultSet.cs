using System;
using NodaTime;
using System.Globalization;
using System.Collections.Generic;

namespace NaturalDateTime.Web.Models
{
    public class QuestionLogResultSet
    {
        public int TotalResults { get; set; }
        public IEnumerable<QuestionLog> QuestionLogs { get; set; }

        public QuestionLogResultSet(int totalResults, IEnumerable<QuestionLog> questionLogs)
        {
            TotalResults = totalResults;
            QuestionLogs = questionLogs;
        }
    }
}

