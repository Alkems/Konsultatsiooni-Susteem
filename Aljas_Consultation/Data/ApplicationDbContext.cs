using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Aljas_Consultation.Models;

namespace Aljas_Consultation.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Aljas_Consultation.Models.Consultation> Consultation { get; set; }
        public DbSet<Aljas_Consultation.Models.Period> Period { get; set; }

        public DbSet<ConsultationUser> ConsultationUser { get; set; }
        public DbSet<ConsultationRole> ConsultationRole { get; set; }
    }
}