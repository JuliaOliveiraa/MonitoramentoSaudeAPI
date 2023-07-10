using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;
using CsvHelper;
using System.Globalization;
using MonitoramentoSaudeAPI.Requests;
using MonitoramentoSaudeAPI.Responses;
using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoEmergenciaController : ControllerBase
    {

        private readonly MonitoramentoContext _context;

        public ContatoEmergenciaController(MonitoramentoContext context)
        {
            _context = context;
        }

        [HttpGet("paciente/{cpf}/contatos-emergencia")]
        public async Task<ActionResult<ContatosEmergenciaResponse>> GetContatosEmergencia(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            var response = new ContatosEmergenciaResponse
            {
                Cpf = paciente.Cpf,
                ContatosEmergencia = paciente.ContatosEmergencia.Select(contato => new ContatoEmergenciaResponse
                {
                    CpfContato = contato.CpfContato,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Endereco = contato.Endereco,
                    GrauParentesco = contato.GrauParentesco
                }).ToList()
            };

            return Ok(response);
        }


        [HttpPut("paciente/{cpfPaciente}/contato/{cpfContato}")]
        public async Task<ActionResult> UpdateContatoEmergencia(string cpfPaciente, string cpfContato, [FromBody] ContatoEmergenciaRequest request)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            var contato = paciente.ContatosEmergencia.FirstOrDefault(c => c.CpfContato == cpfContato);

            if (contato == null)
            {
                return NotFound("Contato de emergência não encontrado!");
            }

            contato.Nome = request.Nome;
            contato.Telefone = request.Telefone;
            contato.Endereco = request.Endereco;
            contato.GrauParentesco = request.GrauParentesco;

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("paciente/{cpfPaciente}/contato/{cpfContato}")]
        public async Task<ActionResult> DeleteContatoEmergencia(string cpfPaciente, string cpfContato)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            var contato = paciente.ContatosEmergencia.FirstOrDefault(c => c.CpfContato == cpfContato);

            if (contato == null)
            {
                return NotFound("Contato de emergência não encontrado!");
            }

            paciente.ContatosEmergencia.Remove(contato);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpPost("paciente/{cpfPaciente}/contato")]
        public async Task<ActionResult> AddContatoEmergencia(string cpfPaciente, [FromBody] ContatoEmergenciaRequest request)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return NotFound("Paciente não encontrado!");
            }

            var contato = new ContatoEmergencia
            {
                CpfContato = request.CpfContato,
                Nome = request.Nome,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                GrauParentesco = request.GrauParentesco,
                PacienteCpf = cpfPaciente
            };

            _context.ContatosEmergencia.Add(contato);
            await _context.SaveChangesAsync();

            return Created($"paciente/{cpfPaciente}", contato);
        }


    }

}
