namespace FinalAgain.Models
{
    public class UserMarksheet
    {
        public int Id { get; set; }
        public string MarkerId { get; set; }
        public int MarksheetId { get; set; }
        public AppUser Marker { get; set; }
        public Marksheet Marksheet { get; set; }
    }
}
