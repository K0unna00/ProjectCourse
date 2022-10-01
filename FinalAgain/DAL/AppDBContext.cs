using FinalAgain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinalAgain.DAL
{
    public class AppDBContext : IdentityDbContext
    {
        private readonly DbContextOptions _options;

        public AppDBContext(DbContextOptions options) : base(options)
        {
            _options = options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<Mark> Marks{ get; set; }
        public DbSet<UserMarksheet> UserMarksheets{ get; set; }
        public DbSet<Marksheet> Marksheets{ get; set; }
        public DbSet<Question> Questions{ get; set; }
        public DbSet<Team> Teams { get; set; }
        public DbSet<TeamMakrsheet> TeamMakrsheets { get; set; }
        public DbSet<Assignment> Assignments { get; set; }


    }
}
