using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BankingApp.Models
{
    public class Bank
    {
        [Key]
        public int BankId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get;  set; } = null!;

        [MaxLength(200)]
        public string? Address { get; set; }

        [Required]
        [MaxLength(100)]
        public string IFSCCODE { get;  set; }

        // Relationships
        public ICollection<Client>? Clients { get; set; }
        //public ICollection<Employee>? Employees { get; set; }
    }
}
