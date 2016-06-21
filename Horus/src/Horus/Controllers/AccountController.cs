using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Horus.Models;
using Horus.ViewModels.Account;
using Horus.Repositories.Interface;

namespace Horus.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly IParentRepository _parentMethod;

        public AccountController(
            UserManager<UserApplication> userManager,
            SignInManager<UserApplication> signInManager,
            IParentRepository parentMethod)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _parentMethod = parentMethod;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody]LoginVM model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Username, model.Password, false, false);
                if (result.Succeeded)
                {
                    var user = await _userManager.FindByNameAsync(model.Username);
                    return Json(new { Id = user.Id });
                }
                if (result.IsNotAllowed)
                {
                    return Json(new { Message = "Error"});
                }
            }
            return Json(new { Message = "ModelInvalid"});
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody]RegisterVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserApplication
                {
                    Email = model.Email,
                    UserName = model.Username,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var success = await _parentMethod.Create(new Parent
                    {
                        id = user.Id,
                        Children = new List<string>(),
                        FirstName= model.FirstName,
                        LastName=model.LastName
                    });
                    if (success)
                    {
                        var loginResult = _signInManager.SignInAsync(user, false);
                        return Json(new { Id = user.Id });
                    }
                    else
                    {
                        return Json(new { Message = "Error" });
                    }
                }
                if (result.Errors != null)
                {
                    return Json(new { Message = "Error"});
                }
            }
            return Json(new { Mesage = "ModelInvalid" });
        }
    } 
}
