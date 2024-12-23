using BookStore.Models;
using BookStore.Packages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Oracle.ManagedDataAccess.Client;

namespace BookStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : MainController
    {
        IPKG_DS_BOOKS package;


        public BookController(IPKG_DS_BOOKS package)
        {
            this.package = package;
      
        }

        [HttpGet]
        public IActionResult GetBooks()
        {
            List<Book> books = new List<Book>();
            try
            {
                books = package.get_books();
            }
            catch (Exception ex)

            { 
                return StatusCode(StatusCodes.Status500InternalServerError, "System error, Try Again.");
            }
            return Ok(books);
        }
        [HttpPost]
        public IActionResult AddUser(Book book)
        {
            try {  
           
                package.add_book(book);

                return Ok("Book added successfully!");
            }
            catch (Exception ex)
            {
            
                return StatusCode(StatusCodes.Status500InternalServerError, "System error. Try again.");
            }


        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] Book book)
        {
            try
            {
             
                package.update_book(book); 

                return Ok("User updated successfully.");
            }
            catch (OracleException ex)
            {
                
                Console.WriteLine($"Oracle error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, $"Database error: {ex.Message}");
            }
        }

        [HttpDelete]
        public IActionResult DeleteUser(Book book)
        {
            try
            {
                package.delete_book(book);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "System error. Try again.");
            }
            return Ok();
        }



    }
}
