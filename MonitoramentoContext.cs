using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;

namespace MonitoramentoSaudeAPI;

public class MonitoramentoContext : DbContext
{
    public MonitoramentoContext(DbContextOptions<MonitoramentoContext> options) : base(options)
    {
    }

    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<LeituraMonitoramento> LeiturasMonitoramento { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<LeituraMonitoramento>(entity =>
        {
            entity.HasKey(lm => lm.Id);
            entity.Property(lm => lm.Id).ValueGeneratedOnAdd();
            entity.HasOne(lm => lm.Paciente)
                .WithMany(p => p.LeiturasMonitoramento)
                .HasForeignKey(lm => lm.PacienteCpf);
            entity.Property(lm => lm.Temperatura).HasColumnType("decimal(18,2)");
        });

        modelBuilder.Entity<Paciente>(entity =>
        {
            entity.HasKey(p => p.Cpf);
        });
    }

}

