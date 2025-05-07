using System;

namespace AgendaCompromissos.Modelo;

public class Local
{
    public string? Nome {get; set;}
    public int Capacidade {get; set;}

    public void ValidarCapacidade(int quantidade)
    {
        if (quantidade > Capacidade)
        {
            throw new InvalidOperationException("A quantidade ultrapassa a capacidade do local.");
        }
    }
}

