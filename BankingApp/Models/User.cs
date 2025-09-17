using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
using BankingApp.Enums;

namespace BankingApp.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Username { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        public string Password { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [MaxLength(10)]
        public string PhoneNumber { get; set; }


        public UserRole UserRole { get; set; }



        // Relationships
        public ICollection<Client>? Clients { get; set; }
        public ICollection<Document>? Documents { get; set; }
        public ICollection<Report>? Reports { get; set; }

    }
}
