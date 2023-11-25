using BookBorrowingSystem.Api.Models;
using BookBorrowingSystem.Api.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;
        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpPost("AddNewBook")]
        public IActionResult AddNewBook([FromBody] Book bookObj)
        {
            if(ModelState.IsValid)
            {
                bool res=_bookService.AddNewBook(bookObj);
                if(res)
                {
                    var response = new { message = "Book Successfully Added" };
                    return Ok(response);
                }
                
            }
            ModelState.AddModelError("", "Invalid user");
            return BadRequest(ModelState);
        }
    }
}
