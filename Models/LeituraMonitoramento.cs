using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Models;

public class LeituraMonitoramento
{
    public int Id { get; set; }
    public DateTime DataHora { get; set; }
    public string PressaoArterial { get; set; }
    public int BatimentosCardiacos { get; set; }
    public int FrequenciaRespiratoria { get; set; }
    public int SaturacaoOxigenio { get; set; }
    public int NivelCO2 { get; set; }
    public decimal Temperatura { get; set; }
    public string PacienteCpf { get; set; }
    public Paciente Paciente { get; set; }
}

