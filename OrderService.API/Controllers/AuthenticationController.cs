using Microsoft.AspNetCore.Mvc;
using OrderService.Application.Contract;
using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request;
using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Response;
using OrderService.SharedKernel;
using System.Net.Mime;

namespace OrderService.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;

    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpPost("SignUp")]
    [ProducesResponseType(typeof(ResponseWrapper<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseWrapper<string>>> SignUp(SignUpDTO request)
    {
        var result = await _authenticationService.SignUp(request);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }

    [HttpPost("Login")]
    [ProducesResponseType(typeof(ResponseWrapper<LogInResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [Consumes(MediaTypeNames.Application.Json)]
    public async Task<ActionResult<ResponseWrapper<LogInResponse>>> Authenticate(LogInDTO request)
    {
        var result = await _authenticationService.SignIn(request);

        if (!result.IsSuccessful)
        {
            return BadRequest(result.Message);
        }

        return Ok(result);
    }
}