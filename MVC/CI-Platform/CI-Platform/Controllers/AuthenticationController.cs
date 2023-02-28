
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CI_Platform.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly CiPlatformContext _db;
        private IConfiguration _configuration;
        public AuthenticationController(CiPlatformContext db , IConfiguration configuration)
        {
            _db = db;
            _configuration = configuration;

        }

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }


        [HttpPost]
        public IActionResult login(User user)
        {
            var userDetails = _db.Users.FirstOrDefault(e => e.Email == user.Email);
            if (userDetails == null)
            {

                TempData["error"] = "Please Enter the correct Details";
                return View();
            }
            else
            {
                if(userDetails.Password == user.Password)
                {
                    TempData["success"] = "Hurray! Login Successfully";
                    return RedirectToAction("LandingPage" , "Mission");
                }
            }
            return View();
        }
       

        [HttpGet]
        public IActionResult forgotPassword()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult forgotPassword(User user)
        {
            var userDetails = _db.Users.FirstOrDefault(e => e.Email == user.Email);

            if(userDetails == null)
            {
                TempData["error"] = "Sorry Email not registered!!";
                return View();  
                
            }
            if (userDetails.Email == user.Email)
            {
                TempData["success"] = "Now please Make your New Password";
                return RedirectToAction("resetPassword", "Authentication");
            }

            return View();
        }


        public async Task<IActionResult> Forgotpassword(string email)
        {
            if (ModelState.IsValid)
            {
                var user = _db.Users.FirstOrDefault(u => u.Email == email);
                if (user != null)
                {
                    var token = GenerateToken(user);
                    var token2 = new JwtSecurityTokenHandler().WriteToken(token);

                    var obj = new PasswordReset()
                    {
                        Email = email,
                        Token = new JwtSecurityTokenHandler().WriteToken(token),
                        CreateAt = DateTime.Now
                    };

                    _db.PasswordResets.Add(obj);
                    _db.SaveChanges();

                    var passwordresetlink = Url.Action("resetPassword", "Authentication", new { token = token2 }, Request.Scheme);
                    TempData["link"] = passwordresetlink;

                    //UserEmailOptions userEmailOptions = new UserEmailOptions()
                    //{
                    //    Subject = "Reset Password Link",
                    //    Body = "<a href=" + passwordresetlink + ">" + passwordresetlink + "</a>"
                    //};
                    //SendEmail(email, userEmailOptions);




                    TempData["success"] = "Email has been sent to your email account";
                    return RedirectToAction("resetPassword");


                }
                else
                {
                    TempData["error"] = "Email has not been registered";
                }
            }


            return View();
        }



        // To generate token
        private JwtSecurityToken GenerateToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.Email,user.Email),
            };
            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
                _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(240),
                signingCredentials: credentials);


            return token;

        }

        public IActionResult resetPassword()
        {
            return View();
        }

       
    }
}
