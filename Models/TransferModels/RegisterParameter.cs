using System.ComponentModel.DataAnnotations;

namespace rad.net.Models
{
    public class RegisterParameter
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "کلمه عبور باید بین 4 تا 50 کاراکتر باشد.")]
        public string Password { get; set; }
    }

    public class UpdatePasswordParameter
    {
        [Required]
        public string Oldpass { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "کلمه عبور باید بین 4 تا 50 کاراکتر باشد.")]
        public string Newpass { get; set; }
    }
}
