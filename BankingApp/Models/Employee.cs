using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BankingApp.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        // FK → Bank
        public int BankId { get; set; }
        [ForeignKey(nameof(BankId))]
        public Bank? Bank { get; set; }

        [Required]
        public double Salary { get; set; }

        // FK → Client
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public Client? Client { get; set; }

        [JsonIgnore]
        public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }
    }
}
