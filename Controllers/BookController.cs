using BookBorrowingSystem.Api.Data;
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
        private readonly ApplicationDbContext _db;
        private readonly IBookService _bookService;
        public BookController(IBookService bookService,ApplicationDbContext db)
        {
            _bookService = bookService;
            _db = db;
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

        [HttpPost("BorrowBook")]
        public IActionResult BorrowBook([FromBody]  BookTransaction bookTransactionObj)
        {
            if(ModelState.IsValid)
            {
                bool res=_bookService.BorrowBook(bookTransactionObj);
                if (res == true)
                {
                    var response = new { message = "Book Is Borrowed" };
                    return Ok(response);
                }
            }
            ModelState.AddModelError("", "Insufficient Token");
            return BadRequest(ModelState);
        }

        [HttpGet("BooksBorrowedByUserId/{username}")]
        public IActionResult GetBooksBorrowedByUserId([FromRoute] string username)
        {
            List<Book> books=_bookService.BooksBorrowedByUserId(username);
            return Ok(books);
        }
        [HttpGet("BooksLentByUserId/{username}")]
        public IActionResult GetBooksLentByUserId([FromRoute] string username)
        {
            List<Book> books = _bookService.BooksLentByUserId(username);
            return Ok(books);
        }

        [HttpGet("Books")]
        public IActionResult GetAllBooks()
        {
            List<Book> books= _bookService.GetAllBooks();
            return Ok(books);
        }

        [HttpGet("TokensByUserId/{username}")]
        public IActionResult GetTokensByUserId([FromRoute] string username)
        {
           int tokens= _bookService.GetTokensByUserId(username);
            if (tokens == -1)
            {
                var response = new { message = "Invalid User" };
                return BadRequest(response);
            }
            
            return Ok(tokens);
        }

        [HttpGet("BookById/{id}")]
        public IActionResult BookById([FromRoute] int id)
        {
            Book book=_bookService.GetBookById(id);
            if (book.Id != 0)
            {
                return Ok(book);
            }
            var res=new { Message="Invalid Id" };
            return BadRequest(res);
        }

        [HttpPost("Test")]
        public IActionResult Test(int id)
        {
            var x=_db.Books.FirstOrDefault(obj => obj.Id == id);
            x.IsAvailable = false;
           
            _db.SaveChanges();
            return Ok(x);
        }

        [HttpPost("ReturnBook")]
        public IActionResult ReturnBook([FromBody] Book bookObj)
        {
            bool res=_bookService.ReturnBook(bookObj);
            if (res)
            {
                var response = new { Message = "Return Successs" };
                return Ok(res);
            }
            var error = new { Message = "Return Failed" };
            return BadRequest(error);
        }

        
    }
}
