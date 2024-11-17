
namespace UserManagement.Domain.Entities;

public class Otp
{
    public string Email { get; set; }
    public string Code { get; set; }
    public DateTime ExpiryTime { get; set; }
    public bool IsValid(string code)
    {
        return Code == code && DateTime.UtcNow <= ExpiryTime;
    }
}
