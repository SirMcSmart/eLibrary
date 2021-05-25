using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using eLibraryPortal.Core.Const;
using eLibraryPortal.Core.Helper;
using eLibraryPortal.Core.Interface;
using eLibraryPortal.Data.Enums;
using eLibraryPortal.Data.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace eLibraryPortal.Controllers
{
    public class AuthController : Controller
    {
        private readonly IConfiguration _config;
        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;
        private readonly IRepository<Users> _userRepo;

        public AuthController(IConfiguration config, UserManager<Users> userManager, SignInManager<Users> signInManager, IRepository<Users> userRepo)
        {
            _config = config;
            _userManager = userManager;
            _signInManager = signInManager;
            _userRepo = userRepo;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(string Username, string Password, bool RememberMe)
        {
            try
            {
                var user = _userRepo.Find(x => x.UserName == Username);
                if(user == null)
                {
                    var showMessage = new AlertMessage
                    {
                        Title = "FAILED USER LOGIN",
                        Message = "Declined - Invalid username or password",
                        MessageType = MessageType.ErrorMessage
                    };
                    Message = JsonConvert.SerializeObject(showMessage);
                    return RedirectToAction("Index","Home");
                }
                else
                {
                    var pwdSignInResult = await _signInManager.PasswordSignInAsync(Username , Password, isPersistent: RememberMe,lockoutOnFailure: false);

                    if (!pwdSignInResult.Succeeded)
                    {
                        var showMessage = new AlertMessage
                        {
                            Title = "FAILED USER LOGIN",
                            Message = "Declined - Invalid username or password",
                            MessageType = MessageType.ErrorMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                        var options = new Microsoft.AspNetCore.Http.CookieOptions()
                        {
                            Expires = DateTime.Now.AddDays(1),
                            Path = "/",
                            IsEssential = true
                        };

                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_EMAIL, user.Email, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_FULLNAME, user.FullName, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_FIRSTNAME, user.FirstName, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_LASTNAME, user.LastName, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_GENDER, user.Gender, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_ADDRESS, user.Address, options);
                        Response.Cookies.Append(Constants.COOKIE_KEY_FOR_PHONENUMBER, user.PhoneNumber, options);

                        var eLibraryClaims = new List<Claim>()
                        {
                            new Claim("Name", Username),
                            new Claim("Name", Password)                         

                        };

                        //create identity
                        ClaimsIdentity identity = new ClaimsIdentity(eLibraryClaims, "cookie");
                        //create principal
                        ClaimsPrincipal principal = new ClaimsPrincipal(identity);
                        //sign-in
                        await HttpContext.SignInAsync(
                            scheme: "CookieAuth",
                            principal: principal);

                        var showMessage = new AlertMessage
                        {
                            Title = "USER LOGIN",
                            Message = "Welcome " + user.FirstName + " Signed in successfully",
                            MessageType = MessageType.SuccessMessage
                        };
                        Message = JsonConvert.SerializeObject(showMessage);
                        return RedirectToAction("BookList", "Admin");


                    }
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IActionResult> SignOut()
        {
            try
            {
                await _signInManager.SignOutAsync();
                await HttpContext.SignOutAsync();

                var options = new Microsoft.AspNetCore.Http.CookieOptions()
                {
                    Expires = DateTime.Now.AddDays(-30),
                    Path = "/",
                    IsEssential = true
                };

                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_EMAIL, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_FULLNAME, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_FIRSTNAME, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_LASTNAME, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_GENDER, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_ADDRESS, string.Empty, options);
                Response.Cookies.Append(Constants.COOKIE_KEY_FOR_PHONENUMBER, string.Empty, options);

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public AlertMessage showMessage { get; set; }
        public object Message
        {
            get
            {
                return TempData["swMESSAGE"];
            }
            set
            {
                TempData["swMESSAGE"] = value;
            }

        }
    }
}