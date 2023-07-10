using MonitoramentoSaudeAPI.Models;
using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Requests;
public class ContatoEmergenciaRequest
{
    [JsonProperty("cpfContato")]
    public string CpfContato { get; set; }

    [JsonProperty("nome")]
    public string Nome { get; set; }

    [JsonProperty("telefone")]
    public string Telefone { get; set; }

    [JsonProperty("endereco")]
    public string Endereco { get; set; }

    [JsonProperty("grauParentesco")]
    public string GrauParentesco { get; set; }

}

