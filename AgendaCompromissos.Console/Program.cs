using AgendaCompromissos.Modelo;

Console.WriteLine("===== Agenda de Compromissos =====\n");

Console.WriteLine("Digite o nome do usuário:");
string nome = Console.ReadLine() ?? string.Empty;

Usuario usuario = new Usuario(nome);

Console.WriteLine("Informe a data do compromisso (dd/MM/yyyy):");
DateTime data = DateTime.Parse(Console.ReadLine() ?? string.Empty);


