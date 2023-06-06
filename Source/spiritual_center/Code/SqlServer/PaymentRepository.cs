using spiritual_center.Code.Interfaces;
using spiritual_center.Models;

namespace spiritual_center.Code.SqlServer
{
    public class PaymentRepository : GenericRepository<Payments>,IPaymentRepository
    {
        public PaymentRepository(UserContext userContext) : base(userContext)
        {

        }
    }
}
