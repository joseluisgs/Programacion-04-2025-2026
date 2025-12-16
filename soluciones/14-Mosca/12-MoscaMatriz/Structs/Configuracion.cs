namespace _12_MoscaMatriz.Structs;

public struct Configuracion {
    public required int VidasJugador { get; init; } // Número de vidas del jugador
    public required int Tamaño { get; init; } // Número de filas en la matriz
    public required int VidasMosca { get; init; } // Número de vidas de la mosca
}