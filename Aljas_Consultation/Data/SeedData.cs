using Microsoft.AspNetCore.Mvc;
using Aljas_Consultation.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Aljas_Consultation.Models;

namespace Aljas_Consultation.Data
{
    public class SeedData : Controller
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<ApplicationDbContext>>()))
            {
                // Look for any Period.
                if (context.Period.Any())
                {
                    return;   // DB has been seeded
                }

                context.Period.AddRange(
                    new Period
                    {
                        Name = "First",
                    },

                    new Period
                    {
                        Name = "Second",
                    },

                    new Period
                    {
                        Name = "Third",
                    }
                );
                context.SaveChanges();
            }
        }
    }
}