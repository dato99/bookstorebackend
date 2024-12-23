using BookStore.Models;
using BookStore.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : MainController
    {
        IPKG_DS_BOOK_ORDER package;


        public UserController(IPKG_DS_BOOK_ORDER package)
        {
            this.package = package;

        }

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            try
            {

                package.add_user(user);

                return Ok("User added successfully!");
            }
            catch (Exception ex)
            {
              
                return StatusCode(StatusCodes.Status500InternalServerError, "System error. Try again.");
            }


        }
    }
}
