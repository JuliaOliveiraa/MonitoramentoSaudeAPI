namespace MonitoramentoSaudeAPI.Responses;

public class ContatosEmergenciaResponse
{
    public string Cpf { get; set; }
    public List<ContatoEmergenciaResponse> ContatosEmergencia { get; set; }
}

public class ContatoEmergenciaResponse
{
    public string CpfContato { get; set; }
    public string Nome { get; set; }
    public string Telefone { get; set; }
    public string Endereco { get; set; }
    public string GrauParentesco { get; set; }
}

