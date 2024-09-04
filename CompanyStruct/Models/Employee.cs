using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CompanyStruct.Models
{
    public class Employee
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("type")]
        public int TypeId { get; set; }

        [MaxLength(50)]
        [Column("first_name")]
        public string FirstName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("last_name")]
        public string LastName { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("title")]
        public string? Title { get; set; }

        [MaxLength(50)]
        [Column("phone")]
        public string Phone { get; set; } = string.Empty;

        [MaxLength(50)]
        [Column("email")]
        public string Email { get; set; } = string.Empty;
    }
}
