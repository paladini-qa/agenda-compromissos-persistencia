using System;

namespace AgendaCompromissos.Modelo;

public class Anotacao
{
    public string? Texto { get; set; }
    public DateTime Data { get; set; }

    public Anotacao() { }

    public Anotacao(string texto)
    {
        Texto = texto;
        Data = DateTime.Now;
    }
    
    public override string ToString()
    {
        return $"{Data:dd/MM/yyyy HH:mm} - {Texto}";
    }
}