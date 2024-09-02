namespace CompanyStruct.Models
{
    public class EmployeeType
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public ICollection<Employee> Employees { get; set; }
    }
}
