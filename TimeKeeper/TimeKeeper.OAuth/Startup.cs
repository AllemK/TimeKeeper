using System;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IdentityServer3.Core.Configuration;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(TimeKeeper.OAuth.Startup))]

namespace TimeKeeper.OAuth
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=316888
            var certificate = Convert.FromBase64String(ConfigurationManager.AppSettings["SigningCertificate"]);
            var inMemo = new InMemoryManager();
            var factory = new IdentityServerServiceFactory()
                            .UseInMemoryClients(inMemo.GetClients())
                            .UseInMemoryScopes(inMemo.GetScopes())
                            .UseInMemoryUsers(inMemo.GetUsers());
            var options = new IdentityServerOptions
            {
                SigningCertificate = new X509Certificate2(certificate, "password"),
                RequireSsl = false,
                Factory = factory
            };
            app.UseIdentityServer(options);
        }
    }
}
