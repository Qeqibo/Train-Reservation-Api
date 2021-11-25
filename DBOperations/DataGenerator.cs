using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TrainReservation.Model;

namespace TrainReservation.DBOperations

{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new TrainDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<TrainDbContext>>()))
            {
                // Look for any book.
                if (context.Trains.Any())
                {
                    return;   // Data was already seeded
                }

                context.Trains.AddRange(
                   new Train()
                   {
                       
                   });

                context.SaveChanges();
            }
        }
    }
}