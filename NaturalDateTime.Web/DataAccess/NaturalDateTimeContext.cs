using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using NaturalDateTime.Web.Models;

namespace NaturalDateTime.Web.DataAccess
{
    public class NaturalDateTimeContext : DbContext
    {
        private IQuestionLogCache _questionLogCache;
        public DbSet<QuestionLog> QuestionLog { get; set; }

        public static int MaxCacheEntries = 50;

        public NaturalDateTimeContext(IQuestionLogCache questionLogCache)
        {
            _questionLogCache = questionLogCache;
        }

        public NaturalDateTimeContext()
        {
            _questionLogCache = new InMemoryQuestionLogCache();
        }

        public void AddQuestionLog(QuestionLog questionLog)
        {
            this.QuestionLog.Add(questionLog);
            _questionLogCache.AddToCache(questionLog);
        }

        public IQuestionLogCache QuestionLogCache
        {
            get { return _questionLogCache; }
        }
    }
}
