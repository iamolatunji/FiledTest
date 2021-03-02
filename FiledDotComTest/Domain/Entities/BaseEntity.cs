using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FiledDotComTest.Domain.Entities
{
    public class BaseEntity<TKey>
    {
        public TKey Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }
}
