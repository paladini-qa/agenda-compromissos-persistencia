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
      Console.WriteLine($"Hora: {c.Hora}");
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

     Console.Write("Digite o número do compromisso que deseja excluir: ");
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
}