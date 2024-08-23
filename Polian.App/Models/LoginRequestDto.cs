using System.ComponentModel.DataAnnotations;

namespace Polian.App.Models
{
    public class LoginRequestDto
    {
        [Required(ErrorMessage = "UserName is mandatory")]
        public string? UserName { get; set; }

        [Required(ErrorMessage = "Password is mandatory")]
        public string? Password { get; set; }
    }
}
