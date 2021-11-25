using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrainReservation.Model;

namespace TrainReservation.DBOperations
{
    public class TrainDbContext:DbContext

    {
        public TrainDbContext(DbContextOptions<TrainDbContext> options) : base(options)
        {

        }
        public DbSet<Train> Trains { get; set; }
        public DbSet<Wagon> Wagons { get; set; }
        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("Data Source=Train.db");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Wagon>().HasOne(p => p.Train).WithMany(b => b.Wagons);
        }*/
    }
}
