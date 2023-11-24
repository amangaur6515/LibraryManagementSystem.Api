using System.ComponentModel.DataAnnotations;

namespace BookBorrowingSystem.Api.Models
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string Username { get; set; }
        public int TokensAvailable { get; set; }
        public int BooksBorrowed { get; set; }
        public int BooksLent { get; set; }


    }
}
