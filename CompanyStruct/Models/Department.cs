namespace CompanyStruct.Models
{
    public class Department
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Head { get; set; }
        public Project ProjectNavigation { get; set; }
        public Employee HeadNavigation { get; set; }
    }
}
