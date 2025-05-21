using System;
using System.Globalization;
using AgendaCompromissos.Modelo;

namespace AgendaCompromissos.ConsoleApp.Utils;

public class Gerenciador
{
  CultureInfo culturaBrasileira = new("pt-BR");

  public void CriarCompromisso()
  {
    string nome = string.Empty;

    while (string.IsNullOrWhiteSpace(nome))
    {
      Console.Write("Digite o nome do usuário: ");
      nome = Console.ReadLine() ?? string.Empty;

      if (string.IsNullOrWhiteSpace(nome))
      {
        Console.WriteLine("O nome não pode estar em branco.");
      }
    }

    Usuario usuario = new Usuario(nome);

    while (true)
    {
      DateTime data;

      while (true)
      {
        Console.WriteLine("Informe a data do compromisso (dd/MM/yyyy):");
        string dataCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(dataCompromisso))
        {
          Console.WriteLine("A data deve ser preenchida.");
          continue;
        }

        bool valido = DateTime.TryParseExact(
                    dataCompromisso,
                    "dd/MM/yyyy",
                    culturaBrasileira,
                    DateTimeStyles.None,
                    out data);

        if (!valido)
        {
          Console.WriteLine("Formato inválido. Use o formato dd/MM/yyyy.\n");
          continue;
        }

        break;
      }

      TimeSpan hora;

      while (true)
      {
        Console.WriteLine("Informe a hora do compromisso (HH:mm):");
        string horaCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(horaCompromisso))
        {
          Console.WriteLine("A hora deve ser preenchida.");
          continue;
        }

        bool valido = TimeSpan.TryParseExact(
                    horaCompromisso,
                    "hh\\:mm",
                    culturaBrasileira,
                    TimeSpanStyles.None,
                    out hora);

        if (!valido)
        {
          Console.WriteLine("Formato inválido. Use o formato HH:mm.\n");
          continue;
        }

        break;
      }

      string descricao, nomelocal;

