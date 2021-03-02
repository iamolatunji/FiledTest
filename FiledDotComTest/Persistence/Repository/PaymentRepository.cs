using FiledDotComTest.Persistence.DbConnection;
using FiledDotComTest.Domain.Entities;

namespace FiledDotComTest.Persistence.Repository
{
    public class PaymentRepository : BaseRepository<Payment, FiledDbContext>
    {
        public PaymentRepository(FiledDbContext context) : base(context)
        {
        }

        // We can extend the BaseRepository here...
        
    }
}
