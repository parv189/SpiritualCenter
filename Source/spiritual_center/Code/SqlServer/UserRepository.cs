using spiritual_center.Code.Interfaces;
using spiritual_center.Models;

namespace spiritual_center.Code.SqlServer
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(UserContext userContext) : base(userContext) { }
    }
}
