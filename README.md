# Fluxo de Caixa - Arquitetura de Software

Desenho de Arquitetura: [Miro - Desenho Arquitetural](https://miro.com/app/board/uXjVLKx22eE=/?share_link_id=482313154363)

Este repositório contém o código-fonte do sistema **Fluxo de Caixa**, desenvolvido com foco em apresentar padrões arquitetonicos e design patterns.
## ⚙️ Padrões Utilizados

### Architectural Patterns
- DDD
- CQRS

### Design Patterns
- Abstract Factory
- Unit of Work
- Dependecy Injection 

### Principios
- SOLID
- KISS
- AAA (Arrange, Act, Assert)

## 📝 Funcionalidades

- **Lançamento de Valores de Entradas**: Insere no banco de dados um lançamento de crédito monetário.
- **Lançamento de Valores de Saída**: Insere no banco de dados um lançamento de débito monetário.
- **Extrato Consolidado Dia Anterior**: Extrato consolidado dos lançamentos do dia anterior à requisição.
- **Extrato Consolidado por Data**: Extrato consolidado dos lançamentos da data informada.

## 🔧 Tecnologias

Este projeto foi desenvolvido utilizando as seguintes tecnologias:

- **.NET Aspire**
- **.NET 8**
- **C# 12**
- **Entity Framework Core 8.0**
- **MSSQl Server**
- **MediatR 12.4**
- **Serilog 4.1**
- **Swashbuckle 6.4**
- **Moq 4.20**
- **xUnit 2.5**

## 📦 Pré-requisitos

Antes de começar, certifique-se de ter as seguintes dependências instaladas no seu sistema:

- **.NET 8 SDK**
   - Você pode baixar o .NET SDK na [página de downloads do .NET](https://dotnet.microsoft.com/download).
     
- **Editor de Código**
   - Recomenda-se usar um editor como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

- **Git**
   - Você pode baixar o Git em [página do GIT](https://git-scm.com/downloads).

- **Docker**
   - Você pode baixar o Dockker para seu sistema operacional em [página do Docker](https://docs.docker.com/get-started/get-docker/).


## 🛠️ Instalação   
Siga as instruções abaixo para configurar o ambiente localmente:

1. **Clone o repositório**

   ```bash
   git clone https://github.com/wspalves/fluxocaixa-arq.software.git
   cd fluxocaixa-arq.software


## 🚀 Execução

### 1. Execução
Em um terminal (Prompt de comando/CMD), navegue até o diretório do projeto Aspire.Host, localizado em FluxoCaixaArq\src\FluxoCaixaArq.Aspire.Host

Execute os seguintes comandos:

```bash
dotnet restore
```

```bash
dotnet build
```

```bash
dotnet run
```

### 2. Aplicação

Após a execução, procure pela seguinte informação no terminal:
```bash
info: Aspire.Hosting.DistributedApplication[0]
      Login to the dashboard at [url exibida]
```

Acesse a URL exibida. Deverá ser exibido a tela do .NET Aspire com os seguintes recursos:

| Tipo | Nome | Estado | Hora de início | Origem | Pontos de extremidade |  Logs | Detalhes |
|-------------|-------------|-------------|-------------|-------------|-------------|-------------|-------------|
| Container | dbserver | Running | hh:mm:ss | mcr.microsoft.com/mssql/server:xxxx-latest | tcp://localhost:xxxxx | Exibir | Exibir |
| Project | ConsolidadoCaixaAPI | Running | hh:mm:ss | FluxoCaixaArq.ConsolidadoCaixa.API.csproj | https://localhost:xxxx/swagger,http://localhost:xxxx/swagger | Exibir | Exibir |
| Project | FluxoCaixaAPI | Running | hh:mm:ss | FluxoCaixaArq.FluxoCaixa.API.csproj | https://localhost:xxxx/swagger,http://localhost:xxxx/swagger | Exibir | Exibir |
| Project | migrations | Finished | hh:mm:ss | FluxoCaixaArq.Aspire.Migration.csproj | Nenhum | Exibir | Exibir |
| SqlServerDatabaseResource | FluxoCaixaDB | Running | hh:mm:ss |  | Nenhum | Exibir | Exibir |

**Explicação**

- O **dbserver** contém o Container do MSSQL Server.
- O **ConsolidadoCaixaAPI** contém a execução da API Consolidado Caixa.
- O **FluxoCaixaAPI** contém a execução da API Consolidado Caixa.
- O **migrations** contém a execução da migrations do contexto de Fluxo de Caixa para o banco de dados MSSQL.
- O **SqlServerDatabaseResource** contém a instância em execução do banco de dados FluxoCaixaDB.

### 3. Interação

Através da URL gerada para o projeto **FluxoCaixaAPI** na coluna Pontos de extremidade no dashboard acessado no passo **2**, é possível realizar  requisições para interação com a API.

- **POST** para "v1/creditar"
- **POST** para "v1/debitar"

Através da URL gerada para o projeto **ConsolidadoCaixaAPI** na coluna Pontos de extremidade no dashboard acessado no passo **2**, é possível realizar  requisições para interação com a API.

- **GET** para "v1/consolidado-ontem"
- **GET** para "v1/{data}/consolidado" 

### 4. Testes

Para execução dos testes unitários, execute o seguinte comando:
```bash
dotnet test
```

## ✏️ Notas de rodapé
- Para não aumentar a complexidade do eco sistema da aplicação, o banco de escrita e o banco de leitura estão sendo compartilhados.
- Ao executar a aplicação, é realizado um seed com créditos e débitos para o dia anterior a fim de possibilitar a execução do **GET** do Consolidado do dia anterior.
- Os testes unitários escritos (usando xUnit) têm o objetivo de apresentar conceitos como AAA (Arrange, Act, Assert) e Mock (com a biblioteca Moq) e não para ter alto percentual de cobertura de teste.
- Os logs da aplicação (usando Serilog) não estão estruturados e foram incluídos somente para abordar o conceito, possibilitando evolução do mesmo no futuro.
- Até o momento, não foi desenvolvido interface visual para interação com os recursos da aplicação. Toda interação deve ser feita através de requisições REST.
- Até o momento, não foi implementado no Aspire funcionalidades como OpenTelemetry.
- Até o momento, não foi implementado autenticação na aplicação.

