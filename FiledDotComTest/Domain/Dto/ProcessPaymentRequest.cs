using FiledDotComTest.Application.Validator;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Domain.Dto
{
    public class ProcessPaymentRequest
    {
        [Required]
        [CreditCard(ErrorMessage = "Not a valid CCN.")]
        public string CreditCardNumber { get; set; }
        [Required]
        public string CardHolder { get; set; }
        [Required]
        [ExpirationDate(ErrorMessage = "Expiration date must be greater than now.")]
        public DateTime ExpirationDate { get; set; }
        [MinLength(3)]
        [MaxLength(3)]
        public string SecurityCode { get; set; }
        [Required]
        public decimal Amount { get; set; }
    }
}
