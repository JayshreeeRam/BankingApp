using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using BankingApp.Enums;
using System;


namespace BankingApp.DTOs
{
    public class SalaryDisbursementDto
    {
        public int DisbursementId { get; set; }
        public int EmployeeId { get; set; }
        public string EmployeeName { get; set; } = null!;

        public string SenderName { get; set; } = null!; 
        public int ClientId { get; set; }
        public decimal Amount { get; set; } 
        public DateTime Date { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus Status { get; set; }
        public int BatchId { get; set; }
    }
}

