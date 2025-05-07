using System;

namespace AgendaCompromissos.Modelo;

public class Anotacao
{
    public string? Texto {get; set;}
    public DateTime Data {get; set;}

    public Anotacao(string texto)
    {
        Texto = texto;
        Data = DateTime.Now;
    }
}
