using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using BankingApp.Models;

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
    // FK → Employer Client (who pays salary)
    [Required]
    public int EmployerClientId { get; set; }
    [ForeignKey(nameof(EmployerClientId))]
    public Client? EmployerClient { get; set; }

    // FK → Employee Client (the employee as a client themselves)
    [Required]
    public int EmployeeClientId { get; set; }
    [ForeignKey(nameof(EmployeeClientId))]
    public Client? EmployeeClient { get; set; }

    [JsonIgnore]
    public ICollection<SalaryDisbursement>? SalaryDisbursements { get; set; }
}
