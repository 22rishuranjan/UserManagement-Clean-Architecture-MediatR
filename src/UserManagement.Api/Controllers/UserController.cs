
using UserManagement.Application.Core.Users.Command;
using UserManagement.Application.Core.Users.Query;

namespace UserManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : BaseApiController
{
    private readonly IMediator _mediator;

    private readonly ILogger<UserController> _logger;

    public UserController(ILogger<UserController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        var userId = await _mediator.Send(command);
        return CreatedAtAction(nameof(GetUserById), new { id = userId }, null);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(int id)
    {
        return Ok();
        // Implement Get user by ID logic using MediatR query
    }

    [HttpGet]
    [ResponseCache(Duration = 60)]
    public async Task<IActionResult> GetAll()
    {
        var users = await _mediator.Send(new GetAllUsersQuery());
        return Ok(users);
    }

}

