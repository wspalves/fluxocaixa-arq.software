## Fluxo de Caixa - Arquitetura de Software

Desenho de Arquitetura: [Miro - Desenho Arquitetural](https://miro.com/app/board/uXjVLKx22eE=/?moveToWidget=3458764605437952643&cot=14) 

## Descritivo

O projeto foi desenvolvido para apresentar conceitos de arquitetura de software. Não foi implementado interface visual, toda interação do usuário acontece via Console.

Não foram desenvolvidas funcionalidades complexas, porém o mesmo foi projetado para que tenha capacidade de ser evoluído para cenários mais complexos e escaláveis.

Para não aumentar a complexidade da aplicação, o conceito do CQRS foi implementado utilizando somente uma base de dados. A aplicação utiliza um banco de dados em memória para facilitar sua execução. Também para facilitar, ao executar a aplicação o banco de dados é preenchido com alguns lançamentos.

Neste cenários, os testes unitários escritos (usando xUnit) têm o objetivo de apresentar conceitos como AAA (Arrange, Act, Assert) e Mock (com a biblioteca Moq) e não para ter alto percentual de cobertura de teste.

Foram utilizadas abordagens/frameworks como:
- Log (com a biblioteca Serilog, não estruturado)
     O arquivo é criado na pasta FluxoCaixaArq\src do projeto

- SOLID
- KISS
- DRY
- Unit test (xUnit)
- Entity Framework (ORM)

## Padrões Utilizados

### Architectural Patterns
- DDD
- CQRS

### Design Patterns
- Abstract Factory
- Singleton
- Unit of Work

# Instruções para Executar a Aplicação

## Pré-requisitos

Antes de começar, você precisará ter os seguintes itens instalados na sua máquina:

1. **.NET SDK**
   - Você pode baixar o .NET SDK na [página de downloads do .NET](https://dotnet.microsoft.com/download).

2. **Editor de Código**
   - Recomenda-se usar um editor como [Visual Studio](https://visualstudio.microsoft.com/) ou [Visual Studio Code](https://code.visualstudio.com/).

## Passos para Executar a Aplicação

### 1. Clone o Repositório

Clone o repositório da aplicação usando o seguinte comando:

```bash
git clone https://github.com/wspalves/fluxocaixa-arq.software.git
cd fluxocaixa-arq.software
```

### 2. Execute a aplicação

Em um terminal, navegue até o diretório do projeto ConsoleApp.Caixa, localizado em FluxoCaixaArq\src\FluxoCaixaArq.ConsoleApp.Caixa

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

Para execução dos testes unitários, execute o seguinte comando:
```bash
dotnet test
```

