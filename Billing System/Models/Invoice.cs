using System;
using System.ComponentModel.DataAnnotations;

namespace BillingSystem.Models
{
    public class Invoice
    {
        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public decimal InvoiceAmount { get; set; }

        [MaxLength(50, ErrorMessage = "Invoice Description can only be 50 characters long")]
        public string InvoiceDescription { get; set; }

        [Required]
        public int PaymentStatus { get; set; }

        [Required]
        public int BillNumber { get; set; }
    }
}
