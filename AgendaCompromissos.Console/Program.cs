using AgendaCompromissos.Modelo;
using System.Globalization;

CultureInfo culturaBrasileira = new("pt-BR");

Console.WriteLine("===== Agenda de Compromissos =====\n");

string nome;
do
{
    Console.WriteLine("Digite o nome do usuário:");
    nome = Console.ReadLine() ?? string.Empty;
    if(string.IsNullOrWhiteSpace(nome))
    { Console.WriteLine("O nome deve ser preenchido");}
} while (string.IsNullOrWhiteSpace(nome));


Usuario usuario = new Usuario(nome);

while(true)
{
DateTime data;
TimeSpan hora;
string entrada,descricao,nomelocal;


 while (true)
{
    do{
    Console.WriteLine("Informe a data do compromisso (dd/MM/yyyy):");
    entrada = Console.ReadLine() ?? string.Empty;
    if(string.IsNullOrWhiteSpace(entrada))
    { Console.WriteLine("A data deve ser preenchida");}
    } while(string.IsNullOrWhiteSpace(entrada));

    bool valido = DateTime.TryParseExact(
        entrada,
        "dd/MM/yyyy",
        System.Globalization.CultureInfo.InvariantCulture,
        System.Globalization.DateTimeStyles.None,
        out data
    );

    if (valido)
        break;

    Console.WriteLine("Formato inválido. Use o formato dd/MM/yyyy\n");
}

while (true)
{
    do{
    Console.WriteLine("Informe a hora do compromisso (HH:mm):");
     entrada = Console.ReadLine() ?? string.Empty;
     if(string.IsNullOrWhiteSpace(entrada))
    { Console.WriteLine("A hora deve ser preenchida");}
    }while(string.IsNullOrWhiteSpace(entrada));

    if (TimeSpan.TryParseExact(entrada, "hh\\:mm", CultureInfo.InvariantCulture, out hora)
        && hora >= TimeSpan.Zero && hora < TimeSpan.FromHours(24))
    {
        break;
    }

    Console.WriteLine("Hora inválida. Use o formato HH:mm, sendo de 00:00 até 23:59");
}

        do{
        Console.WriteLine("Informe a descrição do compromisso:");
        descricao = Console.ReadLine() ?? string.Empty;
        if(string.IsNullOrWhiteSpace(descricao))
        { Console.WriteLine("A descrição deve ser preenchida");}
        }while(string.IsNullOrWhiteSpace(descricao));
         
        do{
        Console.WriteLine("Informe o local do compromisso:");
         nomelocal = Console.ReadLine() ?? string.Empty;
         if(string.IsNullOrWhiteSpace(nomelocal))
        { Console.WriteLine("O nome do local deve ser preenchido");}
        }while(string.IsNullOrWhiteSpace(nomelocal));

int capacidade;


       
        Console.WriteLine("Informe a capacidade do local, em formato numérico:");
         capacidade = int.Parse(Console.ReadLine() ?? string.Empty);


 Local local=null;

try
{
            local = new Local(nomelocal, capacidade);
}
catch (ArgumentException ex)
{
            Console.WriteLine($"Erro ao criar Local: {ex.Message}");
            continue;
}

        Compromisso compromisso;
        try
        {
            compromisso = new Compromisso(data, hora, descricao, usuario, local);
            usuario.AdicionarCompromisso(compromisso);
            Console.WriteLine(usuario);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Erro ao criar compromisso: {ex.Message}");
            continue;
        }

        while (true)
        {

            Console.WriteLine("Deseja adicionar um participante? (s/n)");
            string resposta = Console.ReadLine()?.ToLower() ?? "n";
            if (resposta == "n") 
          { 
            break;
          }
            if (resposta.ToLower() == "s")
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
                    break;
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

            if (resposta.ToLower() == "s")
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
        Console.WriteLine("\n===== Compromisso Criado =====");
        Console.WriteLine(usuario);

Console.WriteLine("\nDeseja adicionar outro compromisso? (s/n)");
    string continuar = Console.ReadLine()?.ToLower() ?? "n";
    if (continuar != "s") break;

}