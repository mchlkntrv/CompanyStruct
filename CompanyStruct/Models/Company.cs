using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyStruct.Models
{
    public class Company
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("code")]
        public string Code { get; set; } = string.Empty;

        [Column("head")]
        public int Head { get; set; }
    }
}
