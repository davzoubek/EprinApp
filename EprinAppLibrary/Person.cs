namespace EprinAppLibrary
{
    public class Person
    {
        public int Id {  get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        public override string ToString()
        {
            return $"{Id}: {FirstName} {LastName}";
        }
    }
}
