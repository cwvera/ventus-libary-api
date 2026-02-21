namespace VentusLibrary.Commons.Responses;

/// <summary>
/// Respuesta estándar para comandos, incluyendo mensaje y datos opcionales.
/// </summary>
public class CommandResponse<T>
{
    /// <summary>Mensaje descriptivo del resultado de la operación.</summary>
    public string Message { get; set; } = string.Empty;
    /// <summary>Datos retornados por el comando, si aplica.</summary>
    public T? Data { get; set; }
}
