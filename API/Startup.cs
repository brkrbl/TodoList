
using API.Token;
using Entities.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Token.API;

namespace API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddControllers();
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // Olu�turdu�umuz gizli anahtar�m�z� byte dizisi olarak al�yoruz.
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);

            //Projede farkl� authentication tipleri olabilece�i i�in varsay�lan olarak JWT ile kontrol edece�imizin bilgisini kaydediyoruz.
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                //JWT kullanaca��m ve ayarlar� da �unlar olsun dedi�imiz yer ise buras�d�r.
                .AddJwtBearer(x =>
                {
                    //Gelen isteklerin sadece HTTPS yani SSL sertifikas� olanlar� kabul etmesi(varsay�lan true)
                    x.RequireHttpsMetadata = false;
                    //E�er token onaylanm�� ise sunucu taraf�nda kay�t edilir.
                    x.SaveToken = true;
                    //Token i�inde neleri kontrol edece�imizin ayarlar�.
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        //Token 3.k�s�m(imza) kontrol�
                        ValidateIssuerSigningKey = true,
                        //Neyle kontrol etmesi gerektigi
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        //Bu iki ayar ise "aud" ve "iss" claimlerini kontrol edelim mi diye soruyor
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });
            //DI i�in IUserService aray�z�n� �a��rd���m zaman UserService s�n�f�n� getirmesini s�yl�yorum.
            services.AddScoped<ITokenServices, TokenServices>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
