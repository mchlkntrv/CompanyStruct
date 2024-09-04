using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CompanyStruct.Models
{
    public class EmployeeType
    {
        [Column("id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [Column("name")]
        public string TypeName { get; set; } = string.Empty;
    }
}
