namespace BaseDatosAlumnado.Models;

/// <summary>
///     Representa un alumno en la base de datos.
/// </summary>
public record Alumno {
    public int Id { get; init; } = 0;
    public required string Dni { get; init; }
    public required string NombreCompleto { get; init; }
    public double Nota { get; init; }
}