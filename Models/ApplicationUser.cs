using Microsoft.AspNetCore.Identity;

namespace BookBorrowingSystem.Api.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Name { get; set; }
    }
}
