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
    public class MessageController : ControllerBase
    {
        private readonly WhatsGoodDbContext _db;
        public IMessageService _messageService { get; set; }
        public IUserService _userService { get; set; }

        public MessageController(WhatsGoodDbContext db, IMessageService messageService, IUserService userService)
        {
            this._db = db;
            _messageService = messageService;
            _userService = userService;
        }

        [Route("GetAllMessages")]
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetAllMessages(int senderId, int recipientId)
        {
            try
            {
                List<Message> messages = await this._messageService.GetAllMessagesForChat(senderId, recipientId);
                List<Message> messages2 = await this._messageService.GetAllMessagesForChat(recipientId, senderId);
                messages.AddRange(messages2);

                if (messages == null || messages.Count == 0)
                {
                    return NotFound(); 
                }

                return Ok(messages.OrderBy(m => m.Timestamp).ToList()); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [Route("SendMessage")]
        [HttpPost]
        public async Task<IActionResult> SendMessage(string senderUsername, string recipientUsername, string content)
        {
            var friend1 = await this._userService.GetUserByUsername(senderUsername);
            var friend2 = await this._userService.GetUserByUsername(recipientUsername);

            MessageDTO message = new(friend1.ID, friend2.ID, content, DateTime.Now);
            try
            {
                await this._messageService.SendMessage(message);
                return Ok(message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
