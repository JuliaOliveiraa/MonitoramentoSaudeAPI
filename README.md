# API de Monitoramento de Saúde

Esta API foi desenvolvida para fornecer recursos de monitoramento de saúde, permitindo o registro e recuperação de dados vitais de pacientes, como pressão arterial, batimentos cardíacos, frequência respiratória, saturação de oxigênio, nível de dióxido de carbono e temperatura corporal.

## Recursos Principais

- Cadastro de pacientes: Permite adicionar informações dos pacientes, como CPF, nome, data de nascimento, endereço e telefone.

- Registro de leituras de monitoramento: Permite adicionar leituras de monitoramento para os pacientes cadastrados, incluindo a data e hora da leitura, pressão arterial, batimentos cardíacos, frequência respiratória, saturação de oxigênio, nível de dióxido de carbono e temperatura corporal.

- Consulta de leituras de monitoramento por paciente: Permite recuperar todas as leituras de monitoramento associadas a um paciente específico com base no CPF.

## Tecnologias Utilizadas

- Linguagem de programação: C#
- Framework: .NET Core
- Banco de dados: SQL Server
- ORM: Entity Framework Core
- Protocolo de comunicação: HTTP
- Formato de dados: JSON

## Configuração do Banco de Dados

A API utiliza o SQL Server como banco de dados. É necessário configurar uma conexão com um banco de dados local ou remoto, fornecendo a Connection String adequada no arquivo de configuração `appsettings.json`.

## Como Utilizar

1. Clone o repositório para a sua máquina local.
2. Configure a conexão com o banco de dados no arquivo `appsettings.json`.
3. Abra o terminal de comando ou o Package Manager Console no Visual Studio.
4. Navegue até o diretório raiz do seu projeto, onde está localizado o arquivo `Startup.cs`.
5. Execute o seguinte comando para aplicar as migrações e criar as tabelas no banco de dados:

   ### Visual Studio:
   ```c#
   Update-Database
   ```
   
   ### .NET CLI:
   ```c#
   dotnet ef database update
   ```
    Isso executará as migrações pendentes e aplicará as alterações no banco de dados.

6. Execute a API localmente.
7. Utilize ferramentas como Postman, cURL ou o Swagger para fazer requisições HTTP para os endpoints da API.

   Para acessar o Swagger, abra um navegador e digite a URL http://localhost:{porta}/swagger, substituindo {porta} pela porta configurada para a API. O Swagger fornecerá uma interface interativa onde você poderá explorar e testar os endpoints da API. Agora, você pode utilizar o Swagger para facilitar o teste e a documentação dos endpoints da sua API.
## Endpoints Disponíveis

### PacienteController

#### GetListaPacientes
- **Endpoint:** GET /pacientes 
- **Descrição:** Retorna uma lista de todos os pacientes cadastrados no sistema.
  <p>Exemplo de solicitação:

  ```
  GET /pacientes
  ```
  
  Exemplo de resposta:
  ```json
  Status: 200 OK
  [
    {
      "cpf": "12345678900",
      "nome": "João da Silva",
      "dataNascimento": "1990-01-01",
      "endereco": "Rua A, 123",
      "telefone": "(00) 1234-5678",
      "alergias": "Nenhuma",
      "historicoMedico": "Nenhum",
      "medicamentosEmUso": "Nenhum",
      "observacoes": ""
    },
    {
      "cpf": "98765432100",
      "nome": "Maria Souza",
      "dataNascimento": "1985-05-10",
      "endereco": "Avenida B, 456",
      "telefone": "(00) 9876-5432",
      "alergias": "Nenhuma",
      "historicoMedico": "Nenhum",
      "medicamentosEmUso": "Nenhum",
      "observacoes": ""
    }
  ]
  ```

#### GetPaciente
- **Endpoint:** GET /paciente/{cpf}
- **Descrição:** Retorna os detalhes de um paciente específico com base no CPF fornecido.

  <p>Exemplo de solicitação:

  ```http
  GET /paciente/12345678900
  ```
  
  Exemplo de resposta:
  ```json
  Status: 200 OK
  [
    {
      "cpf": "12345678900",
      "nome": "João da Silva",
      "dataNascimento": "1990-01-01",
      "endereco": "Rua A, 123",
      "telefone": "(00) 1234-5678",
      "alergias": "Nenhuma",
      "historicoMedico": "Nenhum",
      "medicamentosEmUso": "Nenhum",
      "observacoes": ""
    }
  ]
  ```
  
