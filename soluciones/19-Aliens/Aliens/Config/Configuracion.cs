using System.Globalization;

namespace Aliens.Config;

// Struct para almacenar la configuración de la simulación (solo datos)
public static class Configuracion {
    public static int SpaceSize { get; set; } = Params.DefaultSpaceSize;
    public static int NumAliens { get; set; } = Params.DefaultNumAliens;
    public static int Lives { get; set; } = Params.DefaultLives;
    public static int MaxTime { get; set; } = Params.DefaultMaxTime;
    public static int AliensVida { get; set; } = Params.DefaultAliensVida;

    public static CultureInfo Locale { get; set; } = CultureInfo.GetCultureInfo("es-ES");
}