using System.IO;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

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
