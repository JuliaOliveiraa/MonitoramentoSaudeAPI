using CsvHelper;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using MonitoramentoSaudeAPI.Requests;
using Newtonsoft.Json;
using System.Globalization;

namespace MonitoramentoSaudeAPI.Services
{
    public class PacienteService : IPacienteService
    {
        private readonly MonitoramentoContext _context;

        public PacienteService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PacienteResponse>> GetListaPacientesAsync()
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            if (pacientes.Count == 0)
            {
                return null;
            }

            var response = new List<PacienteResponse>();
            foreach (var paciente in pacientes)
            {
                var contatosEmergencia = await _context.ContatosEmergencia
                    .Where(c => c.PacienteCpf == paciente.Cpf)
                    .Select(c => new ContatoEmergenciaRequest
                    {
                        Nome = c.Nome,
                        Telefone = c.Telefone,
                        GrauParentesco = c.GrauParentesco,
                        CpfContato = c.CpfContato,
                        Endereco = c.Endereco
                    })
                    .ToListAsync();

                var p = new PacienteResponse()
                {
                    Cpf = paciente.Cpf,
                    Nome = paciente.Nome,
                    DataNascimento = paciente.DataNascimento,
                    Endereco = paciente.Endereco,
                    Telefone = paciente.Telefone,
                    Alergias = paciente.Alergias,
                    HistoricoMedico = paciente.HistoricoMedico,
                    MedicamentosEmUso = paciente.MedicamentosEmUso,
                    Observacoes = paciente.Observacoes,
                    ContatosEmergencia = contatosEmergencia,
                };
                response.Add(p);
            }

            return response;
        }

