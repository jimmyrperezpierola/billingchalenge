using System;
using System.ComponentModel.DataAnnotations;

namespace BillingSystem.Models
{
    public class Transaction
    {
        [Key]
        public System.Guid Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        public decimal TransactionAmount { get; set; }

        [MaxLength(50, ErrorMessage = "Transaction Description can only be 50 characters long")]
        public string TransactionDescription { get; set; }

        [Required]
        public int PaymentStatus { get; set; }

        public int BillNumber { get; set; }
    }
}
