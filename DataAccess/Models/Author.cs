namespace DataAccess.Models
{
    public class Author
    {
        public int Id { get; set; } = 0;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        public Author() { }

        public Author(string firstName, string lastName)
        {
            
            FirstName = firstName;
            LastName = lastName;
        }

        public Author(int id, string firstName, string lastName)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
