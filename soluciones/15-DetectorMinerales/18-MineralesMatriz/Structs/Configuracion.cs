namespace _18_MineralesMatriz.Structs;

public static class Configuracion {
    public static readonly int Size = 10; // Tamaño del mapa (Size x Size)
    public static readonly int MaxValue = 20; // Máximo mineral por casilla
    public static readonly int ProbMineral = 50; // Probabilidad (%) de que haya mineral al inicio
    public static readonly int MaxTime = 30; // Máximo de pasos de la simulación
    public static readonly int ProbTakeMineral = 50; // Probabilidad (%) de tomar mineral
    public static readonly int PauseTime = 1000; // Pausa entre pasos (ms)
    public static readonly int NumMineralsTaken = 2; // Cantidad de mineral extraída por intento
    public static readonly int ProbDecision = 30; // Probabilidad (%) de cambiar de dirección
}