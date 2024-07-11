namespace UsingDapperformultipletableInASPnetcoreWebapi.Entities
{
    public class Company
    {

        public int Id { get; set; }

    /*    public string ?CompanyName { get; set; }*/ // Using different name from dababase also it map but we have to set it in SQL whre it needed

        public string Name { get; set; }
        public string? Address { get; set; }
        public string? Country { get; set; }
        public List<Employee> Employees { get; set; } = new List<Employee>();


    }
}
