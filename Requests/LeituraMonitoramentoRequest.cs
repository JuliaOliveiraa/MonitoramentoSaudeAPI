using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Models;

public class LeituraMonitoramentoRequest
{
    [JsonProperty("dataHora")]
    public DateTime DataHora { get; set; }

    [JsonProperty("pressaoArterial")]
    public string PressaoArterial { get; set; }

    [JsonProperty("batimentosCardiacos")]
    public int BatimentosCardiacos { get; set; }

    [JsonProperty("frequenciaRespiratoria")]
    public int FrequenciaRespiratoria { get; set; }

    [JsonProperty("saturacaoOxigenio")]
    public int SaturacaoOxigenio { get; set; }

    [JsonProperty("nivelCO2")]
    public int NivelCO2 { get; set; }

    [JsonProperty("temperatura")]
    public decimal Temperatura { get; set; }

    [JsonProperty("pacienteCpf")]
    public string PacienteCpf { get; set; }
}

