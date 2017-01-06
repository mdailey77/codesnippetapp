using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CodeSnippetApplication.Startup))]
namespace CodeSnippetApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
