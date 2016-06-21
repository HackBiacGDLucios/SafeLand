using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;

using Horus.Models;
using Horus.Repositories.Interface;
using Horus.ViewModels.Account;
using Horus.ViewModels.Parents;
using System.Collections.Concurrent;

namespace Horus.Controllers
{
    public class ParentController : Controller
    {
        private readonly UserManager<UserApplication> _userManager;
        private readonly SignInManager<UserApplication> _signInManager;
        private readonly IParentRepository _parentMethods;
        private readonly IChildRepository _childMethods;

        public ParentController(
            UserManager<UserApplication> userManager,
            SignInManager<UserApplication> signInManager,
            IParentRepository parentMethods,
            IChildRepository childMehtods)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _parentMethods = parentMethods;
            _childMethods = childMehtods;
        }

        [HttpPost]
        public async Task<IActionResult> AddChild([FromBody]ChildVM model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserApplication
                {
                    Email = model.Email,
                    UserName = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    var success = await _childMethods.Create(new Child
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        LastKnownLocation = new Point(),
                        Id=user.Id
                    });

                    if (success)
                    {
                        var parent = await _parentMethods.Get(model.ParentId);
                        if(parent.Children == null)
                        {
                            parent.Children = new List<string>();
                            parent.Children.Add(user.Id);
                        }
                        else
                        {
                            parent.Children.Add(user.Id);
                        }
                        success = await _parentMethods.Update(parent);
                        return Json(new { Id = user.Id });
                    }
                    return Json(new { Message = "ErrorCreatingOnDocument" });
                }
                return Json(new { Message = "ErrorCreatingSql" });
            }
            return Json("ModelInvalid");
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromRoute]string Id)
        {
            var child = await _childMethods.Get(Id);
            return Json(child);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute]string Id)
        {
            var parent = await _parentMethods.Get(Id);
            var childs = new ConcurrentBag<Child>();

            Parallel.ForEach(parent.Children, async child =>
            {
                var children = await _childMethods.Get(child);
                childs.Add(children);
            });

            return Json(childs);
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
                    return Json(new { Message = "Error" });
                }
            }
            return Json(new { Message = "ModelInvalid" });
        }
    }
}
