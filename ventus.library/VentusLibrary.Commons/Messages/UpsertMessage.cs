namespace VentusLibrary.Commons.Messages;

/// <summary>
/// Utilidades para generar mensajes de operaciones de creación, reactivación y actualización.
/// </summary>
public static class UpsertMessage
{
    /// <summary>
    /// Genera el mensaje para creación o reactivación de una entidad.
    /// </summary>
    /// <param name="entityName">Nombre de la entidad (ej.: Autor, Libro).</param>
    /// <param name="isReactivation">Indica si la operación corresponde a una reactivación.</param>
    /// <returns>Texto del mensaje de éxito.</returns>
    public static string ForCreate(string entityName, bool isReactivation) =>
        isReactivation ? $"{entityName} reactivado correctamente." : $"{entityName} creado correctamente.";

    /// <summary>
    /// Genera el mensaje para actualización de una entidad.
    /// </summary>
    /// <param name="entityName">Nombre de la entidad (ej.: Autor, Libro).</param>
    /// <returns>Texto del mensaje de éxito.</returns>
    public static string ForUpdate(string entityName) =>
        $"{entityName} actualizado correctamente.";
}
