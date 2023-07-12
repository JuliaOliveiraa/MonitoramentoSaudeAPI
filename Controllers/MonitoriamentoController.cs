using Microsoft.AspNetCore.Mvc;
using MonitoramentoSaudeAPI.Models;
using MonitoramentoSaudeAPI.Services;

namespace MonitoramentoSaudeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MonitoriamentoController : ControllerBase
    {
        private readonly IMonitoriamentoService _monitoriamentoService;

        public MonitoriamentoController(IMonitoriamentoService monitoriamentoService)
        {
            _monitoriamentoService = monitoriamentoService;
        }

        [HttpGet("{cpf}")]
        public async Task<ActionResult<ListaLeituraMonitoramentoResponse>> GetLeiturasMonitoramento(string cpf)
        {
            var response = await _monitoriamentoService.GetLeiturasMonitoramentoAsync(cpf);
            return Ok(response);
        }

        [HttpPost("{cpf}")]
        public async Task<ActionResult<LeituraMonitoramentoResponse>> CreateLeituraMonitoramento([FromBody] LeituraMonitoramentoRequest request)
        {
            var response = await _monitoriamentoService.CreateLeituraMonitoramentoAsync(request);
            return Created($"{request.PacienteCpf}", response);
        }

        [HttpDelete("{cpf}")]
        public async Task<ActionResult> DeleteLeituraMonitoramento(string cpf, DateTime? dataInicial, DateTime? dataFinal)
        {
            await _monitoriamentoService.DeleteLeituraMonitoramentoAsync(cpf, dataInicial, dataFinal);
            return NoContent();
        }

        [HttpPost("lote/{cpf}")]
        public async Task<ActionResult> CreateLeituraMonitoramentoBatch(string cpf, string pathCsv)
        {
            var response = await _monitoriamentoService.CreateLeituraMonitoramentoBatchAsync(cpf, pathCsv);
            return Ok(response);
        }
    }
}