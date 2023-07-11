using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using MonitoramentoSaudeAPI.Requests;
using MonitoramentoSaudeAPI.Responses;
using MonitoramentoSaudeAPI.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContatoEmergenciaController : ControllerBase
    {
        private readonly IContatoEmergenciaService _contatoEmergenciaService;

        public ContatoEmergenciaController(IContatoEmergenciaService contatoEmergenciaService)
        {
            _contatoEmergenciaService = contatoEmergenciaService;
        }

        [HttpGet("paciente/{cpf}/contatos-emergencia")]
        public async Task<ActionResult<ContatosEmergenciaResponse>> GetContatosEmergencia(string cpf)
        {
            var response = await _contatoEmergenciaService.GetContatosEmergenciaAsync(cpf);
            return Ok(response);
        }

        [HttpPut("paciente/{cpfPaciente}/contato/{cpfContato}")]
        public async Task<ActionResult> UpdateContatoEmergencia(string cpfPaciente, string cpfContato, [FromBody] ContatoEmergenciaRequest request)
        {
            await _contatoEmergenciaService.UpdateContatoEmergenciaAsync(cpfPaciente, cpfContato, request);
            return NoContent();
        }

        [HttpDelete("paciente/{cpfPaciente}/contato/{cpfContato}")]
        public async Task<ActionResult> DeleteContatoEmergencia(string cpfPaciente, string cpfContato)
        {
            await _contatoEmergenciaService.DeleteContatoEmergenciaAsync(cpfPaciente, cpfContato);
            return NoContent();
        }

        [HttpPost("paciente/{cpfPaciente}/contato")]
        public async Task<ActionResult> AddContatoEmergencia(string cpfPaciente, [FromBody] ContatoEmergenciaRequest request)
        {
            var response = await _contatoEmergenciaService.AddContatoEmergenciaAsync(cpfPaciente, request);
            return Created($"paciente/{cpfPaciente}", response);
        }
    }
}
