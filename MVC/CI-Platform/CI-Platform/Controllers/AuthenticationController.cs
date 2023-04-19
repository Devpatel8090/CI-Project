
using CI_Platfrom.Entities.Data;
using CI_Platfrom.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MailKit.Net.Smtp;
using CI_Platform.Model;
using MimeKit;
using Microsoft.Extensions.Options;
using CI_Platfrom.Entities.Models.ViewModel;
using CI_Platfrom.Repository.Interface;

namespace CI_Platform.Controllers
{
    public class AuthenticationController : Controller
    {
        //private readonly CiPlatformContext _db;
        private IConfiguration _configuration;
        private readonly SMTPConfigModel _smtpconfig;
        private readonly IUnitOfWorkRepository _unitOfWork;
        public AuthenticationController(/*CiPlatformContext db*/IUnitOfWorkRepository unitOfWorkRepository , IConfiguration configuration , IOptions<SMTPConfigModel> smtpConfig)
        {
            //_db = db;
            _unitOfWork = unitOfWorkRepository;
            _configuration = configuration;
            _smtpconfig = smtpConfig.Value;

        }

        /// <summary>
        ///     Provides the login screen to user 
        /// </summary>
        /// <returns> empty view with only login page </returns>

        [HttpGet]
        public IActionResult login()
        {
            return View();
        }

        /// <summary>
        /// it takes the user details from the user and checks the email id and password from database .
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>

        [HttpPost]
        public IActionResult login(User user)
        {
            //var userDetails = _db.Users.FirstOrDefault(e => e.Email == user.Email);
            var userDetails = _unitOfWork.User.GetUserDetails().Where(e => e.Email == user.Email).FirstOrDefault();

            if (ModelState.IsValid)
            {
                if (userDetails == null)
                {
                    ModelState.AddModelError("email", "Email is Not Register");
                }


               
                
                else if (userDetails.Password == user.Password)
                {
                        
                        HttpContext.Session.SetString("userEmail",userDetails.Email);
                     if(userDetails.Role == "ADMIN")
                    {
                        TempData["success"] = "Hurray! Admin Logged in Successfully";
                        return RedirectToAction("UserAdminTab", "Admin");
                    }
                    else
                    {
                        TempData["success"] = "Hurray! User Logged in Successfully";
                        return RedirectToAction("LandingPage", "Mission");
                    }
                        
                }
                else
                {
                        TempData["Error"] = "Please Enter the correct Password Details";
                         ModelState.AddModelError("password", "Please Enter the correct Password Details");
                }
            }
                
            
            else
            {
                
               
                TempData["error"] = "Please Enter the Complete Details";
                


            }
            return View();
        }

        public IActionResult logout()
        {
            HttpContext.Session.Remove("userEmail");
            TempData["success"] = "Logged out Successfully ";
            return RedirectToAction("login", "Authentication");
        }

        [HttpGet]
        public IActionResult forgotPassword()
        {
            return View();
        }


        
      

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {


            if (ModelState.IsValid)
            {
                //var user = _db.Users.FirstOrDefault(u => u.Email == email);
                var user = _unitOfWork.User.GetUserDetails().Where(e => e.Email == email).FirstOrDefault();
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

                    //_db.PasswordResets.Add(obj);
                    //_db.SaveChanges();
                    _unitOfWork.PasswordReset.Add(obj);
                    _unitOfWork.save();

                    var passwordresetlink = Url.Action("resetPassword", "Authentication", new { token = token2 }, Request.Scheme);
                    TempData["link"] = passwordresetlink;

                    UserEmailOptions userEmailOptions = new UserEmailOptions()
                    {
                        Subject = "Reset Password Link",
                        Body = "<a href=" + passwordresetlink + ">" + passwordresetlink + "</a>"
                    };
                    SendEmail(email, userEmailOptions);




                    TempData["success"] = "Now Check your mail box to reset password";
                    return RedirectToAction("login","Authentication");


                }
                else
                {
                    TempData["error"] = "Sorry Email not registered!!";
                    ModelState.AddModelError("email", "Sorry Email not registered!!");
                }
            }


            return View();
        }



        public void SendEmail(string toEmail, UserEmailOptions userEmailOptions)
        {
            var email = new MimeMessage();

            email.From.Add(new MailboxAddress(_smtpconfig.SenderDisplayName, _smtpconfig.SenderAddress));
            email.To.Add(new MailboxAddress("Jay", toEmail));

            email.Subject = userEmailOptions.Subject;
            var bodyBuilder = new BodyBuilder();

            bodyBuilder.HtmlBody = userEmailOptions.Body;
            email.Body = bodyBuilder.ToMessageBody();

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.gmail.com", 465, true);

                smtp.Authenticate(_smtpconfig.UserName, _smtpconfig.Password);

                smtp.Send(email);

                smtp.Disconnect(true);
            }
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
       
        public IActionResult resetPassword(string token)
        {
            var findToken = _unitOfWork.PasswordReset.GetFirstOrDefault(x => x.Token == token);
            var tokenObject = new JwtSecurityTokenHandler().ReadJwtToken(token);
            var email = tokenObject.Payload.Claims.ToList()[0].Value;
            ViewBag.Email = new
            {
                email = email,
                token = token
            };
            return View();
        }

        [HttpPost]
        public IActionResult resetPassword(resetPassword obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.password == obj.confirmpassword)
                {
                    var user = _unitOfWork.User.GetFirstOrDefault(e => e.Email == obj.email);
                    user.Password = obj.password;
                    _unitOfWork.User.Update(user);
                    _unitOfWork.save();
                    return RedirectToAction("login", "Authentication");

                }
                else
                {
                    TempData["error"] = "Password doesn't match";
                    ModelState.AddModelError("ConfirmPassword", "password dosn't match!!");

                }
            }
            return View();

        }

     
       



    }
}
