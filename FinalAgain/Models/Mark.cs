using System;

namespace FinalAgain.Models
{
    public class Mark
    {
        public int Id { get; set; }
        public string MarkerId{ get; set; }
        public int QuestionId { get; set; }
        public int TeamId { get; set; }
        public int Grade { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser Marker { get; set; }
        public Team Team { get; set; }
        public Question Question { get; set; }

    }
}