        public async Task<PacienteResponse> GetPacienteAsync(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia).FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null)
            {
                return null;
            }

            var contatosEmergencia = paciente.ContatosEmergencia != null
                ? paciente.ContatosEmergencia.Select(c => new ContatoEmergenciaRequest
                {
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    GrauParentesco = c.GrauParentesco,
                    CpfContato = c.CpfContato,
                    Endereco = c.Endereco
                }).ToList()
                : new List<ContatoEmergenciaRequest>();


            var viewModel = new PacienteResponse()
            {
                Cpf = paciente.Cpf,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
                Endereco = paciente.Endereco,
                Telefone = paciente.Telefone,
                Alergias = paciente.Alergias,
                HistoricoMedico = paciente.HistoricoMedico,
                MedicamentosEmUso = paciente.MedicamentosEmUso,
                Observacoes = paciente.Observacoes,
                ContatosEmergencia = contatosEmergencia
            };

            return viewModel;
        }

        public async Task<PacienteResponse> CreatePacienteAsync(PacienteRequest inputModel)
        {
            var pacienteExistente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == inputModel.Cpf);

            if (pacienteExistente != null)
            {
                return null;
            }

            var contatosEmergencia = inputModel.ContatosEmergencia != null
                ? inputModel.ContatosEmergencia.Select(c => new ContatoEmergencia
                {
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    GrauParentesco = c.GrauParentesco,
                    CpfContato = c.CpfContato,
                    Endereco = c.Endereco
                }).ToList()
                : new List<ContatoEmergencia>();

            var paciente = new Paciente
            {
                Cpf = inputModel.Cpf,
                Nome = inputModel.Nome,
                DataNascimento = inputModel.DataNascimento,
                Endereco = inputModel.Endereco,
                Telefone = inputModel.Telefone,
                Alergias = inputModel.Alergias,
                HistoricoMedico = inputModel.HistoricoMedico,
                MedicamentosEmUso = inputModel.MedicamentosEmUso,
                Observacoes = inputModel.Observacoes,
                ContatosEmergencia = contatosEmergencia
            };

            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();

            var viewModel = new PacienteResponse()
            {
                Cpf = paciente.Cpf,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
                Endereco = paciente.Endereco,
                Telefone = paciente.Telefone,
                Alergias = paciente.Alergias,
                HistoricoMedico = paciente.HistoricoMedico,
                MedicamentosEmUso = paciente.MedicamentosEmUso,
                Observacoes = paciente.Observacoes,
                ContatosEmergencia = inputModel.ContatosEmergencia
            };

            return viewModel;
        }


        public async Task<PacienteResponse> UpdatePacienteAsync(string cpf, PacienteRequest inputModel)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return null;
            }

            paciente.Nome = inputModel.Nome;
            paciente.DataNascimento = inputModel.DataNascimento;
            paciente.Endereco = inputModel.Endereco;
            paciente.Telefone = inputModel.Telefone;
            paciente.Alergias = inputModel.Alergias;
            paciente.HistoricoMedico = inputModel.HistoricoMedico;
            paciente.MedicamentosEmUso = inputModel.MedicamentosEmUso;
            paciente.Observacoes = inputModel.Observacoes;

            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();

            var viewModel = new PacienteResponse()
            {
                Cpf = paciente.Cpf,
                Nome = paciente.Nome,
                DataNascimento = paciente.DataNascimento,
                Endereco = paciente.Endereco,
                Telefone = paciente.Telefone,
                Alergias = paciente.Alergias,
                HistoricoMedico = paciente.HistoricoMedico,
                MedicamentosEmUso = paciente.MedicamentosEmUso,
                Observacoes = paciente.Observacoes
            };

            return viewModel;
        }

        public async Task DeletePacienteAsync(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.LeiturasMonitoramento)
                .Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return;
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task<string> CreatePacientesBatchAsync(string pathCsv)
        {
            try
            {
                if (!System.IO.File.Exists(pathCsv))
                {
                    return "Arquivo CSV não encontrado.";
                }

                using (var reader = new StreamReader(pathCsv))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<PacienteRequestCsv>(); // Mapeia as linhas do CSV para objetos de PacienteRequest

                    int pacientesCadastrados = 0;
                    int pacienteAtualizados = 0;

                    foreach (var pacienteRequest in records)
                    {
                        var pacienteExistente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == pacienteRequest.Cpf);

                        if (pacienteExistente != null)
                        {
                            pacienteExistente.Nome = pacienteRequest.Nome;
                            pacienteExistente.DataNascimento = pacienteRequest.DataNascimento;
                            pacienteExistente.Endereco = pacienteRequest.Endereco;
                            pacienteExistente.Telefone = pacienteRequest.Telefone;
                            pacienteExistente.Alergias = pacienteRequest.Alergias;
                            pacienteExistente.HistoricoMedico = pacienteRequest.HistoricoMedico;
                            pacienteExistente.MedicamentosEmUso = pacienteRequest.MedicamentosEmUso;
                            pacienteExistente.Observacoes = pacienteRequest.Observacoes;

                            var contatosEmergencia = JsonConvert.DeserializeObject<List<ContatoEmergencia>>(pacienteRequest.ContatosEmergencia);
                            pacienteExistente.ContatosEmergencia = contatosEmergencia;

                            _context.Pacientes.Update(pacienteExistente);
                            pacienteAtualizados++;
                        }
                        else
                        {
                            var paciente = new Paciente
                            {
                                Cpf = pacienteRequest.Cpf,
                                Nome = pacienteRequest.Nome,
                                DataNascimento = pacienteRequest.DataNascimento,
                                Endereco = pacienteRequest.Endereco,
                                Telefone = pacienteRequest.Telefone,
                                Alergias = pacienteRequest.Alergias,
                                HistoricoMedico = pacienteRequest.HistoricoMedico,
                                MedicamentosEmUso = pacienteRequest.MedicamentosEmUso,
                                Observacoes = pacienteRequest.Observacoes
                            };

                            var contatosEmergencia = JsonConvert.DeserializeObject<List<ContatoEmergencia>>(pacienteRequest.ContatosEmergencia);
                            paciente.ContatosEmergencia = contatosEmergencia;

                            _context.Pacientes.Add(paciente);
                            pacientesCadastrados++;
                        }
                    }

                    await _context.SaveChangesAsync();

                    return $"{pacientesCadastrados} pacientes cadastrados com sucesso!\n" +
                              $"{pacienteAtualizados} pacientes atualizados com sucesso!";
                }
            }
            catch (Exception ex)
            {
                return $"Erro ao cadastrar pacientes em massa a partir do arquivo CSV: {ex.Message}";
            }
        }
    }
}