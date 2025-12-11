namespace Aliens.Config;

/// <summary>
///     Clase estática que contiene los parámetros por defecto de la simulación.
/// </summary>
public static class Params {
    public const int DefaultSpaceSize = 10;
    public const int DefaultNumAliens = 20;
    public const int DefaultLives = 5;
    public const int DefaultMaxTime = 60;
    public const int DefaultAliensVida = 1;

    public const int TimeToDefend = 2;
    public const int TimeToMove = 3;
    public const int TimeToAttack = 5;

    public const int PauseTime = 1000;
    public const int MaxTriesToMove = 10;
    public const int ProbAccuracy = 70;
    public const int ProbAttack = 40;

    public const int ProgressBarWidth = 30;
}