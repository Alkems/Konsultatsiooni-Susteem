namespace Aljas_Consultation.Models
{
    public class Period
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Consultation>? Consultations { get; set; }
    }
}
