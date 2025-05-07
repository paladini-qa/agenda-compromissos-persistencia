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
      _compromissos.Add(compromisso); 
  }
}
