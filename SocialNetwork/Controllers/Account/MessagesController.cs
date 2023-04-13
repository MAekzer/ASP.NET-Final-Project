using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialNetwork.Data.Repository;
using SocialNetwork.Data.UoW;
using SocialNetwork.Models.Users;
using SocialNetwork.ViewModels.Account;

namespace SocialNetwork.Controllers.Account
{
    public class MessagesController : Controller
    {
        private IUnitOfWork _unitOfWork;
        private UserManager<User> _userManager;

        public MessagesController(IMapper mapper, UserManager<User> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _unitOfWork = unitOfWork;
        }

        [Authorize]
        [Route("Chat")]
        [HttpPost]
        public async Task<IActionResult> Chat(string id)
        {
            var repo = _unitOfWork.GetRepository<Message>() as MessagesRepository;

            var user = await _userManager.GetUserAsync(User);
            var other = await _userManager.FindByIdAsync(id);


            var messages = await repo.GetChat(user.Id, other.Id);

            ChatViewModel model = new()
            {
                Current = user,
                Other = other,
                Messages = messages
            };

            return View("Chat", model);
        }

        [Authorize]
        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> SendMessage(string id, ChatViewModel model)
        {
            var repo = _unitOfWork.GetRepository<Message>() as MessagesRepository;
            var user = await _userManager.GetUserAsync(User);
            var other = await _userManager.FindByIdAsync(id);

            model.Current = user;
            model.Other = other;

            await repo.Send(user, other, model.NewMessage);

            model.Messages = await repo.GetChat(user.Id, id);

            model.NewMessage = String.Empty;

            return View("Chat", model);
        }
    }
}
