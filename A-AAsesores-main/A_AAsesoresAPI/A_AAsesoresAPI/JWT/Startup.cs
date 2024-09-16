using System;
using System.Text;
using Microsoft.Owin;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security;
using Microsoft.IdentityModel.Tokens;
using Owin;
using System.Web.Http;
using System.Configuration;

[assembly: OwinStartup(typeof(A_AAsesoresAPI.Startup))]
namespace A_AAsesoresAPI
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Crear una instancia de HttpConfiguration
            var config = new HttpConfiguration();

            // Configurar la autenticación JWT
            ConfigureJwtAuth(app);

            // Configurar rutas de Web API
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Usar la configuración de Web API
            app.UseWebApi(config);
        }

        private void ConfigureJwtAuth(IAppBuilder app)
        {
            var secret = Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["SecretKey"]);

            app.UseJwtBearerAuthentication(new JwtBearerAuthenticationOptions
            {
                AuthenticationMode = AuthenticationMode.Active,
                TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                }
            });
        }
    }
}