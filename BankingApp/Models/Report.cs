using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using BankingApp.Enums;

namespace BankingApp.Models
{
    public class Report
    {
        [Key]
        public int ReportId { get; set; }

        [Required, MaxLength(50)]
        public ReportType ReportType { get; set; }

        public DateTime GeneratedDate { get; set; } = DateTime.UtcNow;

        public string? FilePath { get; set; }

        // FK → User
        public int GeneratedByUserId { get; set; }
        [ForeignKey(nameof(GeneratedByUserId))]
        public User GeneratedBy { get; set; } = null!;

    }
}
