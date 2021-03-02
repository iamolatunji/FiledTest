using FiledDotComTest.Persistence.DbConnection;
using FiledDotComTest.Domain.Entities;

namespace FiledDotComTest.Persistence.Repository
{
    public class PaymentStateRepository : BaseRepository<PaymentState, FiledDbContext>
    {
        public PaymentStateRepository(FiledDbContext context) : base(context)
        {
        }

        // We can extend the BaseRepository here...
        
    }
}
