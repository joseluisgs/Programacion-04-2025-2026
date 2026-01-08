using System.Globalization;

namespace PuertoDarsenas.Utils;

/// <summary> Clase estática con utilidades comunes para la aplicación </summary>
public static class Utilidades {
    // Expresiones regulares para validación de formatos
    public static string RegexId = @"^[A-Z]{3}\d{3}[A-Z]$"; // LLLNNNL
    public static string RegexTipo = @"^(carga|batalla)$"; // carga o batalla
    public static string RegexPeso = @"^(10000|[1-9]\d{2,3})$"; // 100 a 10000
    public static string RegexValor = @"^\d{1,6}(\,|\.){1}\d{2}$"; // X,XX o XXXXXX,XX, siempre 2 decimales

    // Cultura española para formateo de números y moneda
    public static CultureInfo LocaleEs = new("es-ES");
}