#### CreatePaciente
- **Endpoint:** POST /pacientes
- **Descrição:** Cadastra um novo paciente no sistema.
  <p>Exemplo de solicitação:

  ```http
  POST /pacientes
  Content-Type: application/json

  {
    "cpf": "12345678900",
    "nome": "João da Silva",
    "dataNascimento": "1990-01-01",
    "endereco": "Rua A, 123",
    "telefone": "(00) 1234-5678",
    "alergias": "Nenhuma",
    "historicoMedico": "Nenhum",
    "medicamentosEmUso": "Nenhum",
    "observacoes": ""
  }

  ```
  
  Exemplo de resposta:
  ```json
  Status: 201 Created
  {
  "cpf": "12345678900",
  "nome": "João da Silva",
  "dataNascimento": "1990-01-01",
  "endereco": "Rua A, 123",
  "telefone": "(00) 1234-5678",
  "alergias": "Nenhuma",
  "historicoMedico": "Nenhum",
  "medicamentosEmUso": "Nenhum",
  "observacoes": ""
  }

  ```
  
#### UpdatePaciente
- **Endpoint:** PUT /paciente/{cpf}
- **Descrição:** Atualiza os dados de um paciente existente com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  PUT /paciente/12345678900
  Content-Type: application/json

  {
    "cpf": "12345678900",
    "nome": "João da Silva",
    "dataNascimento": "1990-01-01",
    "endereco": "Rua B, 456",
    "telefone": "(00) 9876-5432",
    "alergias": "Nenhuma",
    "historicoMedico": "Nenhum",
    "medicamentosEmUso": "Nenhum",
    "observacoes": "Atualizado"
  }
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  {
    "cpf": "12345678900",
    "nome": "João da Silva",
    "dataNascimento": "1990-01-01",
    "endereco": "Rua B, 456",
    "telefone": "(00) 9876-5432",
    "alergias": "Nenhuma",
    "historicoMedico": "Nenhum",
    "medicamentosEmUso": "Nenhum",
    "observacoes": "Atualizado"
  }
  ```

#### DeletePaciente
- **Endpoint:** DELETE /paciente/{cpf}
- **Descrição:** Exclui um paciente existente com base no CPF fornecido, juntamente com todos os seus contatos de emergência associados.
  <p>Exemplo de solicitação:

  ```http
  DELETE /paciente/12345678900
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```
  
#### CreatePacientesBatch
- **Endpoint:** POST /pacientes/batch
- **Descrição:** Permite cadastrar vários pacientes de uma vez a partir de um arquivo CSV contendo os dados dos pacientes.

  ```http
  POST /pacientes/batch?pathCsv=/caminho/do/arquivo.csv
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  "2 pacientes cadastrados com sucesso!
  0 pacientes atualizados com sucesso!"
  ```

### ContatoEmergenciaController

#### GetContatosEmergencia
- **Endpoint:** GET /paciente/{cpf}/contatos-emergencia
- **Descrição:** Retorna os contatos de emergência associados a um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  GET /paciente/12345678900/contatos-emergencia
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  {
    "cpf": "12345678900",
    "contatosEmergencia": [
      {
        "cpfContato": "98765432100",
        "nome": "Maria Souza",
        "telefone": "(00) 9876-5432",
        "endereco": "Avenida B, 456",
        "grauParentesco": "Irmão"
      }
    ]
  }
  ```
#### UpdateContatoEmergencia
- **Endpoint:** PUT /paciente/{cpfPaciente}/contato/{cpfContato}
- **Descrição:** Atualiza os dados de um contato de emergência associado a um paciente específico.
  <p>Exemplo de solicitação:

  ```http
  PUT /paciente/12345678900/contato/98765432100
  Content-Type: application/json

  {
    "nome": "Maria Souza",
    "telefone": "(00) 9876-5432",
    "endereco": "Avenida C, 789",
    "grauParentesco": "Irmão"
  }

  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```

