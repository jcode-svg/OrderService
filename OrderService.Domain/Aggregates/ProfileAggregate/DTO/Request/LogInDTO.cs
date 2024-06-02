using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderService.Domain.Aggregates.ProfileAggregate.DTO.Request
{
    public class LogInDTO
    {
        [Required(ErrorMessage = "Username is required.")]
        [EmailAddress(ErrorMessage = "Username must be a valid email address.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; }
    }
}
