using BookBorrowingSystem.Api.DAO.Abstract;
using BookBorrowingSystem.Api.Models;
using BookBorrowingSystem.Api.Services.Abstract;

namespace BookBorrowingSystem.Api.Services.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;  
        }
        public bool AddNewBook(Book bookObj)
        {
            
            if(bookObj!= null)
            {
                bool res=_bookRepository.AddNewBook(bookObj);
                if (res == true)
                {
                    return true;
                }
                else return false;
                
            }
            return false;

        }
    }
}
