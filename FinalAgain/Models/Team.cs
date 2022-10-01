using System.Collections.Generic;

namespace FinalAgain.Models
{
    public class Team
    {
        public int Id { get; set; }
        public int TeamNo { get; set; }
        public string StudentName1 { get; set; }
        public string StudentName2 { get; set; }
        public string StudentName3 { get; set; }
        public string StudentName4 { get; set; }
        public int TotalGrade { get; set; }
        public List<TeamMakrsheet> TeamMakrsheets { get; set; } = new List<TeamMakrsheet>();
    }
}
