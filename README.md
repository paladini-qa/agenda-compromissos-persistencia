# SISTEMA DE AGENDA DE COMPROMISSOS COM ASSOCIAÇÃO ENTRE OBJETOS

**Alunos:** João Pedro Z. S, Layssa Alves, Vitor Paladini.

## Objetivo

Desenvolver uma aplicação de console em C# que simula uma agenda de compromissos, aplicando os princípios da Programação Orientada a Objetos (POO). O sistema permite que o usuário:

- Registre compromissos com data e hora.
- Defina o local e sua capacidade.
- Adicione participantes e anotações.
- Garanta a consistência e integridade das informações por meio de validações e associações entre objetos.
- **Persistência de dados:** Os dados da agenda são salvos e carregados automaticamente utilizando serialização de arquivos JSON.

## Funcionalidades do Menu

O menu principal oferece as seguintes opções:

1. Criar compromisso
2. Listar compromissos
3. Editar compromisso
4. Excluir compromisso
5. Sair

O usuário pode criar, visualizar, editar e excluir compromissos de forma interativa pelo console. Os dados são persistidos entre execuções do programa.

## Fluxo do Processo

1. Solicita o nome do usuário principal.
2. Permite o cadastro de um ou mais compromissos:
   - Data (dd/MM/yyyy), hora (HH:mm), descrição.
   - Nome e capacidade do local.
3. Pergunta se deseja adicionar participantes (sem ultrapassar a capacidade do local).
4. Oferece a opção de adicionar anotações.
5. Ao final, exibe todos os compromissos registrados com seus respectivos detalhes.
6. Os dados são salvos automaticamente em arquivo JSON ao criar, editar ou excluir compromissos, e carregados ao iniciar o programa.

## Validações Implementadas

- Data e hora: O compromisso deve ser no mínimo para o dia seguinte.
- Descrição: Obrigatória.
- Capacidade do local: Deve ser maior que zero.
- Participantes: A quantidade não pode ultrapassar a capacidade definida.
- Anotações: Não podem ser vazias.
- Encapsulamento: Listas e atributos sensíveis foram encapsulados para garantir integridade dos dados.
- Validações interrompem o programa no ponto exato da inconsistência, usando throw para sinalizar erros.

## Estrutura de Classes

- **Usuario**: Contém os compromissos associados ao usuário. As listas são encapsuladas com acesso somente leitura. Possui método para adicionar compromissos com validação.
- **Compromisso**: Associação simples com Usuario e Local. Associação N:N com Participante. Composição com Anotacao. Possui métodos para validação, adicionar participantes e anotações.
- **Participante**: Pode participar de vários compromissos (relação N:N).
- **Anotacao**: Existe apenas dentro de um compromisso. Armazena texto e data de criação.
- **Local**: Nome e capacidade. Valida o limite de participantes e a capacidade mínima.
- **Gerenciador**: Responsável por salvar e carregar os dados da agenda utilizando serialização JSON.

## Como executar

1. No terminal, navegue até a pasta do projeto:

```bash
cd AgendaCompromissos.Console
```

2. Execute o comando abaixo para compilar e rodar o projeto:

```bash
dotnet run
```

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

Depois disso, o usuário pode optar por adicionar, editar ou excluir outros compromissos. Todas as alterações são salvas automaticamente no arquivo JSON.

## Observações

- O programa exige que os dados sejam digitados em formato válido (ex: datas e horas no formato correto).
- O campo capacidade do local deve ser obrigatoriamente preenchido com um número maior que zero.
- Só é necessário um método para validação em algumas classes, lembrando que no programa uma classe pode reutilizar um método de validação de outra classe.
- O sistema utiliza throw para sinalizar erros de validação, interrompendo o fluxo quando necessário.
- Os dados da agenda são persistidos em arquivo JSON, garantindo que as informações não sejam perdidas ao fechar o programa.
