namespace FinalAgain.Models
{
    public class TeamMakrsheet
    {
        public int Id { get; set; }
        public int TeamId { get; set; }
        public int MarksheetId { get; set; }
        public Team Team { get; set; }
        public Marksheet Marksheet { get; set; }
    }
}
