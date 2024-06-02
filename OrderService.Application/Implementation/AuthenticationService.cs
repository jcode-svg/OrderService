using OrderService.Application.Contract;
using OrderService.Domain.Aggregates.ProfileAggregate;
using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request;
using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Response;
using OrderService.Domain.RepositoryContracts;
using OrderService.Infrastructure.TokenGenerator;
using OrderService.SharedKernel;

namespace OrderService.Application.Implementation;

public class AuthenticationService : IAuthenticationService
{
    private readonly IProfileRepository _profileRepository;
    private readonly ITokenGenerator _tokenGenerator;

    public AuthenticationService(IProfileRepository profileRepository, ITokenGenerator tokenGenerator)
    {
        _profileRepository = profileRepository;
        _tokenGenerator = tokenGenerator;
    }

    public async Task<ResponseWrapper<string>> SignUp(SignUpDTO request)
    {
        var user = await _profileRepository.GetUserAsync(request.Username);

        if (user != null)
        {
            return ResponseWrapper<string>.Error($"User, {request.Username}, already exists");
        }

        var newUser = Profile.CreateNewUser(request);
        await _profileRepository.AddUserAsync(newUser);
        await _profileRepository.SaveChangesAsync();
        return ResponseWrapper<string>.Success("Signed up successfully");
    }

    public async Task<ResponseWrapper<LogInResponse>> SignIn(LogInDTO request)
    {
        var user = await _profileRepository.GetUserAsync(request.Username);

        if (user == null)
        {
            return ResponseWrapper<LogInResponse>.Error($"User, {request.Username}, does not exist");
        }

        if (!user.IsPasswordValid(request.Password, out string errorMessage))
        {
            return ResponseWrapper<LogInResponse>.Error(errorMessage);
        }

        var response = new LogInResponse
        {
            Token = _tokenGenerator.GenerateToken(request.Username, user.Id.ToString())
        };

        return ResponseWrapper<LogInResponse>.Success(response);
    }
}
