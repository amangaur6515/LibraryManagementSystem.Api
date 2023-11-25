using BookBorrowingSystem.Api.DAO.Abstract;
using BookBorrowingSystem.Api.Data;
using BookBorrowingSystem.Api.Models;

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
    }
}
