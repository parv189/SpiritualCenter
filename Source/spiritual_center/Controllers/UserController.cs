using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using spiritual_center.Code.Interfaces;
using spiritual_center.Models;

namespace spiritual_center.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _context;
        public UserController(IUserRepository context)
        {
            _context = context;
        }
        [EnableCors("Policy1")]
        [HttpDelete("{id}")]
        public ActionResult<User> DeleteUser(string id)
        {
            try
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                var c = _context.Find(x => x.User_Id == id).FirstOrDefault();
                var deleteduser = _context.Delete(c);
                return deleteduser;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private bool UserExists(string id)
        {
            return _context.IsExist(x => x.User_Id == id);
        }
    }
}
