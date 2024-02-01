using Microsoft.AspNetCore.Mvc;
using WhatsGoodApi.DTOs;
using WhatsGoodApi.Services;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.WGDbContext;
using WhatsGoodApi.Models; 


namespace WhatsGoodApi.Controllers
{
    [Route("Friendship")]
    [ApiController]
    public class FriendhipController : ControllerBase
    {
        private readonly WhatsGoodDbContext _db;
        public IMessageService _messageService { get; set; }
        public IFriendRequestService _friendRequestService { get; set; }
        public IFriendshipService _friendshipService { get; set; }
        public IUserService _userService { get; set; }

        public FriendhipController(WhatsGoodDbContext db, IMessageService messageService, IFriendRequestService friendRequestService, IFriendshipService friendshipService, IUserService userService)
        {
            this._db = db;
            _messageService = messageService;
            _friendRequestService = friendRequestService;
            _friendshipService = friendshipService;
            _userService = userService;
        }

        [Route("GetAllFriends")]
        [HttpGet]
        public async Task<IActionResult> GetAllFriends(string username)
        {
            var user = await this._userService.GetUserByUsername(username);
            try
            {
                List<User> friends = await this._friendshipService.GetAllFriendsForUser(user.ID);
                return Ok(friends);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
