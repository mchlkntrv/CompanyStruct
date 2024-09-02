namespace CompanyStruct.Models
{
    public class Project
    {
        public int Id { get; set; }
        public int DivisionId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Head { get; set; }
        public ICollection<Department> Departments { get; set; }
    }
}
