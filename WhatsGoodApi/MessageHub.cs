using Microsoft.AspNetCore.SignalR;
using WhatsGoodApi.DTOs;
using WhatsGoodApi.Services;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.Unit;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi
{
    public class MessageHub : Hub
    {
        public IUserService _userService { get; set; }
        public IMessageService _messageService { get; set; }
        public IUnitOfWork _unitOfWork { get; set; }
        public MessageHub(WhatsGoodDbContext db, IUserService userService, IMessageService messageService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _messageService = messageService;
            this._unitOfWork = unitOfWork;
        }
        public async Task JoinGroup(string groupName)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
        }

        public async Task LeaveGroup(string groupName)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);
        }
        public async Task SendMessageToUser(string userId, string username, string message)
        {
            var friend1 = await this._userService.GetUserByUsername(userId);
            var friend2 = await this._userService.GetUserByUsername(username);

            MessageDTO messagedto = new MessageDTO(friend2.ID, friend1.ID, message, DateTime.Now);
            await this._messageService.SendMessage(messagedto);
            await Clients.Group(userId).SendAsync("ReceiveMessage", username, message);
        }
    }
}
