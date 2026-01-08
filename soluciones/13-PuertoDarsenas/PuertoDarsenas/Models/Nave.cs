namespace PuertoDarsenas.Models;

// Es un registro (clases de solo datos) para representar una nave

/// <summary> Representa una nave en el contexto de un mundo de puerto de Darsenas. </summary>
public record class Nave {
    /// <summary> Tipo de nave: carga o batalla. Es un enumerado. Está anidada dentro de Nave. </summary>
    public enum TipoNave {
        Carga,
        Batalla
    }

    /// <summary> Identificador único de la nave </summary>
    public required string IdRepublica { get; init; }
    // Manifiesto? es un tipo anulable (null si la nave es de Batalla)

    /// <summary> Manifiesto de la nave. Es un tipo anulable (null si la nave es de Batalla). </summary>
    public required Manifiesto? Manifiesto { get; init; }

    /// <summary>Tipo de la nave: carga o batalla. </summary>
    public TipoNave Tipo { get; init; }
}