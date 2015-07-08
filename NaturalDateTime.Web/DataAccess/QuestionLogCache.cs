using NaturalDateTime.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace NaturalDateTime.Web.DataAccess
{
    public interface IQuestionLogCache
    {
        Queue<QuestionLog> LatestUserQuestionLogs { get; }
        Queue<QuestionLog> LatestBotQuestionLogs { get; }
        void AddToCache(QuestionLog questionLog);
    }

    public class InMemoryQuestionLogCache : IQuestionLogCache
    {
        private static string USER_QUESTIONS_KEY = "LatestUserQuestionLogs";
        private static string BOT_QUESTIONS_KEY = "LatestBotQuestionLogs";

        public InMemoryQuestionLogCache()
        {
            if (LatestUserQuestionLogs == null)
                LatestUserQuestionLogs = new Queue<QuestionLog>(NaturalDateTimeContext.MaxCacheEntries + 1);
            if (LatestBotQuestionLogs == null)
                LatestBotQuestionLogs = new Queue<QuestionLog>(NaturalDateTimeContext.MaxCacheEntries + 1);
        }

        public Queue<QuestionLog> LatestUserQuestionLogs
        {
            get { return (Queue<QuestionLog>)HttpContext.Current.Application[USER_QUESTIONS_KEY]; }
            set { HttpContext.Current.Application[USER_QUESTIONS_KEY] = value; }
        }

        public Queue<QuestionLog> LatestBotQuestionLogs
        {
            get { return (Queue<QuestionLog>)HttpContext.Current.Application[BOT_QUESTIONS_KEY]; }
            set { HttpContext.Current.Application[BOT_QUESTIONS_KEY] = value; }
        }

        public void AddToCache(QuestionLog questionLog)
        {
            Queue<QuestionLog> questionLogs;
            if (questionLog.IsBot)
                questionLogs = LatestBotQuestionLogs;
            else
                questionLogs = LatestUserQuestionLogs;

            questionLogs.Enqueue(questionLog);
            if (questionLogs.Count > NaturalDateTimeContext.MaxCacheEntries)
                questionLogs.Dequeue();
        }
    }
}
