using System;
using System.ComponentModel.DataAnnotations;
using BankingApp.Enums;

namespace BankingApp.DTOs
{
    public class ReportDto
    {
        public int ReportId { get; set; }
        public ReportType ReportType { get; set; }
        public DateTime GeneratedDate { get; set; }
        public string? FilePath { get; set; }
        public int GeneratedByUserId { get; set; }
        public string? GeneratedByUsername { get; set; } 
    }
}