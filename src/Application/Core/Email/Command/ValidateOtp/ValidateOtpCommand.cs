
using UserManagement.Application.Common.Interfaces;
using UserManagement.Domain.Entities;

namespace UserManagement.Application.Core.Email.Command;

public class ValidateOtpCommand : IRequest<bool>
{
    public string Email { get; set; }
    public string Code { get; set; }
}


public class ValidateOtpCommandHandler : IRequestHandler<ValidateOtpCommand, bool>
{
    private readonly List<Otp> _otpStorage; // Simulating storage for demonstration

    public ValidateOtpCommandHandler()
    {
        _otpStorage = new List<Otp>(); // Replace with your actual database or storage
    }

    public async Task<bool> Handle(ValidateOtpCommand request, CancellationToken cancellationToken)
    {
        // Find OTP for the email (in a real application, query the database)
        var otp = _otpStorage.FirstOrDefault(o => o.Email == request.Email);

        if (otp == null || !otp.IsValid(request.Code))
        {
            return false;
        }

        // OTP is valid, optionally remove it after use
        _otpStorage.Remove(otp);

        return true;
    }
}
