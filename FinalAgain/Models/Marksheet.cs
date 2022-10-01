using System;
using System.Collections.Generic;

namespace FinalAgain.Models
{
    public class Marksheet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string AdminId { get; set; }//this property for the save the data that which Admin created this Marksheet
        public int AssignmentId { get; set; }
        public DateTime CreatedAt { get; set; }
        public AppUser Admin { get; set; }
        public Assignment Assignment { get; set; }
        public List<TeamMakrsheet> TeamMakrsheets { get; set; } = new List<TeamMakrsheet>();

    }
}
