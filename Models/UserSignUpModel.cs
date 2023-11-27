using System.ComponentModel.DataAnnotations;

namespace BookBorrowingSystem.Api.Models
{
    public class UserSignUpModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
