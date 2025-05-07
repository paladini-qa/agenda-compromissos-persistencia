using System;

namespace AgendaCompromissos.Modelo;

public class Usuario
{
  //private DateTime _dataAtual = DateTime.Today.AddDays(1);
  private string _nome {get; set;}
  private readonly List<Compromisso> _compromisso = [];
  public IReadOnlyList<Compromisso> Compromissos
    {
        get
        {
            return _compromisso;
        }
    }

    public List<string> ErrosDeValidacao = [];

    public Usuario(string nome)
    {
        _nome = nome;
    }

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

}
