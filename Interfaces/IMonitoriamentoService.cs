using MonitoramentoSaudeAPI.Models;

namespace MonitoramentoSaudeAPI.Services
{
    public interface IMonitoriamentoService
    {
        Task<ListaLeituraMonitoramentoResponse> GetLeiturasMonitoramentoAsync(string cpf);
        Task<LeituraMonitoramentoResponse> CreateLeituraMonitoramentoAsync(LeituraMonitoramentoRequest request);
        Task DeleteLeituraMonitoramentoAsync(string cpf, DateTime? dataInicial, DateTime? dataFinal);
        Task<string> CreateLeituraMonitoramentoBatchAsync(string cpf, string pathCsv);
    }
}

