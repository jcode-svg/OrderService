using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request;
using OrderService.Domain.GenericModels;
using OrderService.SharedKernel;

namespace OrderService.Domain.Aggregates.ProfileAggregate;

public class Profile : Entity<Guid>
{
    public Profile() : base(Guid.NewGuid())
    { }

    public string Username { get; set; }
    public string PasswordHash { get; set; }

    public static Profile CreateNewUser(SignUpDTO newUser)
    {
        return new Profile
        {
            Username = newUser.Username,
            PasswordHash = GeneratePasswordHash(newUser.Username, newUser.Password)
        };
    }

    public bool IsPasswordValid(string passwordProvided, out string errorMessage)
    {
        var providedPasswordHash = GeneratePasswordHash(Username, passwordProvided);

        if (providedPasswordHash == PasswordHash)
        {
            errorMessage = string.Empty;
            return true;
        }

        errorMessage = "Username or Password Incorrect";
        return false;
    }

    private static string GeneratePasswordHash(string username, string password)
    {
        var pwdBytes = SecurityModel.Hash("PWD" + username.ToLower() + password);

        return BitConverter.ToString(pwdBytes).Replace("-", "").ToLowerInvariant().Trim();
    }
}
