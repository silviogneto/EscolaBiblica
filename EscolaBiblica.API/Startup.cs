using System.Text;
using EscolaBiblica.API.Configuracoes;
using EscolaBiblica.API.DAL;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace EscolaBiblica.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        public void ConfigureServices(IServiceCollection services)
        {
#if CONFIG_IIS
            services.Configure<IISOptions>(options => options.ForwardClientCertificate = false);
#endif

            services.AddMvc()
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                    .AddJsonOptions(options => options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore);

            #region Configuração JWT

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<ConfiguracoesApp>(appSettingsSection);

            var appSettings = appSettingsSection.Get<ConfiguracoesApp>();
            var chave = Encoding.ASCII.GetBytes(appSettings.ChaveSecreta);
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                    {
                        options.RequireHttpsMetadata = false;
                        options.SaveToken = true;
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(chave),
                            ValidateIssuer = false,
                            ValidateAudience = false
                        };
                    });

            #endregion

            services.AddDbContext<EscolaBiblicaContext>(options => options.UseSqlServer(Configuration.GetConnectionString("EscolaBiblicaDatabase")));
            services.AddScoped<IUnidadeTrabalho, UnidadeTrabalho>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
