using HabbitsApi.Data;
using HabbitsApi.Migrations;
using Microsoft.AspNetCore.Mvc;
using static HealthyHabbitsWeb.Components.Pages.Account.Signup;

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
        [HttpGet]
        [Route("/getUserByEmail")]
        public ActionResult GetUserByEmail(string Email)
        {
            var userAccount =
                _context.UserAccounts
                .Where(x => x.Email == Email).FirstOrDefault();

            return Ok(userAccount);
        }
        [HttpGet]
        [Route("/emailExists")]
        public ActionResult EmailExists(string email)
        {
            var userExists = _context.UserAccounts.Any(usr => usr.Email == email);
            var response = new EmailVerificationResponse { IsValid = userExists };
          return Ok(response);
         }

        [HttpPost]
        [Route("/signUp")]
        //[ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUp([FromBody] HealthyHabbitsWeb.Data.UserAccount usserac)
        {
            if (usserac == null)
                return BadRequest();

            try
            {
                _context.UserAccounts.Add(usserac);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPost]
        [Route("/EmailConfirmed")]
        public async Task<ActionResult> UpdateUserEmailToConfirmed([FromBody] HealthyHabbitsWeb.Data.UserAccount usserac)
        {
            if (usserac == null)
                return BadRequest();

            try
            {
                _context.UserAccounts.Update(usserac);
                await _context.SaveChangesAsync();

            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
        //signUp
    }
}
