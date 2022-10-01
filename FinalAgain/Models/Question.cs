namespace FinalAgain.Models
{
    public class Question
    {
        public int Id { get; set; }
        public int MarksheetId { get; set; }
        public string Content { get; set; }
        public string AdminId { get; set; } //this property for the save the data that which admin created this question
        public int GradeLimit { get; set; }
        public Marksheet Marksheet { get; set; }
        public AppUser Admin { get; set; }
    }
}
