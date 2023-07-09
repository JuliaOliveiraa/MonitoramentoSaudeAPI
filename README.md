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
3. Execute a API localmente.
4. Utilize ferramentas como Postman ou cURL para fazer requisições HTTP para os endpoints da API.

## Endpoints Disponíveis

- **GET /monitoriamento/{cpf}**: Retorna todas as leituras de monitoramento associadas a um paciente específico com base no CPF.

- **POST /monitoriamento**: Registra uma nova leitura de monitoramento para um paciente existente. É necessário fornecer os dados de monitoramento no corpo da requisição.

- **GET /pacientes**: Retorna a lista de todos os pacientes cadastrados.

- **POST /pacientes**: Cadastra um novo paciente. É necessário fornecer os dados do paciente no corpo da requisição.


