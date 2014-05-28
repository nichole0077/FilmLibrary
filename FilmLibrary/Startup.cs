using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FilmLibrary.Startup))]
namespace FilmLibrary
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
