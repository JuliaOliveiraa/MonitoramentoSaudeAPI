using System;
using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using CsvHelper;
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

        [HttpGet("monitoriamento/{cpf}")]
        public async Task<ActionResult<ListaLeituraMonitoramentoResponse>> GetLeiturasMonitoramento(string cpf)
        {
            var response = await _monitoriamentoService.GetLeiturasMonitoramentoAsync(cpf);
            return Ok(response);
        }

        [HttpPost("monitoriamento/{cpf}")]
        public async Task<ActionResult<LeituraMonitoramentoResponse>> CreateLeituraMonitoramento([FromBody] LeituraMonitoramentoRequest request)
        {
            var response = await _monitoriamentoService.CreateLeituraMonitoramentoAsync(request);
            return Created($"monitoriamento/{request.PacienteCpf}", response);
        }

        [HttpDelete("monitoriamento/{cpf}")]
        public async Task<ActionResult> DeleteLeituraMonitoramento(string cpf, DateTime? dataInicial, DateTime? dataFinal)
        {
            await _monitoriamentoService.DeleteLeituraMonitoramentoAsync(cpf, dataInicial, dataFinal);
            return NoContent();
        }

        [HttpPost("monitoriamento/batch/{cpf}")]
        public async Task<ActionResult> CreateLeituraMonitoramentoBatch(string cpf, string pathCsv)
        {
            var response = await _monitoriamentoService.CreateLeituraMonitoramentoBatchAsync(cpf, pathCsv);
            return Ok(response);
        }
    }
}