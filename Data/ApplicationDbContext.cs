using Microsoft.EntityFrameworkCore;
using MiniSimulator.Models;
using System.Collections.Generic;

namespace MiniSimulator.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Team> Team { get; set; }
        
    }
}
