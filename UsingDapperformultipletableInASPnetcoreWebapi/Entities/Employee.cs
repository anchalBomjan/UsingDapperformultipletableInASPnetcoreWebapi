namespace UsingDapperformultipletableInASPnetcoreWebapi.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Age { get; set; }
        public string? Position { get; set; }

        public int CompanyId { get;set;}

    }
}
