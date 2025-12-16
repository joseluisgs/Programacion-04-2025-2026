namespace PuertoDarsenas.Models;

// Estructura para la información de Carga
// Es una estructura porque es un dato simple

/// <summary> Estructura para la información de Carga </summary>
public struct Manifiesto {
    public int PesoToneladas { get; init; }
    public decimal ValorEuros { get; init; }
}