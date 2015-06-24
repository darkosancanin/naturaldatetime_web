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
        public DbSet<QuestionLog> QuestionLog { get; set; }
    }
}
