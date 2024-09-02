﻿namespace CompanyStruct.Models
{
    public class Division
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int Head { get; set; }
        public ICollection<Project> Projects { get; set; }
        public Company CompanyNavigation { get; set; }
        public Employee HeadNavigation { get; set; }
    }
}
