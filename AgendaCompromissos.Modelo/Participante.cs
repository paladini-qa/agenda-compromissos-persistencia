using System;

namespace AgendaCompromissos.Modelo;

public class Participante
{
    public string? Nome {get; set;}
    public List<Compromisso> Compromissos { get; set; } = new List<Compromisso>();
    
    public void AdicionarCompromisso(Compromisso compromisso)
    {
        if (!Compromissos.Contains(compromisso))
        {
            Compromissos.Add(compromisso);
            compromisso.Participantes.Add(this);
        }
    }

}
