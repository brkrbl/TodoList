
using API.Token;
using BL.Abstract;
using BL.Manager;
using Entities.Dto;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Entities.Helpers;

namespace Token.API
{
    public class TokenServices : ITokenServices
    {

        private readonly AppSettings _appSettings;
        private readonly IUserManager _userManager;
        public TokenServices(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
            _userManager = new UserManager();
        }

        public async Task<TokenDto> Authenticate(LoginDto loginUser)
        {
            //Kullanıcının gerçekten olup olmadığı kontrol ediyorum yoksa direk null dönüyorum.
            var user = await _userManager.GetUserUsernameAndPassword(loginUser.username, loginUser.password);

            if (user == null)
                return null;

            // Token oluşturmak için önce JwtSecurityTokenHandler sınıfından instance alıyorum.
            var tokenHandler = new JwtSecurityTokenHandler();
            //İmza için gerekli gizli anahtarımı alıyorum.
            var key = Encoding.ASCII.GetBytes(_appSettings.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                //Özel olarak şu Claimler olsun dersek buraya ekleyebiliriz.
                Subject = new ClaimsIdentity(new[]
                {
                    //İstersek string bir property istersek ClaimsTypes sınıfının sabitlerinden çağırabiliriz.
                    new Claim("userId", user.Id.ToString()),
                    new Claim(ClaimTypes.Name,user.Username)
                }),
                //Tokenın hangi tarihe kadar geçerli olacağını ayarlıyoruz.
                Expires = DateTime.UtcNow.AddMinutes(2),
                //Son olarak imza için gerekli algoritma ve gizli anahtar bilgisini belirliyoruz.
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            //Token oluşturuyoruz.
            var token = tokenHandler.CreateToken(tokenDescriptor);
            //Oluşturduğumuz tokenı string olarak bir değişkene atıyoruz.
            string generatedToken = tokenHandler.WriteToken(token);

            TokenDto obj = new TokenDto(generatedToken, user.Id);

            return obj;
        }


    }
}
