using MonitoramentoSaudeAPI.Requests;
using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Models;
public class PacienteRequestCsv
{
    [JsonProperty("cpf")]
    public string Cpf { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("dataNascimento")]
    public DateTime DataNascimento { get; set; }

    [JsonProperty("endereco")]
    public string Endereco { get; set; }

    [JsonProperty("telefone")]
    public string Telefone { get; set; }

    [JsonProperty("historicoMedico")]
    public string? HistoricoMedico { get; set; }

    [JsonProperty("medicamentosEmUso")]
    public string? MedicamentosEmUso { get; set; }

    [JsonProperty("alergias")]
    public string? Alergias { get; set; }

    [JsonProperty("observacoes")]
    public string? Observacoes { get; set; }

    [JsonProperty("contatosEmergencia")]
    public string? ContatosEmergencia { get; set; }
}

