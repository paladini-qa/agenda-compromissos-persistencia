using System;

namespace AgendaCompromissos.Modelo;

public class Compromisso
{
    public DateTime Data{get; set;}
    public TimeSpan Hora{get; set;}
    public required string Descricao{get; set;}
    public required Usuario Usuario; // ASSOCIAÇÃO SIMPLES
    public required Local Local;
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

 public bool ValidarCompromisso(DateTime data, TimeSpan hora, String descricao) {
        DateTime dataAtual = DateTime.Today.AddDays(1);
        if ( data < dataAtual ) {
             ErrosDeValidacao.Add($"A data {data.ToString("dd/MM/yyyy")} precisa ser no mínimo {dataAtual.ToString("dd/MM/yyyy")}");
        }
        if( descricao == null){
             ErrosDeValidacao.Add($"A descrição precisa estar preenchida");
        }
        if(hora.TotalHours < 0 || hora.TotalHours >= 24)
        {
            ErrosDeValidacao.Add($"A hora {hora.ToString("hh/:mm")} precisa estar entre 00:00 e 23:59)");
        }
        return ErrosDeValidacao.Count == 0;

 }

 public void AdicionarParticipante(Participante participante) {

        if (participante.Nome == null ) {
            throw new ArgumentNullException($"O participante precisa ter um nome");
        }
        Participantes.Add(participante);
    }

    public void AdicionarAnotacao(Anotacao anotacao) {

        if (anotacao.Texto == null ) {
            throw new ArgumentNullException($"A anotação precisa estar preenchida");
        }
        Anotacoes.Add(anotacao);
    }

}
