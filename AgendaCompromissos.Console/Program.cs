using AgendaCompromissos.Modelo;

Console.WriteLine("===== Agenda de Compromissos =====\n");

Console.WriteLine("Digite o nome do usuário:");
string nome = Console.ReadLine() ?? string.Empty;

Usuario usuario = new Usuario(nome);

Console.WriteLine("Informe a data do compromisso (dd/MM/yyyy):");
DateTime data = DateTime.Parse(Console.ReadLine() ?? string.Empty);

Console.WriteLine("Informe a hora do compromisso (HH:mm):");
TimeSpan hora = TimeSpan.Parse(Console.ReadLine() ?? string.Empty);

Console.WriteLine("Informe a descrição do compromisso:");
string descricao = Console.ReadLine() ?? string.Empty;  

Console.WriteLine("Informe o local do compromisso:");
string local = Console.ReadLine() ?? string.Empty;

Compromisso compromisso = new Compromisso(data, hora, descricao, usuario, local);

Console.WriteLine("Deseja adicionar um participante? (s/n)");
string resposta = Console.ReadLine() ?? string.Empty;

while(true)
{
    if(resposta.ToLower() == "s")
    {
        Console.WriteLine("Informe o nome do participante:");
        string nomeParticipante = Console.ReadLine() ?? string.Empty;
        
        Participante participante = new Participante { Nome = nomeParticipante };
        compromisso.AdicionarParticipante(participante);
    }
    else if(resposta.ToLower() == "n")
    {
        break;
    }
    else
    {
        Console.WriteLine("Resposta inválida. Digite 's' para sim ou 'n' para não.");
    }

    Console.WriteLine("Deseja adicionar outro participante? (s/n)");
    resposta = Console.ReadLine() ?? string.Empty;
}   

Console.WriteLine("Deseja adicionar uma anotação? (s/n)");
string texto = Console.ReadLine() ?? string.Empty;

while(true)
{
    if(texto.ToLower() == "s")
    {
        Console.WriteLine("Informe o texto da anotação:");
        string anotacaoTexto = Console.ReadLine() ?? string.Empty;
        
        Anotacao anotacao = new Anotacao(anotacaoTexto);
        compromisso.AdicionarAnotacao(anotacao);
    }
    else if(texto.ToLower() == "n")
    {
        break;
    }
    else
    {
        Console.WriteLine("Resposta inválida. Digite 's' para sim ou 'n' para não.");
    }

    Console.WriteLine("Deseja adicionar outra anotação? (s/n)");
    texto = Console.ReadLine() ?? string.Empty;
}

