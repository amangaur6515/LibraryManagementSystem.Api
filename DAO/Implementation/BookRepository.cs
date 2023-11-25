using BookBorrowingSystem.Api.DAO.Abstract;
using BookBorrowingSystem.Api.Data;
using BookBorrowingSystem.Api.Models;
using BookBorrowingSystem.Api.Services.Implementation;

namespace BookBorrowingSystem.Api.DAO.Implementation
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _db;
        public BookRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public bool AddNewBook(Book bookObj)
        {
            //add record in books table
            _db.Add(bookObj);
            
            //get email of who is adding book
            string username = bookObj.LentByUserId;
            //update userprofile row
            UserProfile userProfile=_db.UserProfiles.FirstOrDefault(obj=>obj.Username==username);
            if (userProfile!=null)
            {
                userProfile.BooksLent += 1;
                _db.SaveChanges();
                return true;
            }
            return false;
            
        }
        public bool BorrowBook(BookTransaction bookTransactionObj)
        {
            //extract bookId, borrwedBy,lentBy
            int bookId = bookTransactionObj.BookId;
            string borrowedBy = bookTransactionObj.BorrowedByUserId;
            string lentBy = bookTransactionObj.LentByUserId;

            UserProfile userProfileBorrowing = _db.UserProfiles.FirstOrDefault(obj => obj.Username == borrowedBy);
            UserProfile userProfileLenting = _db.UserProfiles.FirstOrDefault(obj => obj.Username == lentBy);

            //only when tokens are greater than one
            if (userProfileBorrowing!=null && userProfileLenting!=null && userProfileBorrowing.TokensAvailable >= 1)
            {
                Book book = _db.Books.FirstOrDefault(obj => obj.Id == bookId);
                if (book != null)
                {
                    //add record in book transaction table
                    _db.BookTransactions.Add(bookTransactionObj);
                    //update borrowedBy column of books table
                    book.BorrowedByUserId = borrowedBy;
                }

                //update userProfile books tokens available and books borrowed column

                if (userProfileBorrowing != null && userProfileLenting != null)
                {
                    userProfileBorrowing.BooksBorrowed += 1;
                    userProfileBorrowing.TokensAvailable -= 1;

                    userProfileLenting.TokensAvailable += 1;

                    _db.SaveChanges();
                    return true;
                }
            }
 
            return false;
        }

        public List<Book> BooksBorrowedByUserId(string username)
        {
            //get all his books
            List<Book> userBorrowedBooks = _db.Books.Where(book => book.BorrowedByUserId == username).ToList();
            return userBorrowedBooks;
        }

        public List<Book> BooksLentByUserId(string username)
        {
            //get all his books
            List<Book> userLentBooks = _db.Books.Where(book => book.LentByUserId == username).ToList();
            return userLentBooks;
        }

        public List<Book> GetAllBooks()
        {
            List<Book> books = _db.Books.ToList();
            return books;
        }

        public int GetTokensByUserId(string username)
        {
            //get the userprofile
            UserProfile userProfile=_db.UserProfiles.FirstOrDefault(obj=>obj.Username == username);
            if(userProfile != null)
            {
                return userProfile.TokensAvailable;
            }
            return -1;
        }
    }
}
