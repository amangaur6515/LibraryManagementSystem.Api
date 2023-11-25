﻿using BookBorrowingSystem.Api.Data;
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
            ModelState.AddModelError("", "Invalid user");
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

        [HttpPost("Test")]
        public IActionResult Test(int id)
        {
            var x=_db.UserProfiles.FirstOrDefault(obj => obj.Id == id);
            x.TokensAvailable += 1;
            x.BooksBorrowed -= 1;
            _db.SaveChanges();
            return Ok(x);
        }
        
    }
}