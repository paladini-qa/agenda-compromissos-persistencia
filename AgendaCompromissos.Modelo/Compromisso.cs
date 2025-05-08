using System;

namespace AgendaCompromissos.Modelo;

public class Compromisso
{
    public DateTime Data {get; set;}
    public TimeSpan Hora {get; set;}
    public string Descricao{get; set;}
    public Usuario Usuario; // ASSOCIAÇÃO SIMPLES
    public Local Local; // ASSOCIAÇÃO SIMPLES
    public List<Participante> Participantes = []; // ASSOCIAÇÃO N:N
    public List<Anotacao> Anotacoes = []; // COMPOSIÇÃO
    public List<string> ErrosDeValidacao = [];

    public Compromisso(
        DateTime data,
        TimeSpan hora,
        string descricao,
        Usuario usuario,
        Local local)
    {
        if (!ValidarCompromisso(data, hora, descricao))
        {
            throw new ArgumentException(string.Join("\n", ErrosDeValidacao));
        }

        Data = data;
        Hora = hora;
        Descricao = descricao;
        Usuario = usuario;
        Local = local;
    } 

    public bool ValidarCompromisso(DateTime data, TimeSpan hora, string descricao) {
        ErrosDeValidacao.Clear(); 

        DateTime dataAtual = DateTime.Today.AddDays(1);
        
        if ( data < dataAtual ) 
        {
            ErrosDeValidacao.Add($"A data {data.ToString("dd/MM/yyyy")} precisa ser no mínimo {dataAtual.ToString("dd/MM/yyyy")}");
        }


        return ErrosDeValidacao.Count == 0;

 }

 public void AdicionarParticipante(Participante participante) {

        if (string.IsNullOrWhiteSpace(participante.Nome)) {
            throw new ArgumentException("O participante precisa ter um nome.");
        }

      int novaQuantidade = Participantes.Count + 1;
      if (!Local.ValidarCapacidade(novaQuantidade))  
        {
            throw new ArgumentException("O número de participantes ultrapassa a capacidade do local.");
        }
        
        Participantes.Add(participante);
    }

    public void AdicionarAnotacao(string texto) {

        if (string.IsNullOrWhiteSpace(texto)) {
            throw new ArgumentException("A anotação precisa estar preenchida.");
        }
        Anotacoes.Add(new Anotacao(texto));
    }

     //public override string ToString()
    //{
       // return $"\nData: {Data.ToString("dd/MM/yyyy")} \nCompromissos:\n ";
                
    //}

}
