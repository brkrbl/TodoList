using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Dto
{
    public class TokenDto
    {
        public string token { get; set; }
        public int userId { get; set; }
        public string userName { get; set; }
        public TokenDto(string u_token,int u_userId)
        {
            token = u_token;
            userId = u_userId;
        }
        
    }
}
