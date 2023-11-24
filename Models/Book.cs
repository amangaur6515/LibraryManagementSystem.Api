using System.ComponentModel.DataAnnotations;

namespace BookBorrowingSystem.Api.Models
{
    public class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Rating { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Genre { get; set; }

        [Required]
        public Boolean IsAvailable { get; set; }
        [Required]
        public string  Description { get; set; }
        [Required]
        public string  LentByUserId { get; set;}
        [Required]
        public string BorrowedByUserId { get; set;}
    }
}
