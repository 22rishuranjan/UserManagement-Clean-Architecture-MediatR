namespace UserManagement.Api.Controllers;

public class UserController : BaseApiController
{

    private readonly ILogger<UserController> _logger;
    // in-memory data
    private static readonly List<string> Users = new List<string>
    {
    "User 1",
    "User 2",
    "User 3"
    };

    public UserController(ILogger<UserController> logger)
    {
        _logger = logger;
    }

    // GET api/products
    [HttpGet]
    public IActionResult Get()
    {
         return Ok(Users); // Return the list of products
    }

    // GET api/products/{id}
    [HttpGet("{id}")]
    public IActionResult Get(int id)
    {
        _logger.LogInformation("GET request received at UserController.");
        if (id < 0 || id >= Users.Count)
        return NotFound(); // Return 404 if the product ID is invalid

        return Ok(Users[id]); // Return a single product by ID
    }
}

