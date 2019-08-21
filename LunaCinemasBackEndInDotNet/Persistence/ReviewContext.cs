using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public class ReviewContext : DbContext
    {
        public ReviewContext (DbContextOptions<ReviewContext> options)
            : base(options)
        {
        }

        public DbSet<LunaCinemasBackEndInDotNet.Models.Review> Review { get; set; }
    }
}
