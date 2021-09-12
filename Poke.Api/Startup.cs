using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Poke.Services;
using System.IO;
using Poke.Models;
using Poke.Services.ExternalServices;
using Poke.Services.Translators;
using RestSharp;

namespace Poke.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var configurationRoot = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var restClient = new RestSharp.RestClient();

            services.AddMvc();
            services.AddSingleton(configurationRoot);
            services.AddSingleton<IRestClient>(restClient);
            services.AddScoped<IConfig, Config>();
            services.AddScoped<IPokemonService, PokemonService>();
            services.AddScoped<IPokeApiClient, PokeApiClient>();
            services.AddScoped<IFunTranslationsApiClient, FunTranslationsApiClient>();

            services.AddScoped<IYodaTranslator, YodaTranslator>();
            services.AddScoped<IShakespeareTranslator, ShakespeareTranslator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
