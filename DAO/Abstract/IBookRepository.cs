using BookBorrowingSystem.Api.Models;

namespace BookBorrowingSystem.Api.DAO.Abstract
{
    public interface IBookRepository
    {
        public bool AddNewBook(Book bookObj);
    }
}
