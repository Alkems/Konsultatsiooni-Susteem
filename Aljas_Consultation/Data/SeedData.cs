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

                context.Consultation.AddRange(
                    new Consultation
                    {
                        Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Monday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 1,
                    },

                    new Consultation
                    {
                        Teacher = "Tanel",
                        Classroom = 152,
                        Day = "Tuesday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 1,
                    },

                    new Consultation
                    {
                        Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Friday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 1,
                    },

                    new Consultation
                    {
                    Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Monday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 2,
                    },

                    new Consultation
                    {
                        Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Monday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 3,
                    },

                    new Consultation
                    {
                        Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Thursday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 2,
                    },

                    new Consultation
                    {
                        Teacher = "Mihkel",
                        Classroom = 212,
                        Day = "Tuesday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 3,
                    },

                    new Consultation
                    {
                        Teacher = "Tanel",
                        Classroom = 152,
                        Day = "Wednseday",
                        StartTime = DateTime.Now,
                        EndTime = DateTime.Now,
                        PeriodId = 3,
                    }
                );
                context.SaveChanges();
            }
        }

    }
}