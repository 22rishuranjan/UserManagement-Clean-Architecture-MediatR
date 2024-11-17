using System.Security.Cryptography.X509Certificates;
using UserManagement.Application.Common.Interfaces;

namespace UserManagement.Application.Core.Email.Command.SendOtp;

public class SendOtpCommand : IRequest
{
    public string Email { get; set; }
    public string Name { get; set; }
}


public class SendOtpCommandHandler : IRequestHandler<SendOtpCommand>
{
    private readonly IEmailSender _emailSender;
    private const string SUBJECT = "OTP for User Management APP";
    private const string BODY = "Your OTP code is {0}. It is valid for 10 minutes.";

    public SendOtpCommandHandler(IEmailSender emailSender)
    {
        _emailSender = emailSender;
    }

    public async Task Handle(SendOtpCommand request, CancellationToken cancellationToken)
    {
        // Generate a random OTP
        var otpCode = new Random().Next(100000, 999999).ToString();
        var expiryTime = DateTime.UtcNow.AddMinutes(10);

        // Send OTP via email
        await _emailSender.SendEmailAsync(request.Email, SUBJECT, string.Format(BODY, otpCode));

        // Log or store the OTP (in a real application, save it to the database)
        Console.WriteLine($"OTP {otpCode} sent to {request.Email}");

    }

}
