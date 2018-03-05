using System;
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
            var options = new IdentityServerOptions
            {
                SigningCertificate = X509Certificate(certificate);
            };
            app.UseIdentityServer();
        }
    }
}
