using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public class CommentContext : DbContext
    {
        public CommentContext (DbContextOptions<CommentContext> options)
            : base(options)
        {
        }

        public DbSet<LunaCinemasBackEndInDotNet.Models.Comment> Comment { get; set; }
    }
}
