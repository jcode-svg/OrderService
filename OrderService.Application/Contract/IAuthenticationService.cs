using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request;
using OrderService.Domain.Aggregates.ProfileAggregate.DTO.Response;
using OrderService.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Application.Contract
{
    public interface IAuthenticationService
    {
        Task<ResponseWrapper<LogInResponse>> SignIn(LogInDTO request);
        Task<ResponseWrapper<string>> SignUp(SignUpDTO request);
    }
}
