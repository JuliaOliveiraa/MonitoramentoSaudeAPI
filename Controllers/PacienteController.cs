using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using CsvHelper;
using System.Globalization;
using MonitoramentoSaudeAPI.Requests;
using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {

        private readonly MonitoramentoContext _context;

        public PacienteController(MonitoramentoContext context)
        {
            _context = context;
        }


        [HttpGet("pacientes")]
        public async Task<ActionResult<IEnumerable<PacienteResponse>>> GetListaPacientes()
        {
            var pacientes = await _context.Pacientes.ToListAsync();

            if (pacientes.Count == 0)
            {
                return NotFound("Nenhum paciente cadastrado!");
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

            return Ok(response);
        }

        [HttpGet("paciente/{cpf}")]
        public async Task<ActionResult<PacienteResponse>> GetPaciente(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia).FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null)
            {
                return NotFound("Paciente não cadastrado!");
            }

            var contatosEmergencia = paciente.ContatosEmergencia
                .Select(c => new ContatoEmergenciaRequest
                {
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    GrauParentesco = c.GrauParentesco,
                    CpfContato = c.CpfContato,
                    Endereco = c.Endereco
                }).ToList();

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

            return Ok(viewModel);
        }

        [HttpPost("pacientes")]
        public async Task<ActionResult<PacienteResponse>> CreatePaciente([FromBody] PacienteRequest inputModel)
        {
            var pacienteExistente = _context.Pacientes.FirstOrDefault(p => p.Cpf == inputModel.Cpf);

            if (pacienteExistente != null)
            {
                return BadRequest("Paciente já cadastrado!");
            }

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
                ContatosEmergencia = inputModel.ContatosEmergencia.Select(c => new ContatoEmergencia
                {
                    Nome = c.Nome,
                    Telefone = c.Telefone,
                    GrauParentesco = c.GrauParentesco,
                    CpfContato = c.CpfContato,
                    Endereco = c.Endereco
                }).ToList()
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

            return Created("paciente/{cpf}", viewModel);
        }

        [HttpPut("paciente/{cpf}")]
        public async Task<ActionResult<PacienteResponse>> UpdatePaciente(string cpf, [FromBody] PacienteRequest inputModel)
        {
            var paciente = _context.Pacientes.FirstOrDefault(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return NotFound("Paciente não cadastrado!");
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

            return Ok(viewModel);
        }

        [HttpDelete("paciente/{cpf}")]
        public async Task<ActionResult> DeletePaciente(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.LeiturasMonitoramento)
                .Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("pacientes/batch")]
        public async Task<ActionResult> CreatePacientesBatch(string pathCsv)
        {
            try
            {
                if (!System.IO.File.Exists(pathCsv))
                {
                    return BadRequest("Arquivo CSV não encontrado.");
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
                            // Atualiza os dados do paciente existente com os dados do novo registro
                            pacienteExistente.Nome = pacienteRequest.Nome;
                            pacienteExistente.DataNascimento = pacienteRequest.DataNascimento;
                            pacienteExistente.Endereco = pacienteRequest.Endereco;
                            pacienteExistente.Telefone = pacienteRequest.Telefone;
                            pacienteExistente.Alergias = pacienteRequest.Alergias;
                            pacienteExistente.HistoricoMedico = pacienteRequest.HistoricoMedico;
                            pacienteExistente.MedicamentosEmUso = pacienteRequest.MedicamentosEmUso;
                            pacienteExistente.Observacoes = pacienteRequest.Observacoes;

                            // Deserializa o JSON dos contatos de emergência
                            var contatosEmergencia = JsonConvert
                                .DeserializeObject<List<ContatoEmergencia>>(pacienteRequest.ContatosEmergencia);
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

                            // Deserializa o JSON dos contatos de emergência
                            var contatosEmergencia = JsonConvert
                                .DeserializeObject<List<ContatoEmergencia>>(pacienteRequest.ContatosEmergencia);
                            paciente.ContatosEmergencia = contatosEmergencia;

                            _context.Pacientes.Add(paciente);
                            pacientesCadastrados++;
                        }
                    }

                    await _context.SaveChangesAsync();

                    return Ok($"{pacientesCadastrados} pacientes cadastrados com sucesso!\n" +
                              $"{pacienteAtualizados} pacientes atualizados com sucesso!");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar pacientes em massa a partir do arquivo CSV: {ex.Message}");
            }
        }

    }

}
