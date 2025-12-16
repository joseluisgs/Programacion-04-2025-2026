using System.Globalization;

namespace Cine.Config;

public static class Configuracion {
    public static readonly int TamFilas = 5;
    public static readonly int TamColumnas = 8;
    public static readonly int NumButacasFueraServicio = 3;
    public static readonly decimal PrecioButacaVip = 8.75m;
    public static readonly decimal PrecioButacaEstandar = 5.50m;
    public static readonly decimal PrecioButacaDiscapacitados = 4.00m;
    public static readonly CultureInfo Locale = new("es-ES");
}