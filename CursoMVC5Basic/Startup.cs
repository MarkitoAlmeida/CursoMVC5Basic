using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CursoMVC5Basic.Startup))]
namespace CursoMVC5Basic
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