      while (true)
      {
        Console.WriteLine("Informe a descrição do compromisso:");
        descricao = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(descricao))
        {
          Console.WriteLine("A descrição deve ser preenchida.");
        }
        else
        {
          break;
        }
      }

      while (true)
      {
        Console.WriteLine("Informe o nome do local do compromisso:");
        nomelocal = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nomelocal))
        {
          Console.WriteLine("O nome do local deve ser preenchido.");
        }
        else
        {
          break;
        }
      }

      int capacidade;

      while (true)
      {
        Console.WriteLine("Informe a capacidade do local:");
        string entradaCompromisso = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(entradaCompromisso))
        {
          Console.WriteLine("A capacidade deve ser preenchida.");
          continue;
        }
        if (!int.TryParse(entradaCompromisso, out capacidade))
        {
          Console.WriteLine("O valor deve ser um número.");
        }
        else
        {
          break;
        }
      }

      Local local;
      Compromisso compromisso;

      try
      {
        local = new Local(nomelocal, capacidade);
        compromisso = new Compromisso(data, hora, descricao, usuario, local);
        usuario.AdicionarCompromisso(compromisso);

        // Participantes
        while (true)
        {
          Console.WriteLine("Deseja adicionar um participante? (s/n)");
          string resposta = Console.ReadLine()?.ToLower() ?? "n";

          if (resposta == "n") break;

          if (resposta == "s")
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

        // Anotações
        while (true)
        {
          Console.WriteLine("Deseja adicionar uma anotação? (s/n)");
          string resposta = Console.ReadLine()?.ToLower() ?? "n";

          if (resposta == "n") break;

          if (resposta == "s")
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

        string caminho = Path.Combine("Dados", "compromissos.json");
        List<Compromisso> compromissosSalvos = JsonPersistencia.Carregar<Compromisso>(caminho);
        compromissosSalvos.Add(compromisso);
        JsonPersistencia.Salvar(compromissosSalvos, caminho);

        Console.WriteLine("\n===== Compromisso Criado =====");
        Console.WriteLine(usuario);
      }
      catch (ArgumentException ex)
      {
        Console.WriteLine($"Erro ao criar compromisso: {ex.Message}\n");
        Console.WriteLine("Insira novamente os dados do compromisso");
        continue;
      }

      Console.WriteLine("\nDeseja adicionar outro compromisso? (s/n)");
      string novoCompromisso = Console.ReadLine()?.ToLower() ?? "n";

      if (novoCompromisso == "n")
      {
        Console.WriteLine("Saindo...");
        break;
      }

      if (novoCompromisso != "s")
      {
        Console.WriteLine("Resposta inválida. Encerrando por segurança.");
        break;
      }
    }
  }
  public void ListarCompromissos()
  {
    string caminho = Path.Combine("Dados", "compromissos.json");

    if (!File.Exists(caminho))
    {
      Console.WriteLine("Nenhum compromisso encontrado.");
      return;
    }

    List<Compromisso> compromissos = JsonPersistencia.Carregar<Compromisso>(caminho);

    int quantidadeCompromisso = compromissos.Count;

    if (quantidadeCompromisso == 0)
    {
      Console.WriteLine("Nenhum compromisso cadastrado.");
      return;
    }

    Console.WriteLine("\n===== Lista de Compromissos =====");

    for (int i = 0; i < quantidadeCompromisso; i++)
    {
      Compromisso c = compromissos[i];

      int capacidade = c.Local?.Capacidade ?? 0;  // A capacidade total
      int participantesCount = c.Participantes.Count;  // A quantidade de participantes
      int capacidadeRestante = capacidade - participantesCount;

      Console.WriteLine($"\n[{i + 1}] {c.Descricao}");
      Console.WriteLine($"Data: {c.Data:dd/MM/yyyy}");
      Console.WriteLine($"Hora: {c.Hora:hh\\:mm}");
      Console.WriteLine($"Usuário: {c.Usuario?.Nome}");
      Console.WriteLine($"Local: {c.Local?.Nome} \n(Capacidade Total: {c.Local?.Capacidade})");
      Console.WriteLine($"Vagas Restantes: {capacidadeRestante}");

      if (c.Participantes.Count > 0)
      {
        Console.WriteLine("Participantes:");
        foreach (var p in c.Participantes)
        {
          Console.WriteLine($"- {p.Nome}");
        }
      }
      else
      {
        Console.WriteLine("Participantes:");
        Console.WriteLine("Nenhum");
      }

      if (c.Anotacoes.Count > 0)
      {
        Console.WriteLine("Anotações:");
        foreach (var a in c.Anotacoes)
        {
          Console.WriteLine($"- {a}");
        }
      }
      else
      {
        Console.WriteLine("Anotações:");
        Console.WriteLine("Nenhuma");
      }
    }
  }
  public void EditarCompromisso()
  {

    ListarCompromissos();

    string caminho = Path.Combine("Dados", "compromissos.json");
    List<Compromisso> compromissos = JsonPersistencia.Carregar<Compromisso>(caminho);

    Console.Write("\nDigite o número do compromisso que deseja editar: ");
    int indice = int.Parse(Console.ReadLine()!) - 1;
    if (indice < 0 || indice >= compromissos.Count)
    {
      Console.WriteLine("Índice inválido.");
      return;
    }

    Compromisso compromisso = compromissos[indice];

    while (true)
    {
      Console.WriteLine("\nO que deseja editar:");
      Console.WriteLine("1 - Data");
      Console.WriteLine("2 - Hora");
      Console.WriteLine("3 - Local");
      Console.WriteLine("4 - Descrição");
      //Console.WriteLine("\n5 - Participantes");
      //Console.WriteLine("\n6 - Anotações");
      Console.WriteLine("0 - Sair");

      string opcao = string.Empty;

      while (string.IsNullOrWhiteSpace(opcao))
      {
        Console.Write("\nDigite a Opção: ");
        opcao = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(opcao))
        {
          Console.WriteLine("A opção deve estar preenchida.");
        }
      }


      if (opcao == "0")
      {
        JsonPersistencia.Salvar(compromissos, caminho);
        Console.WriteLine("Alterações salvas. Saindo...");
        return;
      }
      else if (opcao == "1")
      {
        EditarData(compromisso);
      }
      else if (opcao == "2")
      {
        EditarHora(compromisso);
      }
      else if (opcao == "3")
      {
        EditarLocal(compromisso);
      }
      else if (opcao == "4")
      {
        EditarDescricao(compromisso);
      }
      else if (opcao == "5")
      {
       // EditarParticipantes(compromisso);
      }
      else if (opcao == "6")
      {
        //EditarAnotacoes(compromisso);
      }
      else
      {
        Console.WriteLine("Opção inválida.");
      }

    }
  }

  public void ExcluirCompromisso()
  {

    ListarCompromissos();

    string caminho = Path.Combine("Dados", "compromissos.json");
    List<Compromisso> compromissos = JsonPersistencia.Carregar<Compromisso>(caminho);

    if (compromissos == null || compromissos.Count == 0)
    {
      Console.WriteLine("Não há compromissos para excluir.");
      return;
    }

    Console.Write("\nDigite o número do compromisso que deseja excluir: ");
    int indice = int.Parse(Console.ReadLine()!) - 1;
    if (indice < 0 || indice >= compromissos.Count)
    {
      Console.WriteLine("Índice inválido.");
      return;
    }
    compromissos.RemoveAt(indice);
    Console.WriteLine("Compromisso removido.");
    JsonPersistencia.Salvar(compromissos, caminho);
  }


  private void EditarData(Compromisso compromisso)
  {
    DateTime data;

    while (true)
    {
      Console.WriteLine("Informe a nova data do compromisso (dd/MM/yyyy):");
      string dataNova = Console.ReadLine() ?? string.Empty;

      if (string.IsNullOrWhiteSpace(dataNova))
      {
        Console.WriteLine("A data deve ser preenchida.");
        continue;
      }

      bool valido = DateTime.TryParseExact(
                  dataNova,
                  "dd/MM/yyyy",
                  culturaBrasileira,
                  DateTimeStyles.None,
                  out data);

      if (!valido)
      {
        Console.WriteLine("Formato inválido. Use o formato dd/MM/yyyy.\n");
        continue;
      }

      DateTime dataAtual = DateTime.Today.AddDays(1);

      if (data < dataAtual)
     {
       Console.WriteLine($"A data {data.ToString("dd/MM/yyyy")} precisa ser no mínimo {dataAtual.ToString("dd/MM/yyyy")}");
       continue;
     }

      compromisso.Data = data;
      break;
    }
  }
  
  private void EditarHora(Compromisso compromisso)
  {
    TimeSpan hora;

      while (true)
      {
        Console.WriteLine("Informe a nova hora do compromisso (HH:mm):");
        string horaNova = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(horaNova))
        {
          Console.WriteLine("A hora deve ser preenchida.");
          continue;
        }

        bool valido = TimeSpan.TryParseExact(
                    horaNova,
                    "hh\\:mm",
                    culturaBrasileira,
                    TimeSpanStyles.None,
                    out hora);

        if (!valido)
        {
          Console.WriteLine("Formato inválido. Use o formato HH:mm.\n");
          continue;
        }

        compromisso.Hora = hora;
        break;
      }
  }
  private void EditarLocal(Compromisso compromisso)
  {

     Local local;
     string nomelocalNovo;
     int capacidadeNova;

     while (true)
      {
        Console.WriteLine("Informe o nome do novo local do compromisso:");
        nomelocalNovo = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nomelocalNovo))
        {
          Console.WriteLine("O nome do local deve ser preenchido.");
        }
        else
        {
          break;
        }
      }

      while (true)
      {
        Console.WriteLine("Informe a capacidade do novo local:");
        string entradaCompromisso = Console.ReadLine() ?? string.Empty;

         if (string.IsNullOrWhiteSpace(entradaCompromisso))
        {
            Console.WriteLine("A capacidade deve ser preenchida.");
            continue;
        }

        if (!int.TryParse(entradaCompromisso, out capacidadeNova))
        {
            Console.WriteLine("O valor deve ser um número.");
            continue;
        }

        if (capacidadeNova < 1)
        {
            Console.WriteLine("A capacidade deve ser de no mínimo 1.");
            continue;
        }
       
        break;
      }

      local = new Local(nomelocalNovo, capacidadeNova);
      compromisso.Local = local;
      Console.WriteLine("Local atualizado.");
  }

  private void EditarDescricao(Compromisso compromisso)
  {
    string descricao;

      while (true)
      {
        Console.WriteLine("Informe a nova descrição do compromisso:");
        descricao = Console.ReadLine() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(descricao))
        {
          Console.WriteLine("A descrição deve ser preenchida.");
        }
        else
        {
          break;
        }
      }
      compromisso.Descricao = descricao;
      Console.WriteLine("Descrição atualizada");
  }

}
  