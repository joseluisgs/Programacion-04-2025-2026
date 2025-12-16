using Cine.Config;

namespace Cine.Structs;

public record Butaca {
    public enum Disponibilidad {
        // [💺] Disponible para la venta
        Libre = 0,

        // [🔴] Butaca vendida/ocupada
        Ocupada = 1,

        // [🚫] Butaca no disponible (por ejemplo, por avería)
        FueraServicio = 2
    }

    public enum Tipo {
        // [💺] Butaca estándar
        Estandar = 0,

        // [🍾] Butaca VIP
        Vip = 1,

        // [♿] Butaca para personas con discapacidad
        Discapacidad = 2
    }

    public required Posicion Posicion { get; init; }
    public required Disponibilidad Estado { get; set; }
    public required Tipo Categoria { get; init; }

    public decimal Precio => Categoria switch {
        Tipo.Vip => Configuracion.PrecioButacaVip,
        Tipo.Estandar => Configuracion.PrecioButacaEstandar,
        Tipo.Discapacidad => Configuracion.PrecioButacaDiscapacitados,
        _ => throw new ArgumentOutOfRangeException()
    };
}