﻿using System;
using System.Globalization;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using CsvHelper;

namespace MonitoramentoSaudeAPI.Services
{
    public class MonitoriamentoService : IMonitoriamentoService
    {
        private readonly MonitoramentoContext _context;

        public MonitoriamentoService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<ListaLeituraMonitoramentoResponse> GetLeiturasMonitoramentoAsync(string cpf)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null)
            {
                return null;
            }

            var leituras = await _context.LeiturasMonitoramento
                .Where(lm => lm.PacienteCpf == cpf)
                .ToListAsync();

            var listLeituraResponse = leituras
                .Select(leitura => new LeituraMonitoramentoResponse
                {
                    DataHora = leitura.DataHora,
                    PressaoArterial = leitura.PressaoArterial,
                    BatimentosCardiacos = leitura.BatimentosCardiacos,
                    FrequenciaRespiratoria = leitura.FrequenciaRespiratoria,
                    SaturacaoOxigenio = leitura.SaturacaoOxigenio,
                    NivelCO2 = leitura.NivelCO2,
                    Temperatura = leitura.Temperatura
                })
                .ToList();

            var response = new ListaLeituraMonitoramentoResponse
            {
                PacienteCpf = paciente.Cpf,
                LeiturasMonitoramento = listLeituraResponse
            };

            return response;
        }

        public async Task<LeituraMonitoramentoResponse> CreateLeituraMonitoramentoAsync(LeituraMonitoramentoRequest request)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == request.PacienteCpf);
            if (paciente == null)
            {
                return null;
            }

            var leitura = new LeituraMonitoramento
            {
                DataHora = request.DataHora,
                PressaoArterial = request.PressaoArterial,
                BatimentosCardiacos = request.BatimentosCardiacos,
                FrequenciaRespiratoria = request.FrequenciaRespiratoria,
                SaturacaoOxigenio = request.SaturacaoOxigenio,
                NivelCO2 = request.NivelCO2,
                Temperatura = request.Temperatura,
                PacienteCpf = request.PacienteCpf,
                Paciente = paciente
            };

            _context.LeiturasMonitoramento.Add(leitura);
            await _context.SaveChangesAsync();

            var response = new LeituraMonitoramentoResponse
            {
                DataHora = leitura.DataHora,
                PressaoArterial = leitura.PressaoArterial,
                BatimentosCardiacos = leitura.BatimentosCardiacos,
                FrequenciaRespiratoria = leitura.FrequenciaRespiratoria,
                SaturacaoOxigenio = leitura.SaturacaoOxigenio,
                NivelCO2 = leitura.NivelCO2,
                Temperatura = leitura.Temperatura
            };

            return response;
        }

        public async Task DeleteLeituraMonitoramentoAsync(string cpf, DateTime? dataInicial, DateTime? dataFinal)
        {
            var paciente = await _context.Pacientes.Include(p => p.LeiturasMonitoramento)
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return;
            }

            IQueryable<LeituraMonitoramento> query = _context.LeiturasMonitoramento.Where(lm => lm.PacienteCpf == cpf);

            if (dataInicial != null)
            {
                query = query.Where(lm => lm.DataHora >= dataInicial);
            }

            if (dataFinal != null)
            {
                query = query.Where(lm => lm.DataHora <= dataFinal);
            }

            var leituras = await query.ToListAsync();

            _context.LeiturasMonitoramento.RemoveRange(leituras);
            await _context.SaveChangesAsync();
        }

        public async Task<string> CreateLeituraMonitoramentoBatchAsync(string cpf, string pathCsv)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null)
            {
                return "Paciente não cadastrado!";
            }

            if (!System.IO.File.Exists(pathCsv))
            {
                return "Arquivo CSV não encontrado.";
            }

            try
            {
                int countIntens;
                using (var reader = new StreamReader(pathCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var leituras = csv.GetRecords<LeituraMonitoramentoResponse>()
                        .Select(record => new LeituraMonitoramento
                        {
                            PacienteCpf = cpf,
                            DataHora = record.DataHora,
                            PressaoArterial = record.PressaoArterial,
                            BatimentosCardiacos = record.BatimentosCardiacos,
                            FrequenciaRespiratoria = record.FrequenciaRespiratoria,
                            SaturacaoOxigenio = record.SaturacaoOxigenio,
                            NivelCO2 = record.NivelCO2,
                            Temperatura = record.Temperatura
                        })
                        .ToList();

                    _context.LeiturasMonitoramento.AddRange(leituras);
                    await _context.SaveChangesAsync();
                    countIntens = leituras.Count;
                }

                return $"{countIntens} dados de monitoramento adicionados com sucesso!";
            }
            catch (Exception ex)
            {
                return $"Erro ao processar o arquivo CSV: {ex.Message}";
            }
        }
    }
}
