using Microsoft.AspNetCore.Mvc;
using MonitoramentoSaudeAPI.Models;
using MonitoramentoSaudeAPI.Services;

namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PacienteController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacienteController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet("pacientes")]
        public async Task<ActionResult<IEnumerable<PacienteResponse>>> GetListaPacientes()
        {
            var response = await _pacienteService.GetListaPacientesAsync();
            return Ok(response);
        }

        [HttpGet("paciente/{cpf}")]
        public async Task<ActionResult<PacienteResponse>> GetPaciente(string cpf)
        {
            var response = await _pacienteService.GetPacienteAsync(cpf);
            return Ok(response);
        }

        [HttpPost("pacientes")]
        public async Task<ActionResult<PacienteResponse>> CreatePaciente([FromBody] PacienteRequest inputModel)
        {
            var response = await _pacienteService.CreatePacienteAsync(inputModel);
            return Created($"paciente/{response.Cpf}", response);
        }

        [HttpPut("paciente/{cpf}")]
        public async Task<ActionResult<PacienteResponse>> UpdatePaciente(string cpf, [FromBody] PacienteRequest inputModel)
        {
            var response = await _pacienteService.UpdatePacienteAsync(cpf, inputModel);
            return Ok(response);
        }

        [HttpDelete("paciente/{cpf}")]
        public async Task<ActionResult> DeletePaciente(string cpf)
        {
            await _pacienteService.DeletePacienteAsync(cpf);
            return NoContent();
        }

        [HttpPost("pacientes/batch")]
        public async Task<ActionResult> CreatePacientesBatch(string pathCsv)
        {
            var response = await _pacienteService.CreatePacientesBatchAsync(pathCsv);
            return Ok(response);
        }
    }
}
