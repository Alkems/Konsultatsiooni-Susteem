using Microsoft.AspNetCore.Mvc;
using Aljas_Consultation.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Aljas_Consultation.Models;
using NuGet.DependencyResolver;
using Microsoft.AspNetCore.Identity;

namespace Aljas_Consultation.Data
{
    public class SeedData
    {
        public static List<string> Teachers = new List<string> { "Mihkel", "Tanel", "Peeter", "Kivikangur" };
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
                            StartTime = DateTime.Parse("12:00:00"),
                            EndTime = DateTime.Parse("14:00:00"),
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "152A",
                            Day = "Tuesday",
                            StartTime = DateTime.Parse("12:00:00"),
                            EndTime = DateTime.Parse("14:00:00"),
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Friday",
                            StartTime = DateTime.Parse("7:00:00"),
                            EndTime = DateTime.Parse("8:00:00"),
                            PeriodId = 1,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Monday",
                            StartTime = DateTime.Parse("13:00:00"),
                            EndTime = DateTime.Parse("14:00:00"),
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "133B",
                            Day = "Monday",
                            StartTime = DateTime.Parse("12:00:00"),
                            EndTime = DateTime.Parse("16:00:00"),
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "212B",
                            Day = "Thursday",
                            StartTime = DateTime.Parse("12:00:00"),
                            EndTime = DateTime.Parse("13:00:00"),
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Peeter",
                            Classroom = "341A",
                            Day = "Thursday",
                            StartTime = DateTime.Parse("12:00:00"),
                            EndTime = DateTime.Parse("13:00:00"),
                            PeriodId = 2,
                        },

                        new Consultation
                        {
                            Teacher = "Mihkel",
                            Classroom = "B",
                            Day = "Tuesday",
                            StartTime = DateTime.Parse("14:00:00"),
                            EndTime = DateTime.Parse("15:00:00"),
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "341A",
                            Day = "Wednseday",
                            StartTime = DateTime.Parse("15:00:00"),
                            EndTime = DateTime.Parse("16:00:00"),
                            PeriodId = 3,
                        },

                        new Consultation
                        {
                            Teacher = "Tanel",
                            Classroom = "341A",
                            Day = "Friday",
                            StartTime = DateTime.Parse("16:00:00"),
                            EndTime = DateTime.Parse("17:00:00"),
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

        public const string ROLE_ADMIN = "Admin";
        public static async Task SeedIdentity(UserManager<ConsultationUser> userManager, RoleManager<ConsultationRole> roleManager)
        {
            var user = await userManager.FindByNameAsync("admin@Consultation.com");
            if (user == null)
            {
                user = new ConsultationUser();
                user.Email = "admin@Consultation.com";
                user.EmailConfirmed = true;
                user.UserName = "admin@Consultation.com";
                var userResult = await userManager.CreateAsync(user);
                if (!userResult.Succeeded)
                {
                    throw new Exception($"User creation failed: {userResult.Errors.FirstOrDefault()}");
                }
                await userManager.AddPasswordAsync(user, "Pa$$w0rd");
            }
            var role = await roleManager.FindByNameAsync(ROLE_ADMIN);
            if (role == null)
            {
                role = new ConsultationRole();
                role.Name = ROLE_ADMIN;
                role.NormalizedName = ROLE_ADMIN;
                var roleResult = roleManager.CreateAsync(role).Result;
                if (!roleResult.Succeeded)
                {
                    throw new Exception(roleResult.Errors.First().Description);
                }
            }
            await userManager.AddToRoleAsync(user, ROLE_ADMIN);
        }

    }

}
