# Sistema-de-Agenda-de-Compromissos-com-Associao-entre-Objetos
Alunos: João Pedro Z S, Emilly Pessuti.
Equipe 12

Objetivo:

Criar uma agenda de compromissos usando a associação entre objetos.

O fluxo do Processo é o seguinte:

1 - Pede-se o nome do usuário

2 - Os dados sobre o compromisso ( data, hora, descrição, nome local, capacidade do local)

3 - Pede-se que quer adicionar um participante, colocando o nome do participante

4 - Pergunta se quer deixar alguma anotção para o compromisso

Cuidados a serem tomados:

No Program, obrigo o usuário a colocar os dados no formato correto, com exeção da capacidade, pois int nunca será null, por esse motivo já deixo explicito pro usuário que ele precisa preencher a capacidade total do local.

Validações:

1 - Em cada classe coloco suas validações e suas mensagens de erro, no caso ThrowException, para garantir que se algum dado for inválido, o programa pare naquele exato momento

2 - Só é necessário um método para validação em algumas classes, lembrando que no progama um classe pode reutilizar um método de validação de outra classe

3 - As validações eram sobre a data, que deve ser no mínimo o dia de amanhã, sobre a capacidade ser no minimo maior que 0, e a chegagem da adição de participantes a um compromisso para certificar-se de que a quantidade não ultrapasse a capacidade do local.

Sobre os campos das classes:

- Alguns campos de algumas classes tinham que ser obrigatoriamente privados, ou seja encapsulados, para garantir a proteção dos dados, pode-se averigurar na pasta AgendaCompromissos.Modelo, em súmula isso é visto na classe Usuário, aonde usou-se o encapsulamento.

- Na classe Compromisso, houve o uso de associação N:N para o campo de lista de paricipantes, e a lista de anotacões, segue a explicação abaixo:

  Considerando que posso ter vários participantes em vários compromissos, logo é N:N, e assim como a lista de anotações, os dois só podem existir se antes, houver um compromisso ( TODO ) já que os dois campos  representam a parte de um compromisso.

  As anotações e os paticipantes precisam ser uma lista, para que possa guarda-los por completo.

 Resultado:

 - Ao final, apresenta-se o nome do usuário, a descrição do compromisso, data, hora, local, capacidade, vagas ocupadas, vagas restantes, nomes dos participantes, e as anotações.
