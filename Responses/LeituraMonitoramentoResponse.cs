using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Models;

public class ListaLeituraMonitoramentoResponse
{
    [JsonProperty("Cpf-Paciente")]
    public string PacienteCpf { get; set; }

    [JsonProperty("Monitoriamento")]
    public List<LeituraMonitoramentoResponse> LeiturasMonitoramento { get; set; }
}

public class LeituraMonitoramentoResponse
{
    [JsonProperty("Data-Hora")]
    public DateTime DataHora { get; set; }

    [JsonProperty("Pressao-Arterial")]
    public string PressaoArterial { get; set; }

    [JsonProperty("Batimentos-Cardiacos")]
    public int BatimentosCardiacos { get; set; }

    [JsonProperty("Frequencia-Respiratoria")]
    public int FrequenciaRespiratoria { get; set; }

    [JsonProperty("Saturacao-Oxigenio")]
    public int SaturacaoOxigenio { get; set; }

    [JsonProperty("Nivel-CO2")]
    public int NivelCO2 { get; set; }

    [JsonProperty("Temperatura")]
    public decimal Temperatura { get; set; }

}
