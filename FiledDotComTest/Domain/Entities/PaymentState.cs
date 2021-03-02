using FiledDotComTest.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Domain.Entities
{
    public class PaymentState : BaseEntity<int>
    {
        public Payment Payment { get; set; }
        public int PaymentId { get; set; }
        public Status State { get; set; }
    }
}
