using Microsoft.AspNetCore.Mvc;
using WhatsGoodApi.WGDbContext;

namespace WhatsGoodApi.Controllers;

[ApiController]
[Route("[user]")]
public class UserController : ControllerBase
{
    private readonly WhatsGoodDbContext _db;

    public UserController(WhatsGoodDbContext db)
    {
        this._db = db;
    }

    
}
