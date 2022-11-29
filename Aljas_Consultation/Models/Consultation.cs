using Microsoft.EntityFrameworkCore;

namespace Aljas_Consultation.Models
{
    public class Consultation
    {
        // Id=1, Teacher=Mihkel, Classroom=215b, Day=Monaday, StartTime=15:00, EndTime=16:00, Period=First
        public int Id { get; set; }
        public string? Teacher { get; set; }
        public int Classroom { get; set; }
        public string? Day { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public Period Session { get; set; }
        public int PeriodId { get; set; }
    }
}
