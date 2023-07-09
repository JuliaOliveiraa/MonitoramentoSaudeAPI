using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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

            return Ok(pacientes);
        }

        [HttpGet("paciente/{cpf}")]
        public async Task<ActionResult<IEnumerable<PacienteResponse>>> GetPaciente(string cpf)
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

            return CreatedAtAction("GetListaPacientes", new { id = paciente.Cpf }, viewModel);
        }
    }

}
