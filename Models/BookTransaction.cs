using System.ComponentModel.DataAnnotations;

namespace BookBorrowingSystem.Api.Models
{
    public class BookTransaction
    {
        [Key]
        public int Id { get; set; }
        public int BookId { get; set; }
        public string Date { get; set; }
        public string BorrowedByUserId { get; set; }
        public string LentByUserId { get; set; }

    }
}
