using AgendaCompromissos.Modelo;

Console.WriteLine("===== Agenda de Compromissos =====\n");

Console.WriteLine("Digite o nome do usuário:");
string nome = Console.ReadLine() ?? string.Empty;

Usuario usuario = new Usuario(nome);

