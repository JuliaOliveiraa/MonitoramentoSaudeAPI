using MonitoramentoSaudeAPI.Models;

namespace MonitoramentoSaudeAPI.Services
{
    public interface IPacienteService
    {
        Task<IEnumerable<PacienteResponse>> GetListaPacientesAsync();
        Task<PacienteResponse> GetPacienteAsync(string cpf);
        Task<PacienteResponse> CreatePacienteAsync(PacienteRequest inputModel);
        Task<PacienteResponse> UpdatePacienteAsync(string cpf, PacienteRequest inputModel);
        Task DeletePacienteAsync(string cpf);
        Task<string> CreatePacientesBatchAsync(string pathCsv);
    }
}