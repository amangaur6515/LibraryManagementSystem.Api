﻿using BookBorrowingSystem.Api.Models;

namespace BookBorrowingSystem.Api.Services.Abstract
{
    public interface IBookService
    {
        public bool AddNewBook(Book bookObj);
        public bool BorrowBook(BookTransaction  bookTransactionObj);
        public List<Book> BooksBorrowedByUserId(string username);
        public List<Book> BooksLentByUserId(string username);
        public List<Book> GetAllBooks();

        public int GetTokensByUserId(string username);
    }
}
