using BookBorrowingSystem.Api.Models;

namespace BookBorrowingSystem.Api.DAO.Abstract
{
    public interface IBookRepository
    {
        public bool AddNewBook(Book bookObj);
        public bool BorrowBook(BookTransaction bookTransactionObj);
        public List<Book> BooksBorrowedByUserId(string username);
        public List<Book> BooksLentByUserId(string username);
        public List<Book> GetAllBooks();

        public int GetTokensByUserId(string username);
        public Book GetBookById(int id);
        public bool ReturnBook(int id);
    }
}
