using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApiDesafioFinal.Data;
using WebApiDesafioFinal.Settings;
using WebAPIVS2019;
using System.Text.Json.Serialization;
using WebApiDesafioFinal.Contratos;
using WebApiDesafioFinal.Services;

namespace WebApiDesafioFinal
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApiContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DesafioSqlServer"));
            });

            services.AddScoped<IGeralPersist, GeralPersist>();
            services.AddScoped<ICursoPersist, CursoPersist>();
            services.AddScoped<ICursoService, CursoService>();

            var configuracoesSection = Configuration.GetSection("ConfiguracoesJWT");
            var configuracoesJWT = configuracoesSection.Get<ConfiguracoesJWT>();

            services.Configure<ConfiguracoesJWT>(configuracoesSection);

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            })
                .AddJwtBearer(opcoes =>
                {
                    opcoes.TokenValidationParameters = new TokenValidationParameters
                    {
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuracoesJWT.Segredo)),
                        ValidAudience = "https://localhost:5001",
                        ValidIssuer = "DesafioFinal2022",
                    };
                });

            services.AddControllers().AddJsonOptions(x =>
            {
                x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddSwaggerExtension();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDesafioFinal v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
