﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MonitoramentoSaudeAPI;

#nullable disable

namespace MonitoramentoSaudeAPI.Migrations
{
    [DbContext(typeof(MonitoramentoContext))]
    [Migration("20230709022444_NewMigration")]
    partial class NewMigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MonitoramentoSaudeAPI.Models.LeituraMonitoramento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("BatimentosCardiacos")
                        .HasColumnType("int");

                    b.Property<DateTime>("DataHora")
                        .HasColumnType("datetime2");

                    b.Property<int>("FrequenciaRespiratoria")
                        .HasColumnType("int");

                    b.Property<int>("NivelCO2")
                        .HasColumnType("int");

                    b.Property<string>("PacienteCpf")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("PressaoArterial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("SaturacaoOxigenio")
                        .HasColumnType("int");

                    b.Property<decimal>("Temperatura")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("Id");

                    b.HasIndex("PacienteCpf");

                    b.ToTable("LeiturasMonitoramento");
                });

            modelBuilder.Entity("MonitoramentoSaudeAPI.Models.Paciente", b =>
                {
                    b.Property<string>("Cpf")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Alergias")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HistoricoMedico")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("MedicamentosEmUso")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Observacoes")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Cpf");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("MonitoramentoSaudeAPI.Models.LeituraMonitoramento", b =>
                {
                    b.HasOne("MonitoramentoSaudeAPI.Models.Paciente", "Paciente")
                        .WithMany("LeiturasMonitoramento")
                        .HasForeignKey("PacienteCpf")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("MonitoramentoSaudeAPI.Models.Paciente", b =>
                {
                    b.Navigation("LeiturasMonitoramento");
                });
#pragma warning restore 612, 618
        }
    }
}
