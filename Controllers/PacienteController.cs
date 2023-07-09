using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

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
                    Observacoes = paciente.Observacoes
                };
                response.Add(p);
            }

            return Ok(response);
        }

        [HttpGet("paciente/{cpf}")]
        public async Task<ActionResult<PacienteResponse>> GetPaciente(string cpf)
        {
            var paciente = _context.Pacientes.Where(p => p.Cpf == cpf).FirstOrDefault();
            if (paciente == null)
            {
                return NotFound("Paciente não cadastrado!");
            }

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
                Observacoes = inputModel.Observacoes
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
                Observacoes = paciente.Observacoes
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
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            _context.Pacientes.Remove(paciente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }

}
