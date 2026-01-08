using Cine.Structs;
using Serilog;

namespace Cine.Factories;

public static class ButacasFactory {
    private static readonly ILogger _logger = Log.ForContext(typeof(ButacasFactory)); // Porque la clase es estática

    public static Butaca CreateButaca(Posicion posicion,
        Butaca.Disponibilidad disponibilidad = Butaca.Disponibilidad.Libre) {
        // Soretamos por tipo y creamos la butaca según los parámetros
        var random = Random.Shared.Next(0, 100);
        var tipo = random switch {
            < 50 => Butaca.Tipo.Estandar, // 50% de probabilidad
            < 80 => Butaca.Tipo.Vip, // 30% de probabilidad
            _ => Butaca.Tipo.Discapacidad // 20% de probabilidad
        };
        _logger.Debug("Butaca creada en fila {Fila}, columna {Columna} como {Tipo} y estado {Estado}",
            posicion.Fila, posicion.Columna, tipo, disponibilidad);
        return new Butaca {
            Posicion = posicion,
            Categoria = tipo,
            Estado = disponibilidad
        };
    }
}