using System.ComponentModel.DataAnnotations;

namespace Shared.DataTransferObject.IdentityModuleDto
{
    public class RegisterDto
    {
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; } = default!;
        public string Password { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string DisplayName { get; set; } = default!;
        [Phone]
        public string PhoneNumber { get; set; }
    }
}
