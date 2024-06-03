using HabbitsApi.Data;
using Microsoft.AspNetCore.Mvc;

namespace HabbitsApi.Controllers
{
    public class UserController : ControllerBase
    {
        EleniContext _context;
        public UserController(EleniContext eleniContext)
        {
            _context = eleniContext;
        }
        // GET: habbits/Details/5
        [HttpGet]
        [Route("/getUser")]
        public ActionResult GetUser(string UserName)
        {
            var userAccount =
                _context.UserAccounts
                .Where(x => x.UserName == UserName).FirstOrDefault();

            return Ok(userAccount);
        }

    }
}
