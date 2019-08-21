using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using LunaCinemasBackEndInDotNet.Models;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public class ShowingContext : DbContext
    {
        public ShowingContext(DbContextOptions<ShowingContext> options)
            : base(options)
        {
        }

        public DbSet<LunaCinemasBackEndInDotNet.Models.Showing> Showing { get; set; }
    }
}