using System.Web.Http;
using WebActivatorEx;
using TimeKeeper.API;
using Swashbuckle.Application;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace TimeKeeper.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        
                        c.SingleApiVersion("v1", "TimeKeeper.API");
                        c.IncludeXmlComments
                        ($"{System.AppDomain.CurrentDomain.BaseDirectory}\\bin\\TimeKeeper.API.XML");                      
                    })
                .EnableSwaggerUi();
        }
    }
}
