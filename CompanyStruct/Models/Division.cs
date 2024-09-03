namespace CompanyStruct.Models
{
    public class Division
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Head { get; set; }
    }
}
