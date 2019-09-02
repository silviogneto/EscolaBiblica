using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace EscolaBiblica.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args)
        {
            var webHostBuilder = WebHost.CreateDefaultBuilder(args);

            var configuration = new ConfigurationBuilder()
                .AddCommandLine(args)
                .Build();

            var hostUrl = configuration["hosturl"];
            if (string.IsNullOrWhiteSpace(hostUrl))
                hostUrl = "http://0.0.0.0:5000";

            webHostBuilder.UseKestrel()
                          .UseUrls(hostUrl)
                          .UseContentRoot(Directory.GetCurrentDirectory())
                          .UseIISIntegration()
                          .UseConfiguration(configuration);

#if CONFIG_IIS
            // Utilizado para subir no IIS
            webHostBuilder = webHostBuilder.UseKestrel()
                                           .UseContentRoot(Directory.GetCurrentDirectory())
                                           .UseIISIntegration();
#endif

            return webHostBuilder.UseStartup<Startup>();
        }
    }
}
