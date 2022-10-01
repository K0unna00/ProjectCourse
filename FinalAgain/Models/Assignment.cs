namespace FinalAgain.Models
{
    public class Assignment
    {
        public int Id { get; set; }
        public string AdminId { get; set; }
        public string Desc { get; set; }
        public string Name { get; set; }
        public AppUser Admin { get; set; }

    }
}
