using Microsoft.AspNetCore.Mvc;
using WhatsGoodApi.DTOs;
using WhatsGoodApi.Services;
using WhatsGoodApi.Services.IServices;
using WhatsGoodApi.WGDbContext;
using WhatsGoodApi.Models; 


namespace WhatsGoodApi.Controllers
{
    [Route("Message")]
    [ApiController]
    public class FriendRequestController : ControllerBase
    {
        private readonly WhatsGoodDbContext _db;
        public IMessageService _messageService { get; set; }
        public IFriendRequestService _friendRequestService { get; set; }
        public IFriendshipService _friendshipService { get; set; }
        public IUserService _userService { get; set; }

        public FriendRequestController(WhatsGoodDbContext db)
        {
            this._db = db;
            _messageService = new MessageService(db);
            _friendRequestService = new FriendRequestService(db);
            _friendshipService = new FriendshipService(db);
            _userService = new UserService(db);
        }

        [Route("SendFriendRequest/{username}/{friendUsername}")]
        [HttpPost]
        public async Task<IActionResult> SendFriendRequest(string username, string friendUsername)
        {
            try
            {
                var friend1 = await this._userService.GetUserByUsername(username);
                var friend2 = await this._userService.GetUserByUsername(friendUsername);
                if (friend1 != null && friend2 != null)
                {

                    FriendRequestDTO request = new FriendRequestDTO
                    {
                        SenderId = friend1.ID,
                        RecipientId = friend2.ID,
                        IsAccepted = false,
                        Timestamp = DateTime.Now
                    };
                    await this._friendRequestService.SendFriendRequest(request);
                    return Ok(request);
                }
                else
                    return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("AcceptFriendRequest")]
        [HttpPost]
        public async Task<IActionResult> AcceptFriendRequest(int requestId)
        {
            try
            {
                await this._friendRequestService.AcceptFriendRequest(requestId);
                await this._friendshipService.CreateFriendship(requestId);
                return Ok(requestId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("DeclineFriendRequest")]
        [HttpPost]
        public async Task<IActionResult> DeclineFriendRequest(int requestId)
        {
            try
            {
                await this._friendRequestService.DeclineFriendRequest(requestId);

                return Ok(requestId);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("GetAllFriendRequests")]
        [HttpGet]
        public async Task<IActionResult> GetAllFriendRequests(string username)
        {
            var friend1 = await this._userService.GetUserByUsername(username);
            try
            {

                List<FriendRequest> requests = await this._friendRequestService.GetAllFriendRequestsForUser(friend1.ID);
                foreach (FriendRequest req in requests)
                {
                    req.Sender = await this._userService.GetUserByUserId(req.SenderId);
                }
                return Ok(requests);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("CheckIfFriendRequestSent/{UserName}/{FriendName}")]
        [HttpGet]
        public async Task<IActionResult> CheckIfFriendRequestSent(string UserName, string FriendName)
        {
            try
            {
                bool friends = await this._friendRequestService.CheckIfFriendRequestSent(UserName, FriendName);

                return Ok(friends);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
