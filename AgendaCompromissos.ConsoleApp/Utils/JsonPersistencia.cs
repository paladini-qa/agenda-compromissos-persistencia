using System.Text.Json;
using System.Text.Json.Serialization;

namespace AgendaCompromissos.ConsoleApp.Utils;

public static class JsonPersistencia
{
  private static JsonSerializerOptions options = new JsonSerializerOptions
  {
    WriteIndented = true,
    ReferenceHandler = ReferenceHandler.Preserve //suporte a referencias
  };
  public static void Salvar<T>(List<T> dados, string caminho)
  {    
    var json = JsonSerializer.Serialize(dados, options);
    File.WriteAllText(caminho, json);
  }
  public static List<T> Carregar<T>(string caminho)
  {
    if (!File.Exists(caminho))
      return new List<T>();

    var json = File.ReadAllText(caminho);

    if (string.IsNullOrWhiteSpace(json))
      return new List<T>();

    return JsonSerializer.Deserialize<List<T>>(json, options) ?? new List<T>();
  }
}
