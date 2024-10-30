using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using NDISBudget.Areas.Company.Data;
using NDISBudget.Services;
using System.Security.Claims;
using NDISBudget.Model;
using NDIS.BAL.Interfaces;
using NDIS.Entities;

namespace NDISBudget.Areas.Company.Controllers
{
    [Area("Company")]
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IUnitOfWork unitOfWork;
        public AccountController(ILogger<AccountController> logger, IUnitOfWork context)
        {
            _logger = logger;
            unitOfWork = context;
        }
        public IActionResult Login()
        {
            LoginModel objLoginModel = new LoginModel();
            return View(objLoginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginModel objLoginModel)
        {
        if (ModelState.IsValid)
            {
                CompanyEntity d =new CompanyEntity();
                d.UserName = objLoginModel.UserName;
                d.Password = objLoginModel.Password;
                var user = await unitOfWork.Companies.CheckCompanyLogin(d);
                if (user == null)
                {
                    //Add logic here to display some message to user    
                    ViewBag.Message = "Invalid Credential";
                    return View(objLoginModel);
                }
                //else
                {
                    //A claim is a statement about a subject by an issuer and    
                    //represent attributes of the subject that are useful in the context of authentication and authorization operations.    
                    var claims = new List<Claim>() {
                    new Claim(ClaimTypes.NameIdentifier, Convert.ToString(user.CompanyId)),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Role, "Company"),
                        new Claim("FavoriteDrink", "Tea")
                };
                    //Initialize a new instance of the ClaimsIdentity with the claims and authentication scheme    
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    //Initialize a new instance of the ClaimsPrincipal with ClaimsIdentity    
                    var principal = new ClaimsPrincipal(identity);
                    //SignInAsync is a Extension method for Sign in a principal for the specified scheme.    
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = true
                    }); ;

                    return RedirectToAction("Index", "Home", new { area= "Company" }); 
                }
            }
            return View(objLoginModel);
        }

        public async Task<IActionResult> Logout()
        {
            // Sign out the user
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirect to a page after logout if needed
            return RedirectToAction("Login", "Account");
        }


    }
}
