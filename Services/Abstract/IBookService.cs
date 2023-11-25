using BookBorrowingSystem.Api.Models;

namespace BookBorrowingSystem.Api.Services.Abstract
{
    public interface IBookService
    {
        public bool AddNewBook(Book bookObj);
    }
}
