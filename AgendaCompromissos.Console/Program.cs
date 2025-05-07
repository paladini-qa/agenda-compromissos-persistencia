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
string nomeLocal = Console.ReadLine() ?? string.Empty;

Console.WriteLine("Informe a capacidade do local:");
int capacidade = int.Parse(Console.ReadLine() ?? string.Empty);

Local local = new Local { Nome = nomeLocal, Capacidade = capacidade };

Compromisso compromisso;

try
{
    compromisso = new Compromisso(data, hora, descricao, usuario, local);
}
catch (ArgumentException ex)
{
    Console.WriteLine($"Erro ao criar compromisso: {ex.Message}");
    return;
}

while (true)
{

    Console.WriteLine("Deseja adicionar um participante? (s/n)");
    string resposta = Console.ReadLine()?.ToLower() ?? "n";
    if (resposta == "n") break;

    if(resposta.ToLower() == "s")
    {
        Console.WriteLine("Informe o nome do participante:");
        string nomeParticipante = Console.ReadLine() ?? string.Empty;
        
        Participante participante = new Participante { Nome = nomeParticipante };
        try 
        {
            compromisso.AdicionarParticipante(participante);
        } 
        catch (ArgumentException ex) 
        {
            Console.WriteLine($"Erro ao adicionar participante: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Resposta inválida.");
    }

}   

while (true)
{

    Console.WriteLine("Deseja adicionar uma anotação? (s/n)");
    string resposta = Console.ReadLine()?.ToLower() ?? "n";
    if (resposta == "n") break;

    if(resposta.ToLower() == "s")
    {
        Console.WriteLine("Informe a anotação:");
        string anotacao = Console.ReadLine() ?? string.Empty;
        
        try 
        {
            compromisso.AdicionarAnotacao(anotacao);
        } 
        catch (ArgumentException ex) 
        {
            Console.WriteLine($"Erro ao adicionar anotação: {ex.Message}");
        }
    }
    else
    {
        Console.WriteLine("Resposta inválida.");
    }

}
Console.WriteLine("\n===== Compromisso Criado =====\n");
