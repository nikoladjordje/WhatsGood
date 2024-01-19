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

        public MessageController(WhatsGoodDbContext db)
        {
            this._db = db;
            _messageService = new MessageService(db);
        }



        [Route("GetAllMessages")]
        [HttpGet]
        public async Task<ActionResult<List<Message>>> GetAllMessages(int senderId, int recipientId)
        {
            try
            {
                List<Message> messages = await this._messageService.GetAllMessagesForChat(senderId, recipientId);

                if (messages == null || messages.Count == 0)
                {
                    return NotFound(); 
                }

                return Ok(messages); 
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }

}
