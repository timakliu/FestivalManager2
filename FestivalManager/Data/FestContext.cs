using FestivalManager.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalManager.Data
{
    public class FestContext :DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Fest> Fests { get; set; } = null!;
        public DbSet<Participant> Participants { get; set; } = null!;
        public DbSet<Judge> Judges { get; set; } = null!;
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=FestManager.db");
        }
    }
}
