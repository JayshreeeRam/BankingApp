using BankingApp.Enums;

public class ClientDto
{
    public int ClientId { get; set; }
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public int BankId { get; set; }
    public int UserId { get; set; }
    public AccountStatus VerificationStatus { get; set; }
    public AccountType AccountType { get; set; }
    public string? AccountNo { get; set; }    // mapped from enum
}