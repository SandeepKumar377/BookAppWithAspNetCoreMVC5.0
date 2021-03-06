using DiverseBookApp.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DiverseBookApp.Data
{
    public class BookAppContext : IdentityDbContext<ApplicationUsers>
    {
        public BookAppContext(DbContextOptions<BookAppContext> options) : base(options)
        {
        }
        public DbSet<Books> Books { get; set; }
        public DbSet<BookGallery> BookGallery { get; set; }
        public DbSet<Language> Language { get; set; }

    }
}
