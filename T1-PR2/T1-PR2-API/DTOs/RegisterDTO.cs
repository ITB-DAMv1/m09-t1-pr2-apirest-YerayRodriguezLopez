using Microsoft.AspNetCore.Identity;

namespace T1_PR2_API.DTOs
{
    public class RegisterDTO
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public string Name { get; set; }
        public string Surname { get; set; }
    }
}
