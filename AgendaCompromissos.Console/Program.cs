using AgendaCompromissos.Modelo;


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
    if (opcao == "0")
    {
        Console.WriteLine("Saindo...");
        Environment.Exit(0);
    }
    switch (opcao)
    {
        case "1":

            break;

        case "2":

            break;

        case "3":

            break;

        case "4":

            break;

        default:
            Console.WriteLine("Opção inválida.");
            break;
    }
    continue;
    break;
}