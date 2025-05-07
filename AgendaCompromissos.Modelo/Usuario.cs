using System;

namespace AgendaCompromissos.Modelo;

public class Usuario
{
  private readonly string _nome;
  private readonly List<Compromisso> _compromissos = new();
  public IReadOnlyCollection<Compromisso> Compromissos => _compromissos;
  public List<string> ErrosDeValidacao = [];

  public Usuario(string nome)
    {
        _nome = nome;
    }

  public void AdicionarCompromisso(Compromisso compromisso) 
  {
    ErrosDeValidacao.Clear();

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
        _compromissos.Add(compromisso);
      }
    }

}
