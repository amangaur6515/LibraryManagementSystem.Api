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

        public bool BorrowBook(BookTransaction bookTransactionObj)
        {
            if (bookTransactionObj.BookId == 0 || bookTransactionObj.BookId == null)
            {
                return false;
            }
            if( bookTransactionObj!= null)
            {
                bool res=_bookRepository.BorrowBook(bookTransactionObj);
                return res;
            }
            return false;
        } 

        public List<Book> BooksBorrowedByUserId(string username)
        {

            if (username != "" || username != null)
            {
                List<Book> booksBorrowedByUserId=_bookRepository.BooksBorrowedByUserId(username);
                return booksBorrowedByUserId;
            }
            List<Book> emptyList=new List<Book>();
            return emptyList;
            
        }

        public List<Book> BooksLentByUserId(string username)
        {
            if (username != "" || username != null)
            {
                List<Book> booksLentByUserId = _bookRepository.BooksLentByUserId(username);
                return booksLentByUserId;
            }
            List<Book> emptyList = new List<Book>();
            return emptyList;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books=_bookRepository.GetAllBooks();
            return books;
        }

        public int GetTokensByUserId(string username)
        {
            if (username != "" || username != null)
            {
                int tokens=_bookRepository.GetTokensByUserId(username);
                return tokens;
            }
            return -1;
        }

        public Book GetBookById(int id)
        {
            if(id!=0 || id != null)
            {
                Book book=_bookRepository.GetBookById(id);
                return book;
            }
            Book emptyBook=new Book();
            return emptyBook;
        }

        public bool ReturnBook(Book bookObj)
        {
            if (bookObj != null)
            {
                bool res=_bookRepository.ReturnBook(bookObj.Id);
                return res;
            }
            return false;
        }
    }
}
