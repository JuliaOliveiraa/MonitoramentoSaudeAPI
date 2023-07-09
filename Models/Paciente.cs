using Newtonsoft.Json;

namespace MonitoramentoSaudeAPI.Models;
public class Paciente
{
    public string Cpf { get; set; }
    public string Nome { get; set; }
    public DateTime DataNascimento { get; set; }
    public string Endereco { get; set; }
    public string Telefone { get; set; }
    public string HistoricoMedico { get; set; }
    public string MedicamentosEmUso { get; set; }
    public string Alergias { get; set; }
    public string Observacoes { get; set; }
    public ICollection<LeituraMonitoramento> LeiturasMonitoramento { get; set; }
}

