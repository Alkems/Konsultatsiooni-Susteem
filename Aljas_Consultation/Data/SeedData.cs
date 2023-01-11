using Microsoft.AspNetCore.Mvc;
using Aljas_Consultation.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Aljas_Consultation.Models;
using NuGet.DependencyResolver;

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
                if (!context.Period.Any())
                {
                    // Add the periods to the database
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

                if (!context.Consultation.Any())
                {
                    // Add the consultations to the database
                    context.Consultation.AddRange(
                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "253A",
                            Day = "Monday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "152A",
                            Day = "Tuesday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Friday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Monday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "133B",
                            Day = "Monday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Thursday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Peeter",
                            Classroom = "341A",
                            Day = "Thursday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "B",
                            Day = "Tuesday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "341A",
                            Day = "Wednseday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "341A",
                            Day = "Friday",
                            StartTime = DateTime.Now,
                            EndTime = DateTime.Now,
                            PeriodId = 3,
                        }
                    );
                    context.SaveChanges();
                }


                // Query the database for the consultations and group them by teacher and period
                var consultations = context.Consultation
                    .Include(c => c.Session)
                    .ToList()
                    .GroupBy(c => new { c.Teacher, c.Session.Name });

                // Create a list to hold the names of the teachers who do not have two consultations in each period
                var missingConsultations = new List<string>();

                // Iterate over the groups of consultations
                foreach (var group in consultations)
                {
                    // Count the number of consultations for each teacher in each period
                    var consultationCount = group.Count();

                    if (consultationCount < 2)
                    {
                        missingConsultations.Add(group.Key.Teacher);
                    }
                }
            }
        }
    }
}