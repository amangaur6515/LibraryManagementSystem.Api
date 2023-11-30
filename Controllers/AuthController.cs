using BookBorrowingSystem.Api.Data;
using BookBorrowingSystem.Api.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookBorrowingSystem.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ApplicationDbContext _db;
        public AuthController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,ApplicationDbContext db)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _db = db;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody] UserSignUpModel obj)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser() { UserName = obj.Username, Email = obj.Username,Name=obj.Name };
                var result = await _userManager.CreateAsync(user, obj.Password); //bool result, identity result
                //if user created successfully
                if (result.Succeeded)
                {
                    //add record in userProfile table
                    var userProfile = new UserProfile()
                    {
                        Username = obj.Username,
                        TokensAvailable = 10,
                        BooksBorrowed = 0,
                        BooksLent = 0
                    };
                    _db.UserProfiles.Add(userProfile);
                    _db.SaveChanges();
                    //await _signInManager.CreateAsync(user, isPersistent: false);//session key using
                    var successResponse = new { message = "Signup success" };
                    return Ok(successResponse);
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            ModelState.AddModelError("", "invalid");
            return BadRequest(ModelState);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LogIn([FromBody] UserSignInModel signInModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(signInModel.Username, signInModel.Password, false, false);
                if (result.Succeeded)
                {
                    //get the available tokens from userProfiles table
                    UserProfile userProfile=_db.UserProfiles.FirstOrDefault(obj => obj.Username == signInModel.Username);
                    int tokensAvaialable = userProfile.TokensAvailable;
                    var response = new { Username = signInModel.Username };
                    return Ok(response);

                }

            }
            ModelState.AddModelError("", "Invalid Credentials");
            return BadRequest(ModelState);


        }

        [HttpPost("Logout")]

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            var res = new { Message = "Logout success" };
            return Ok(res);
        }
    }
}
