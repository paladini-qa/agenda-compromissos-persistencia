using System;

namespace AgendaCompromissos.Modelo;

public class Usuario
{
  //private DateTime _dataAtual = DateTime.Today.AddDays(1);
   private readonly string _nome;
  private readonly List<Compromisso> _compromisso = [];
  public IReadOnlyList<Compromisso> Compromissos
    {
        get
        {
            return _compromisso;
        }
    }

    public Usuario(string nome)
    {
        _nome = nome;
    }

    public List<string> ErrosDeValidacao = [];

    public void AdicionarCompromisso(Compromisso compromisso) {
      DateTime dataAtual = DateTime.Today.AddDays(1);

        if (compromisso.Data < dataAtual ) {
            ErrosDeValidacao.Add($"{compromisso.Data.ToString("dd/MM/yyyy")} precisa ser no minimo {dataAtual.ToString("dd/MM/yyyy")}");
        }
        if(compromisso.Descricao == null)
        {
          ErrosDeValidacao.Add($"A descrição precisa estar preenchida");
        }
        if(ErrosDeValidacao.Count == 0)
        {
          _compromisso.Add(compromisso);
        }
    }

public override string ToString()
{
    var sb = new System.Text.StringBuilder();
    sb.AppendLine($"\nUsuário: {_nome}");

    sb.AppendLine("\nCompromissos:");
    if (_compromisso.Count == 0)
    {
        sb.AppendLine("  Nenhum compromisso.");
    }
    else
    {
        foreach (var c in _compromisso)
        {
            sb.AppendLine($"Descrição: {c.Descricao} \nData {c.Data:dd/MM/yyyy} \nHora: {c.Hora:hh\\:mm}");
            sb.AppendLine($"    Local: {c.Local?.Nome ?? "N/A"}");

            if (c.Participantes.Count == 0)
                sb.AppendLine("    Participantes: Nenhum");
            else
            {
                sb.AppendLine("    Participantes:");
                foreach (var p in c.Participantes)
                    sb.AppendLine($"      - {p.Nome}");
            }

            if (c.Anotacoes.Count == 0)
                sb.AppendLine("    Anotações: Nenhuma");
            else
            {
                sb.AppendLine("    Anotações:");
                foreach (var a in c.Anotacoes)
                    sb.AppendLine($"      - {a.Texto}");
            }
        }
    }
    return sb.ToString();

}

}
