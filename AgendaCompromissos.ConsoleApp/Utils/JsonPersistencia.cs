using System.Text.Json;

namespace AgendaCompromissos.ConsoleApp.Utils;

public static class JsonPersistencia
{
  public static void Salvar<T>(List<T> dados, string caminho)
  {
    var json = JsonSerializer.Serialize(dados, new JsonSerializerOptions { WriteIndented = true });
    File.WriteAllText(caminho, json);
  }

  public static List<T> Carregar<T>(string caminho)
  {
    if (!File.Exists(caminho))
      return new List<T>();

    var json = File.ReadAllText(caminho);

    if (string.IsNullOrWhiteSpace(json)) // ← isso evita o erro
      return new List<T>();

    return JsonSerializer.Deserialize<List<T>>(json) ?? new List<T>();
  }
}
