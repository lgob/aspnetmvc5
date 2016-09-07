using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace aspnetmvc5.Models
{
    public class TopicsDBContext:DbContext
    {
        public TopicsDBContext() : base("TopicsConnection") { }

        public DbSet<Topic> Topics { get; set; }
        public DbSet<Comment> Comments { get; set; }
    }
}