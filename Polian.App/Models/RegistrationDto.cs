using System.ComponentModel.DataAnnotations;

namespace Polian.App.Models
{
    public class RegistrationDto
    {
        [Required(ErrorMessage = "Password is mandatory")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Name is mandatory")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Email is mandatory")]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }


        [Required(ErrorMessage = "Role is mandatory")]
        public string? RoleName { get; set; }
    }
}
