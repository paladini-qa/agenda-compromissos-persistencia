using System;

namespace AgendaCompromissos.Modelo;

public class Usuario
{
    //private readonly string _nome;
    public string Nome { get; set;  }
    public List<Compromisso> Compromissos { get; set; } = new();

    //private readonly List<Compromisso> _compromisso = [];
    // public IReadOnlyList<Compromisso> Compromissos
    // {
    //     get
    //     {
    //         return _compromisso;
    //     }
    // }

    // public Usuario(string nome)
    // {
    //     _nome = nome;
    // }

    public List<string> ErrosDeValidacao { get; set;  } = new();
    
    public Usuario() {}
    public Usuario(string nome) {
        Nome = nome;
    }

    public void AdicionarCompromisso(Compromisso compromisso) {
        DateTime dataAtual = DateTime.Today.AddDays(1);

        if (compromisso.Data < dataAtual) {
            ErrosDeValidacao.Add($"{compromisso.Data.ToString("dd/MM/yyyy")} precisa ser no minimo {dataAtual.ToString("dd/MM/yyyy")}");
        }
        if (compromisso.Descricao == null)
        {
            ErrosDeValidacao.Add($"A descrição precisa estar preenchida");
        }
        if (compromisso.Local.Capacidade < 1)
        {
            ErrosDeValidacao.Add("O local precisa ter no mínimo 1 de capacidade.");
        }
        if (ErrosDeValidacao.Count == 0)
        {
            Compromissos.Add(compromisso);
        }

    }

public override string ToString()
{
    var sb = new System.Text.StringBuilder();
    sb.AppendLine($"\nUsuário: {Nome}\n");

    if (Compromissos.Count == 0)
    {
        sb.AppendLine("  Nenhum compromisso.");
    }
    else
    {
        foreach (var c in Compromissos)
        {

            sb.AppendLine($"\nDescrição: {c.Descricao}");
            sb.AppendLine($"Data: {c.Data:dd/MM/yyyy}");
            sb.AppendLine($"Hora: {c.Hora:hh\\:mm}");
            
            // Local: Nome, capacidade total, quantidade de participantes e restantes
            int capacidade = c.Local?.Capacidade ?? 0;  // A capacidade total
            int participantesCount = c.Participantes.Count;  // A quantidade de participantes
            int capacidadeRestante = capacidade - participantesCount;  // Quantidade restante

            sb.AppendLine($"Local: {c.Local?.Nome ?? "N/A"}");
            sb.AppendLine($"Capacidade total: {capacidade}");
            sb.AppendLine($"Vagas restantes: {capacidadeRestante}");

            // Participantes
            if (participantesCount == 0)
                sb.AppendLine("Participantes: Nenhum");
            else
            {
                sb.AppendLine("Participantes:");
                foreach (var p in c.Participantes)
                    sb.AppendLine($"- {p.Nome}");
            }

            // Anotações
            if (c.Anotacoes.Count == 0)
                sb.AppendLine("Anotações: Nenhuma");
            else
            {
                sb.AppendLine("Anotações:");
                foreach (var a in c.Anotacoes)
                    sb.AppendLine($"- {a.Texto}");
            }
        }
    }
    return sb.ToString();
}

}
