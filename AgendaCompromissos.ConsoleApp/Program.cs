using AgendaCompromissos.Modelo;
using AgendaCompromissos.ConsoleApp.Utils;

var gerenciador = new Gerenciador();

while (true)
{
    Console.Clear();
    Console.WriteLine("===== Agenda de Compromissos =====\n");
    Console.WriteLine("O que você deseja fazer?\n");
    Console.WriteLine("1. Criar compromisso");
    Console.WriteLine("2. Listar compromissos");
    Console.WriteLine("3. Editar compromisso");
    Console.WriteLine("4. Excluir compromisso");
    Console.WriteLine("0. Sair\n");

    Console.Write("Escolha uma opção: ");
    string opcao = Console.ReadLine() ?? "";

    switch (opcao)
    {
        case "0":
            Console.WriteLine("Saindo...");
            Environment.Exit(0);
            break;
        case "1":
            gerenciador.CriarCompromisso();
            break;

        case "2":
            gerenciador.ListarCompromissos();
            break;

        case "3":
            gerenciador.EditarCompromisso();
            break;

        case "4":
            gerenciador.ExcluirCompromisso();
            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
}