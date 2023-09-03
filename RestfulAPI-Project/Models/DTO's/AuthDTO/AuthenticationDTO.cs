using System.ComponentModel.DataAnnotations;

namespace RestfulAPI_Project.Models.DTO_s.AuthDTO
{
    public class AuthenticationDTO
    {
        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        public string UserName { get; set; }


        [Required(ErrorMessage = "Bu alan zorunludur!!")]
        public string Password { get; set; }
    }
}
