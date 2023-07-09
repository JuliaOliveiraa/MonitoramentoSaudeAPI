using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using System.Text.Json.Serialization;
using System.Text.Json;



namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoriamentoController : ControllerBase
    {

        private readonly MonitoramentoContext _context;

        public MonitoriamentoController(MonitoramentoContext context)
        {
            _context = context;
        }


        [HttpGet("monitoriamento/{cpf}")]
        public async Task<ActionResult<IEnumerable<LeituraMonitoramento>>> GetLeiturasMonitoramento(string cpf)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpf);
            if (paciente == null)
            {
                return NotFound("Paciente não cadastrado!");
            }

            var leituras = await _context.LeiturasMonitoramento
                .Where(lm => lm.PacienteCpf == cpf).ToArrayAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var json = JsonSerializer.Serialize(leituras, options);

            return Ok(json);
        }

        [HttpPost("monitoriamento")]
        public async Task<ActionResult<LeituraMonitoramento>> CreateLeituraMonitoramento([FromBody] LeituraMonitoramento leitura)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == leitura.PacienteCpf);
            if (paciente == null)
            {
                return BadRequest("Paciente não cadastrado!");
            }

            _context.LeiturasMonitoramento.Add(leitura);
            await _context.SaveChangesAsync();

            var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve
            };

            var json = JsonSerializer.Serialize(leitura, options);

            return Content(json, "application/json");
        }

    }

}
