using System;
using System.Text.Json.Serialization;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class PaymentDto
    {
        public int PaymentId { get; set; }
        public int ClientId { get; set; }
        public int BeneficiaryId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }

        [JsonConverter(typeof(JsonStringEnumConverter))]
        public PaymentStatus PaymentStatus { get; set; } = PaymentStatus.Pending;
      

    }
}
