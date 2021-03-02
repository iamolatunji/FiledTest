using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Domain.Entities
{
    public class Payment : BaseEntity<int>
    {
        public string CreditCardNumber { get; set; }
        public string CardHolder { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string SecurityCode { get; set; }
        public decimal Amount { get; set; }
        public virtual PaymentState PaymentState { get; set; }
        public int PaymentStateId { get; set; }
    }
}
