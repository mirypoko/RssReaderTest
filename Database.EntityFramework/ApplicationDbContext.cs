using Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.EntityFramework
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<News> News { get; set; }

        public DbSet<RssSource> RssSources { get; set; }

        public ApplicationDbContext() : base("DefaultDbConnection")
        { }
    }
}
