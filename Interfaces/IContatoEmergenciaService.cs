using MonitoramentoSaudeAPI.Requests;
using MonitoramentoSaudeAPI.Responses;
using MonitoramentoSaudeAPI.Models;

namespace MonitoramentoSaudeAPI.Services
{
    public interface IContatoEmergenciaService
    {
        Task<ContatosEmergenciaResponse> GetContatosEmergenciaAsync(string cpf);
        Task UpdateContatoEmergenciaAsync(string cpfPaciente, string cpfContato, ContatoEmergenciaRequest request);
        Task DeleteContatoEmergenciaAsync(string cpfPaciente, string cpfContato);
        Task<ContatoEmergencia> AddContatoEmergenciaAsync(string cpfPaciente, ContatoEmergenciaRequest request);
    }
}