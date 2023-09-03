using RestfulAPI_Project.Models.Entities.Abstract;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestfulAPI_Project.Models.Entities.Concrete
{
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        [NotMapped]
        public string Token { get; set; }
    }
}
