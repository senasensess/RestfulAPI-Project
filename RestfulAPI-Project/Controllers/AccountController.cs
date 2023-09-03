using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RestfulAPI_Project.Infrastructure.Repositories.Interfaces;
using RestfulAPI_Project.Models.DTO_s.AuthDTO;
using RestfulAPI_Project.Models.Entities.Concrete;

namespace RestfulAPI_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IMapper _mapper;

        public AccountController(IAuthRepository authRepository, IMapper mapper)
        {
            _authRepository = authRepository;
            _mapper = mapper;
        }

        [HttpPost]
        public IActionResult Authenticate([FromBody] AuthenticationDTO model)
        {
            var appUser = _mapper.Map<AppUser>(model);

            var user = _authRepository.Authentication(appUser.UserName, appUser.Password);

            if (user == null)
            {
                return BadRequest("Kullanıcı adı veya şifre yanlış!!");
            }

            return Ok(user);
        }
    }
}
