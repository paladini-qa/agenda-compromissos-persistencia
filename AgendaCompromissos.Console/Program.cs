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