using Microsoft.AspNetCore.Mvc.Rendering;

namespace Aljas_Consultation.Models
{
    public class ConsultationTeacherViewModel
    {
        public List<Consultation>? Consultations { get; set; }
        public SelectList? Teachers { get; set; }
        public string? ConsultationTeacher { get; set; }
        public string? SearchString { get; set; }
    }
}
