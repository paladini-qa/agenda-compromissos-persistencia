using System;

namespace AgendaCompromissos.Modelo;

public class Participante
{
    public string? Nome { get; set; }

    private readonly List<Compromisso> _compromissos = new();
    public IReadOnlyList<Compromisso> Compromissos => _compromissos;

    public void AdicionarCompromisso(Compromisso compromisso)
    {
        if (!_compromissos.Contains(compromisso))
        {
            _compromissos.Add(compromisso);
        }
    }
}
