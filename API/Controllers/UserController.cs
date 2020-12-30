using System.Collections.Generic;
using System.Threading.Tasks;
using API.Token;
using BL.Abstract;
using BL.Manager;
using Entities;
using Entities.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserManager _userManager;
        private readonly ITokenServices _token;
        public UserController(ITokenServices token)
        {
            _userManager = new UserManager();
            _token = token;
        }

        [HttpGet]
        public async Task<List<Usertable>> Get()
        {
            return await _userManager.GetAllUsers();
        }

        [HttpGet("{id}")]
        public async Task<Usertable> Get(int id)
        {
            return await _userManager.GetUserById(id);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<Usertable> Post([FromBody]RegisterDto user)
        {
            return await _userManager.InsertUser(user);
        }

        [HttpPut]
        public async Task<Usertable> Put([FromBody] Usertable user)
        {
            return await _userManager.UpdateUser(user);
        }

        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
            await _userManager.DeleteUser(id);
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<TokenDto> Authenticate([FromBody] LoginDto loginUser)
        {
            return await _token.Authenticate(loginUser);
        }

    }
}
