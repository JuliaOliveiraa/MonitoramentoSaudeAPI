using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Services;

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
            services.AddDbContext<MonitoramentoContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc();

            services.AddScoped<IPacienteService, PacienteService>();
            services.AddScoped<IMonitoriamentoService, MonitoriamentoService>();
            services.AddScoped<IContatoEmergenciaService, ContatoEmergenciaService>();

            services.AddControllers();
            services.AddSwaggerGen();

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
            
            app.UseRouting();
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

