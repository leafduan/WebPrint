using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace WebPrint.Web.Mvc.Models
{
    public class LoginModel
    {
        [DisplayName("username")]
        [Required(ErrorMessage = "*")]
        public string Username { get; set; }

        [DisplayName("password")]
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DisplayName("remember me")]
        public bool RememberMe { get; set; }
    }
}