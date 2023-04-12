using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using SocialNetwork.Data.Repository;
using SocialNetwork.Data.UoW;
using SocialNetwork.Extentions;
using SocialNetwork.Models.Users;
using SocialNetwork.ViewModels.Account;

namespace SocialNetwork.Controllers.Account
{
    public class FriendsController : Controller
    {
        private IMapper _mapper;

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        private readonly IUnitOfWork _unitOfWork;

        public FriendsController(IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> DeleteFriend([FromRoute] string id)
        {
            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);

            if (friend is not null)
            {
                await repo.DeleteFriend(user, friend);
                return StatusCode(200);
            }

            return StatusCode(400);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddFriend([FromRoute] string id)
        {
            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;

            var user = await _userManager.GetUserAsync(User);
            var friend = await _userManager.FindByIdAsync(id);
            Console.WriteLine($"Request: {id}, {friend.FirstName}, {friend.LastName}");

            if (friend is not null)
            {
                await repo.AddFriend(user, friend);
                return StatusCode(200);
            }

            Console.WriteLine("foo");

            return StatusCode(400);
        }

        [Authorize]
        [HttpGet]
        [Route("UserSearch")]
        public async Task<IActionResult> Search(SearchViewModel model)
        {
            model ??= new SearchViewModel();

            var repo = _unitOfWork.GetRepository<Friend>() as FriendRepository;
            var user = await _userManager.GetUserAsync(User);

            var friends = await repo.GetFriendsByUser(user);
            model.Friends = friends;

            return View("UserSearch", model);
        }

        [Authorize]
        [HttpPost]
        [Route("UserSearchResult")]
        public async Task<IActionResult> SearchResult(SearchViewModel model)
        {
            User user;

            if (!ModelState.IsValid)
            {
                return View("UserSearch");
            }

            switch ((int)model.SearchParam)
            {
                case 0:
                    user = await _userManager.FindByNameAsync(model.Value);

                    if (user != null)
                        model.Result.Add(user);

                    break;

                case 1:
                    user = await _userManager.FindByEmailAsync(model.Value);

                    if (user != null)
                        model.Result.Add(user);

                    break;

                case 2:
                    model.Result = await _userManager.FindByFullNameAsync(model.Value);
                    break;

                case 3:
                    model.Result = await _userManager.FindByStatusAsync(model.Value);
                    break;

                case 4:
                    model.Result = await _userManager.FindByAboutAsync(model.Value);
                    break;

                default:
                    return View("UserSearch");
            }

            return View("UserSearch", model);
        }
    }
}
