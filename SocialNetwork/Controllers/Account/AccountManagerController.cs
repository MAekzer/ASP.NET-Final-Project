using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data.UoW;
using SocialNetwork.Models.Users;
using SocialNetwork.ViewModels.Account;
using SocialNetwork.Extentions;
using System.ComponentModel.DataAnnotations;
using SocialNetwork.Data.Repository;

namespace SocialNetwork.Controllers.Account
{
    public class AccountManagerController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IUnitOfWork _unitOfWork;

        public AccountManagerController(UserManager<User> userManager, SignInManager<User> signInManager, IMapper mapper, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [Route("Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            string username = model.Email;

            if (ModelState.IsValid)
            {
                if (new EmailAddressAttribute().IsValid(username))
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);

                    if (user == null)
                    {
                        ModelState.AddModelError("", "Неверный адрес электронной почты или пароль");
                        return View("/Views/Home/Index.cshtml");
                    }
                    else
                    {
                        username = user.UserName;
                    }
                }
                else if (username.Contains('@'))
                {
                    ModelState.AddModelError("", "Некорректный формат адреса электронной почты");
                    return View("/Views/Home/Index.cshtml");
                }

                var result = await _signInManager.PasswordSignInAsync(username, model.Password, model.RememberMe, false);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    ModelState.AddModelError("", "Неверный логин или пароль");
                }
            }
            return View("/Views/Home/Index.cshtml");
        }

        [Route("Logout")]
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [Route("MyPage")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> MyPage()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);

            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            var model = new UserViewModel(result);
            model.Friends = await repo.GetFriendsByUser(result);

            return View("User", model);
        }

        [Authorize]
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> EditProfile()
        {
            var user = User;
            var result = await _userManager.GetUserAsync(user);

            var viewModel = _mapper.Map<UserEditViewModel>(result);

            return View("Edit", viewModel);
        }

        [Authorize]
        [Route("Update")]
        [HttpPost]
        public async Task<IActionResult> UpdateProfile(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                user.Update(model);

                var result = await _userManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    return RedirectToAction("MyPage", "AccountManager");
                }
                else
                {
                    ModelState.AddModelError("", "Непредвиденная ошибка");
                    return View("Edit", model);
                }
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View("Edit", model);
            }
        }
    }
}
