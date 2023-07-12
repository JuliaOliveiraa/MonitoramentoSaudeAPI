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
- **Endpoint:** GET /api/Paciente/pacientes 
- **Descrição:** Retorna uma lista de todos os pacientes cadastrados no sistema.
  <p>Exemplo de solicitação:

  ```
  GET /api/Paciente/pacientes
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
- **Endpoint:** GET /api/Paciente/{cpf}
- **Descrição:** Retorna os detalhes de um paciente específico com base no CPF fornecido.

  <p>Exemplo de solicitação:

  ```http
  GET /api/Paciente/12345678900
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
- **Endpoint:** POST /api/Paciente/paciente
- **Descrição:** Cadastra um novo paciente no sistema.
  <p>Exemplo de solicitação:

  ```http
  POST /api/Paciente/paciente
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
- **Endpoint:** PUT /api/Paciente/{cpf}
- **Descrição:** Atualiza os dados de um paciente existente com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  PUT /api/Paciente/12345678900
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
- **Endpoint:** DELETE /api/Paciente/{cpf}
- **Descrição:** Exclui um paciente existente com base no CPF fornecido, juntamente com todos os seus contatos de emergência associados.
  <p>Exemplo de solicitação:

  ```http
  DELETE /api/Paciente/paciente/12345678900
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```
  
#### CreatePacientesBatch
- **Endpoint:** POST /api/Paciente/lote
- **Descrição:** Permite cadastrar vários pacientes de uma vez a partir de um arquivo CSV contendo os dados dos pacientes.

  ```http
  POST /api/Paciente/pacientes/batch?pathCsv=/caminho/do/arquivo.csv
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  "2 pacientes cadastrados com sucesso!
  0 pacientes atualizados com sucesso!"
  ```

### ContatoEmergenciaController

#### GetContatosEmergencia
- **Endpoint:** GET /api/ContatoEmergencia/{cpf}
- **Descrição:** Retorna os contatos de emergência associados a um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  GET /12345678900
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
- **Endpoint:** PUT /api/ContatoEmergencia/{cpfPaciente}/{cpfContato}
- **Descrição:** Atualiza os dados de um contato de emergência associado a um paciente específico.
  <p>Exemplo de solicitação:

  ```http
  PUT /api/ContatoEmergencia/12345678900/98765432100
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
- **Endpoint:** DELETE /api/ContatoEmergencia/{cpfPaciente}/{cpfContato}
- **Descrição:** Exclui um contato de emergência específico associado a um paciente.
  <p>Exemplo de solicitação:

  ```http
  DELETE /12345678900/98765432100
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```

#### AddContatoEmergencia
- **Endpoint:** POST /api/ContatoEmergencia/{cpfPaciente}
- **Descrição:** Adiciona um novo contato de emergência ao paciente com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  POST /api/ContatoEmergencia/12345678900
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
- **Endpoint:** GET /api/Monitoriamento/{cpf}
- **Descrição:** Retorna a lista de leituras de monitoramento associadas a um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  GET /12345678900
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
- **Endpoint:** POST /api/Monitoriamento/{cpf}
- **Descrição:** Cria uma nova leitura de monitoramento para um paciente específico com base no CPF fornecido.
  <p>Exemplo de solicitação:

  ```http
  POST /api/Monitoriamento/12345678900
  Content-Type: application/json
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
- **Endpoint:** DELETE /api/Monitoriamento/{cpf}
- **Descrição:** Exclui as leituras de monitoramento de um paciente específico com base no CPF fornecido. As leituras podem ser filtradas por um período de tempo opcional.
  <p>Exemplo de solicitação:

  ```http
  DELETE /api/Monitoriamento/12345678900?dataInicial=2023-07-01&dataFinal=2023-07-02
  ```
  Exemplo de resposta:
  ```json
  Status: 204 No Content
  ```
#### CreateLeituraMonitoramentoBatch
- **Endpoint:** POST /api/Monitoriamento/lote/{cpf}
- **Descrição:** Permite adicionar várias leituras de monitoramento de uma vez a partir de um arquivo CSV contendo os dados das leituras.
  <p>Exemplo de solicitação:

  ```http
  POST /api/Monitoriamento/lote/12345678900?pathCsv=/caminho/do/arquivo.csv
  ```
  Exemplo de resposta:
  ```json
  Status: 200 OK
  "2 dados de monitoramento adicionados com sucesso!"
  ```
# Dados de Teste

A pasta `CSVs` contém dois arquivos CSV que podem ser utilizados para realizar testes nos endpoints da API:

1. **DadosPacientes.csv**: Este arquivo contém dados fictícios de pacientes. Cada linha representa as informações de um paciente, incluindo CPF, nome, data de nascimento, endereço, telefone, alergias, histórico médico, medicamentos em uso e observações. Esses dados podem ser usados para cadastrar novos pacientes ou atualizar os dados de pacientes existentes.

2. **MonitorSaude.csv**: Este arquivo contém dados fictícios de monitoramento de saúde. Cada linha representa uma leitura de monitoramento associada a um paciente. As informações incluem o CPF do paciente, data e hora da leitura, pressão arterial, batimentos cardíacos, frequência respiratória, saturação de oxigênio, nível de dióxido de carbono e temperatura corporal. Esses dados podem ser utilizados para registrar leituras de monitoramento ou consultar as leituras de um paciente específico.

Para utilizar esses arquivos nos endpoints relevantes, você pode especificar o caminho completo do arquivo CSV como parâmetro nas requisições adequadas. Certifique-se de que os arquivos estejam presentes na pasta `CSVs` do projeto antes de realizar os testes.

Exemplo de uso para cada endpoint:

- **Cadastro de Paciente em Lote (CSV)**:
  - Endpoint: `POST /api/pacientes/lote`
  - Parâmetro: `pathCsv` - Caminho completo para o arquivo `DadosPacientes.csv`

- **Registro de Leitura de Monitoramento em Lote (CSV)**:
  - Endpoint: `POST /api/monitoriamento/lote/{cpf}`
  - Parâmetros: `cpf` - CPF do paciente associado às leituras de monitoramento
  - Parâmetro: `pathCsv` - Caminho completo para o arquivo `MonitorSaude.csv`

Certifique-se de fornecer os caminhos corretos para os arquivos CSV ao realizar as requisições.

# Conclusão

A API de Monitoramento de Saúde oferece recursos essenciais para o registro e recuperação de dados vitais de pacientes. Com endpoints para cadastrar pacientes, registrar leituras de monitoramento e consultar as leituras associadas a um paciente, a API permite o acompanhamento e controle adequados da saúde dos pacientes.

Utilizando tecnologias modernas como C#, .NET Core e SQL Server. Os dados são armazenados em um banco de dados relacional, usando o Entity Framework Core como ORM para interagir com o banco de dados.

Além disso, a API disponibiliza arquivos CSV de dados fictícios para facilitar os testes dos endpoints. Os arquivos `DadosPacientes.csv` e `MonitorSaude.csv`, localizados na pasta `CSVs`, podem ser usados para cadastrar pacientes em lote e registrar leituras de monitoramento em lote, respectivamente.