#### DeleteContatoEmergencia
- **Endpoint:** DELETE /paciente/{cpfPaciente}/contato/{cpfContato}
- **Descrição:** Exclui um contato de emergência específico associado a um paciente.
  <p>Exemplo de solicitação:

  ```http
  DELETE /paciente/12345678900/contato/98765432100
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```

#### AddContatoEmergencia
- **Endpoint:** POST /paciente/{cpfPaciente}/contato
- **Descrição:** Adiciona um novo contato de emergência ao paciente com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  POST /paciente/12345678900/contato
  Content-Type: application/json

  {
    "cpfContato": "98765432100",
    "nome": "Maria Souza",
    "telefone": "(00) 9876-5432",
    "endereco": "Avenida B, 456",
    "grauParentesco": "Irmão"
  }
  ```
  Exemplo de resposta:
  ```json
  Status: 201 Created
  {
    "cpfContato": "98765432100",
    "nome": "Maria Souza",
    "telefone": "(00) 9876-5432",
    "endereco": "Avenida B, 456",
    "grauParentesco": "Irmão"
  }
  ```

### MonitoramentoController

#### GetLeiturasMonitoramento
- **Endpoint:** GET /monitoriamento/{cpf}
- **Descrição:** Retorna a lista de leituras de monitoramento associadas a um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  GET /monitoriamento/12345678900
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  {
    "pacienteCpf": "12345678900",
    "leiturasMonitoramento": [
      {
        "dataHora": "2023-07-01T08:00:00",
        "pressaoArterial": "120/80",
        "batimentosCardiacos": 75,
        "frequenciaRespiratoria": 16,
        "saturacaoOxigenio": 98,
        "nivelCO2": 35,
        "temperatura": 36.5
      },
      {
        "dataHora": "2023-07-02T09:30:00",
        "pressaoArterial": "118/82",
        "batimentosCardiacos": 80,
        "frequenciaRespiratoria": 18,
        "saturacaoOxigenio": 97,
        "nivelCO2": 36,
        "temperatura": 36.8
      }
    ]
  }

  ```

#### CreateLeituraMonitoramento
- **Endpoint:** POST /monitoriamento/{cpf}
- **Descrição:** Cria uma nova leitura de monitoramento para um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  Status: 200 OK
  {
    "pacienteCpf": "12345678900",
    "leiturasMonitoramento": [
      {
        "dataHora": "2023-07-01T08:00:00",
        "pressaoArterial": "120/80",
        "batimentosCardiacos": 75,
        "frequenciaRespiratoria": 16,
        "saturacaoOxigenio": 98,
        "nivelCO2": 35,
        "temperatura": 36.5
      },
      {
        "dataHora": "2023-07-02T09:30:00",
        "pressaoArterial": "118/82",
        "batimentosCardiacos": 80,
        "frequenciaRespiratoria": 18,
        "saturacaoOxigenio": 97,
        "nivelCO2": 36,
        "temperatura": 36.8
      }
    ]
  }
  ```
  Exemplo de resposta:
  ```json
  Status: 201 Created
  {
    "dataHora": "2023-07-03T10:15:00",
    "pressaoArterial": "122/78",
    "batimentosCardiacos": 70,
    "frequenciaRespiratoria": 17,
    "saturacaoOxigenio": 99,
    "nivelCO2": 34,
    "temperatura": 36.2
  }
  ```
#### DeleteLeituraMonitoramento
- **Endpoint:** DELETE /monitoriamento/{cpf}
- **Descrição:** Exclui as leituras de monitoramento de um paciente específico com base no CPF fornecido. As leituras podem ser filtradas por um período de tempo opcional.
  <p>Exemplo de solicitação:

  ```http
  DELETE /monitoriamento/12345678900?dataInicial=2023-07-01&dataFinal=2023-07-02
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```
#### CreateLeituraMonitoramentoBatch
- **Endpoint:** POST /monitoriamento/batch/{cpf}
- **Descrição:** Permite adicionar várias leituras de monitoramento de uma vez a partir de um arquivo CSV contendo os dados das leituras.
  <p>Exemplo de solicitação:

  ```http
  POST /monitoriamento/batch/12345678900?pathCsv=/caminho/do/arquivo.csv
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  "2 dados de monitoramento adicionados com sucesso!"
  ```
