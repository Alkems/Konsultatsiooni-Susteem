using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Aljas_Consultation.Models
{
    public class Consultation
    {
        // Id=1, Teacher=Mihkel, Classroom=215b, Day=Monaday, StartTime=15:00, EndTime=16:00, Period=First
        public int Id { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string? Teacher { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string Classroom { get; set; }    
        [Required(AllowEmptyStrings = false)]
        public string? Day { get; set; }

        [DataType(DataType.Time)]
        public DateTime StartTime { get; set; }
        [DataType(DataType.Time)]
        public DateTime EndTime { get; set; }
        public Period Session { get; set; }
        public int PeriodId { get; set; }
    }
}
