using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MonitoramentoSaudeAPI
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
            // Configuração do banco de dados
            services.AddDbContext<MonitoramentoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // Configuração dos controladores da API
            services.AddControllers();

            // Configuração da documentação da API (opcional)
            services.AddSwaggerGen();

            // Adicionar outros serviços necessários
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                // Habilitar a documentação da API em ambiente de desenvolvimento (opcional)
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MonitoramentoSaudeAPI v1"));
            }
            else
            {
                // Configurações de produção
                // app.UseExceptionHandler("/Error");
                // app.UseHsts();
            }

            // Configuração do middleware para roteamento
            app.UseRouting();

            // Configuração do middleware para autorização (opcional)
            // app.UseAuthorization();

            // Configuração do middleware para endpoints da API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

