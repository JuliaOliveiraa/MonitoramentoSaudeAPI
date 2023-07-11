using Microsoft.EntityFrameworkCore;
using MonitoramentoSaudeAPI.Models;
using MonitoramentoSaudeAPI.Requests;
using MonitoramentoSaudeAPI.Responses;


namespace MonitoramentoSaudeAPI.Services
{
    public class ContatoEmergenciaService : IContatoEmergenciaService
    {
        private readonly MonitoramentoContext _context;

        public ContatoEmergenciaService(MonitoramentoContext context)
        {
            _context = context;
        }

        public async Task<ContatosEmergenciaResponse> GetContatosEmergenciaAsync(string cpf)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpf);

            if (paciente == null)
            {
                return null;
            }

            var response = new ContatosEmergenciaResponse
            {
                Cpf = paciente.Cpf,
                ContatosEmergencia = paciente.ContatosEmergencia.Select(contato => new ContatoEmergenciaResponse
                {
                    CpfContato = contato.CpfContato,
                    Nome = contato.Nome,
                    Telefone = contato.Telefone,
                    Endereco = contato.Endereco,
                    GrauParentesco = contato.GrauParentesco
                }).ToList()
            };

            return response;
        }

        public async Task UpdateContatoEmergenciaAsync(string cpfPaciente, string cpfContato, ContatoEmergenciaRequest request)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return;
            }

            var contato = paciente.ContatosEmergencia.FirstOrDefault(c => c.CpfContato == cpfContato);

            if (contato == null)
            {
                return;
            }

            contato.Nome = request.Nome;
            contato.Telefone = request.Telefone;
            contato.Endereco = request.Endereco;
            contato.GrauParentesco = request.GrauParentesco;

            await _context.SaveChangesAsync();
        }

        public async Task DeleteContatoEmergenciaAsync(string cpfPaciente, string cpfContato)
        {
            var paciente = await _context.Pacientes.Include(p => p.ContatosEmergencia)
                .FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return;
            }

            var contato = paciente.ContatosEmergencia.FirstOrDefault(c => c.CpfContato == cpfContato);

            if (contato == null)
            {
                return;
            }

            paciente.ContatosEmergencia.Remove(contato);
            await _context.SaveChangesAsync();
        }

        public async Task<ContatoEmergencia> AddContatoEmergenciaAsync(string cpfPaciente, ContatoEmergenciaRequest request)
        {
            var paciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Cpf == cpfPaciente);

            if (paciente == null)
            {
                return null;
            }

            var contato = new ContatoEmergencia
            {
                CpfContato = request.CpfContato,
                Nome = request.Nome,
                Telefone = request.Telefone,
                Endereco = request.Endereco,
                GrauParentesco = request.GrauParentesco,
                PacienteCpf = cpfPaciente
            };

            _context.ContatosEmergencia.Add(contato);
            await _context.SaveChangesAsync();

            return contato;
        }
    }
}
