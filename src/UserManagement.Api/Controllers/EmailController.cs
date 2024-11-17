using UserManagement.Application.Core.Email.Command.SendOtp;

namespace UserManagement.Api.Controllers;

public class EmailController : BaseApiController
{
    private readonly IMediator _mediator;

    private readonly ILogger<EmailController> _logger;

    public EmailController(ILogger<EmailController> logger, IMediator mediator)
    {
        _mediator = mediator;
        _logger = logger;
    }


    [HttpPost("send")]
    public async Task<IActionResult> SendOtp([FromBody] SendOtpCommand otpCommand)
    {
        await _mediator.Send(otpCommand);
        return Ok("OTP sent successfully.");
    }

    //[HttpPost("validate")]
    //public async Task<IActionResult> ValidateOtp([FromBody] SendOtpCommand otpCommand)
    //{
    //    await _mediator.Send(otpCommand);
    //    return Ok("OTP validated successfully.");
    //}
}

