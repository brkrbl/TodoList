using Entities.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Token
{
    public interface ITokenServices
    {
        public Task<TokenDto> Authenticate(LoginDto loginUser);
    }
}
