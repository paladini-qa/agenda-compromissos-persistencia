# SISTEMA DE AGENDA DE COMPROMISSOS COM ASSOCIAÇÃO ENTRE OBJETOS

**Alunos:** Emilly Pessuti, João Pedro Z. S, Leonardo H.

**Equipe:** 12

## Objetivo

Desenvolver uma aplicação de console em C# que simule uma agenda de compromissos, aplicando os princípios da Programação Orientada a Objetos (POO). O sistema permite que o usuário:

- Registre compromissos com data e hora.

- Defina o local e sua capacidade.

- Adicione participantes e anotações.

- Garanta a consistência e integridade das informações por meio de validações e associações entre objetos.

## Fluxo do Processo

1 - Solicita o nome do usuário principal.

2 - Permite o cadastro de um ou mais compromissos:

- Data, hora, descrição.

- Nome e capacidade do local.

3 - Pergunta se deseja adicionar participantes.

4 - Oferece a opção de adicionar anotações.

5 - Ao final, exibe todos os compromissos registrados com seus respectivos detalhes.

## Requisitos

- [.NET 6.0](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) ou superior.

## Como executar

1. No terminal, navegue até a pasta do projeto:
```bash
cd AgendaCompromissos.Console
```
2. Execute o comando e abaixo e compile o projeto:
```bash
dotnet run
```

## Cuidados a serem tomados

O programa exige que os dados sejam digitados em formato válido (ex: datas e horas no formato correto). O campo capacidade do local deve ser obrigatoriamente preenchido com um número maior que zero. Validações interrompem o programa no ponto exato da inconsistência, usando throw para sinalizar erros.

### Validações Implementadas

- Data e hora: O compromisso deve ser no mínimo para o dia seguinte.

- Descrição: Obrigatória.

- Capacidade do local: Deve ser maior que zero.

- Participantes: A quantidade não pode ultrapassar a capacidade definida.

- Encapsulamento: Listas e atributos sensíveis foram encapsulados para garantir integridade dos dados.

### Estrutura de Classes

- Usuario: Contém os compromissos associados ao usuário. As listas são encapsuladas com acesso somente leitura.

- Compromisso:

    Associação simples com Usuario e Local.

    Associação N:N com Participante.

    Composição com Anotacao.

- Participante: Pode participar de vários compromissos (relação N:N). Inclui atualização bidirecional.

- Anotacao: Existe apenas dentro de um compromisso. Armazena texto e data de criação.

- Local: Nome e capacidade. Valida o limite de participantes.

## Resultado

Ao final do processo, o sistema exibe:

- Nome do usuário

- Descrição do compromisso

- Data e hora

- Local e capacidade

- Número de participantes

- Vagas restantes

- Lista de participantes

- Anotações feitas

Depois disso, o usuário pode optar por adicionar outro compromisso.
