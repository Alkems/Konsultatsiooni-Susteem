using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Aljas_Consultation.Models
{
    public class ConsultationRole : IdentityRole
    {
        [StringLength(128, MinimumLength = 1)]
        public string DisplayName { get; set; }
    }
}
