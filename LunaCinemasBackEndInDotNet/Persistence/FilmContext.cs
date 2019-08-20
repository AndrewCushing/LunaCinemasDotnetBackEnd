using LunaCinemasBackEndInDotNet.Models;
using Microsoft.EntityFrameworkCore;

namespace LunaCinemasBackEndInDotNet.Persistence
{
    public class FilmContext : DbContext
    {
        public FilmContext(DbContextOptions<FilmContext> options) : base(options)
        {
        }

        public DbSet<Film> Films { get; set; }
    }
}