namespace MonitoramentoSaudeAPI.Models;
public class ContatoEmergencia
{
    public string CpfContato { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public string GrauParentesco { get; set; }
    public string PacienteCpf { get; set; }
    public Paciente Paciente { get; set; }
}