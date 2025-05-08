using System;

namespace AgendaCompromissos.Modelo;

public class Local
{
    public string Nome {get; set;}
    public int Capacidade {get; set;}
    public List<string> ErrosDeValidacao = [];

    public Local(string nome, int capacidade)
    {
        
        if (!ValidarCapacidade(capacidade))
        {
            throw new ArgumentException(string.Join("\n", ErrosDeValidacao));
        }
        Nome = nome;
        Capacidade = capacidade;
    }

    // Método para validar a capacidade
    public bool ValidarCapacidade(int quantidade)
    {
        ErrosDeValidacao.Clear(); 

        if (quantidade > Capacidade)
        {
            ErrosDeValidacao.Add("A capacidade deve ser de no mínimo 1.");
        }

        return ErrosDeValidacao.Count == 0; 
    }
}

