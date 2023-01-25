using System.ComponentModel.DataAnnotations;

namespace Aljas_Consultation.Models
{
    public class Period
    {
        public int Id { get; set; }
        [StringLength(20)]
        [Required(AllowEmptyStrings = false)]
        public string Name { get; set; }
        public ICollection<Consultation>? Consultations { get; set; }
    }
}
