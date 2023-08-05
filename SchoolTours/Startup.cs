using Microsoft.Owin;
using Microsoft.Owin.Security.Google;
using Owin;

[assembly: OwinStartupAttribute(typeof(SchoolTours.Startup))]
namespace SchoolTours
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
            var googleOAuth2AuthenticationOptions = new GoogleOAuth2AuthenticationOptions
            {
                ClientId = "524873686285-vrpttuuvv2fuclfjq6vaelt5lu0jc1uk.apps.googleusercontent.com",
                ClientSecret = "pmEF0WaVFGZpJGm5RuogUbDy",
            };
            app.UseGoogleAuthentication(googleOAuth2AuthenticationOptions);
        }
    }
}
