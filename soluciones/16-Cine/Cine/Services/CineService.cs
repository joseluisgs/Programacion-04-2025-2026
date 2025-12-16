using Cine.Config;
using Cine.Factories;
using Cine.Structs;
using Serilog;

namespace Cine.Services;

public class CineService {
    // Version A de Singleton
    private static CineService? _instance;

    // Versión B con Lazy<T>
    //private static readonly Lazy<CineService> Instance = new(() => new CineService());
    private readonly ILogger _log = Log.ForContext<CineService>();
    private readonly Butaca[,] _sala = new Butaca[Configuracion.TamFilas, Configuracion.TamColumnas];

    private CineService() {
        _log.Debug("Iniciando servicio de cine...");
        InitButacas();
    }

    public static CineService GetInstance() {
        // Método A de Singleton
        return _instance ??= new CineService();
        // Metodo con Lazy<T>
        //return Instance.Value;
    }

    private void InitButacas() {
        _log.Debug("Inicializando butacas...");

        var random = new Random();

        for (var i = 0; i < Configuracion.TamFilas; i++)
            for (var j = 0; j < Configuracion.TamColumnas; j++)
                _sala[i, j] = ButacasFactory.CreateButaca(new Posicion { Fila = i, Columna = j });

        var countFs = 0;
        while (countFs < Configuracion.NumButacasFueraServicio) {
            var f = random.Next(Configuracion.TamFilas); // Generar una fila aleatoria entre 0 y TamFilas-1
            var c = random.Next(Configuracion.TamColumnas); // Generar una columna aleatoria entre 0 y TamColumnas-1
            if (_sala[f, c].Estado != Butaca.Disponibilidad.FueraServicio) {
                // Como Butaca es inmutable, creamos una nueva instancia con el estado modificado
                _sala[f, c] = _sala[f, c] with { Estado = Butaca.Disponibilidad.FueraServicio };
                countFs++;
            }
        }
    }

    public void MostrarSala() {
        _log.Information("Mostrando sala de cine...");

        Console.WriteLine("\nEstado de la sala de cine\n");

        var ancho = 3; // fijo para todos

        // Usamos padLeft para alinear a la derecha

        // Encabezados de columnas
        Console.Write("    ");
        for (var i = 1; i <= Configuracion.TamColumnas; i++)
            Console.Write(i.ToString().PadLeft(ancho));

        Console.WriteLine();

        // Filas con butacas
        for (var fila = 0; fila < Configuracion.TamFilas; fila++) {
            Console.Write(" " + Utility.Utility.Letra(fila) +
                          "  "); // Esto funciona porque 'A' + 0 = 'A', 'A' + 1 = 'B', etc.

            for (var col = 0; col < Configuracion.TamColumnas; col++) {
                var icono = _sala[fila, col].Estado switch {
                    Butaca.Disponibilidad.Ocupada => "🔴",
                    Butaca.Disponibilidad.FueraServicio => "🚫",
                    _ => "" // Este caso no se usará porque lo manejamos abajo
                };
                // Debemos cambiar el icono por si esta libre, por el tipo de butaca
                if (_sala[fila, col].Estado == Butaca.Disponibilidad.Libre)
                    icono = _sala[fila, col].Categoria switch {
                        Butaca.Tipo.Vip => "🍾",
                        Butaca.Tipo.Discapacidad => "♿",
                        _ => "💺"
                    };

                Console.Write(icono.PadLeft(ancho));
            }

            Console.WriteLine();
        }

        Console.WriteLine();
    }


    public bool HayButacaPorEstado(Butaca.Disponibilidad estado) {
        _log.Information("Buscando butacas por estado: {estado}", estado);

        foreach (var b in _sala)
            if (b.Estado == estado)
                return true;
        return false;
    }

    public Butaca ButacaPorPosicion(Posicion posicion) {
        _log.Information("Buscando butaca por posición: ({posicion.Fila}, {posicion.Columna})", posicion.Fila,
            posicion.Columna);

        return _sala[posicion.Fila, posicion.Columna];
    }

    public bool IsOcupada(Posicion posicion) {
        _log.Information(
            "Verificando si la butaca en la posición ({posicion.Fila}, {posicion.Columna}) está ocupada",
            posicion.Fila, posicion.Columna);
        // No necesitamos el if, podemos devolver directamente la comparación booleana
        return _sala[posicion.Fila, posicion.Columna].Estado == Butaca.Disponibilidad.Ocupada;
    }

    // Obtiene una butaca aleatoria libre en la miSala
    public Entrada OcuparButaca(Posicion posicion) {
        _log.Information("Ocupando butaca en la posición ({posicion.Fila}, {posicion.Columna})", posicion.Fila,
            posicion.Columna);
        // miSala[posicion.Fila, posicion.Columna].Estado = Estado.Ocupada;
        // Recuerda que es inmutable, así que hay que crear una nueva instancia
        _sala[posicion.Fila, posicion.Columna] = _sala[posicion.Fila, posicion.Columna] with {
            Estado = Butaca.Disponibilidad.Ocupada
        };

        return GenerarEntrada(_sala[posicion.Fila, posicion.Columna]);
    }

    private Entrada GenerarEntrada(Butaca butaca) {
        _log.Debug("Generando entrada para la butaca en la posición ({posicion.Fila}, {posicion.Columna})",
            butaca.Posicion.Fila, butaca.Posicion.Columna);

        return new Entrada {
            Butaca = butaca,
            FechaCompra = DateTime.Now,
            Precio = butaca.Precio,
            Categoria = butaca.Categoria
        };
    }

    public void LiberarButaca(Posicion posicion) {
        _log.Information("Liberando butaca en la posición ({posicion.Fila}, {posicion.Columna})", posicion.Fila,
            posicion.Columna);
        // _sala[posicion.Fila, posicion.Columna].Estado = Butaca.Disponibilidad.Libre;
        // Recuerda que es inmutable, así que hay que crear una nueva instancia
        _sala[posicion.Fila, posicion.Columna] = _sala[posicion.Fila, posicion.Columna] with {
            Estado = Butaca.Disponibilidad.Libre
        };
    }

    public Informe InformeDeSala() {
        _log.Information("Generando informe de sala...");

        int vendidas = 0, libres = 0, fueraServicio = 0;
        foreach (var b in _sala)
            switch (b.Estado) {
                case Butaca.Disponibilidad.Ocupada:
                    vendidas++;
                    break;
                case Butaca.Disponibilidad.Libre:
                    libres++;
                    break;
                default:
                    fueraServicio++;
                    break;
            }

        var disponibles = vendidas + libres;
        var ocupacion = disponibles == 0 ? 0 : (double)vendidas / disponibles * 100;
        var recaudacion = CalcularRecaudacion();

        return new Informe {
            RecaudacionTotal = recaudacion,
            EntradasVendidas = vendidas,
            AsientosLibres = libres,
            AsientosFueraDeServicio = fueraServicio,
            PorcentajeDeOcupacion = ocupacion
        };
    }

    private decimal CalcularRecaudacion() {
        _log.Debug("Calculando recaudación...");
        decimal total = 0;
        foreach (var b in _sala)
            if (b.Estado == Butaca.Disponibilidad.Ocupada)
                total += b.Precio;
        return total;
    }

    public int CalcularButacasOcupadasOFueraServicio() {
        _log.Information("Calculando butacas ocupadas o fuera de servicio...");
        var contador = 0;
        foreach (var b in _sala)
            if (b.Estado == Butaca.Disponibilidad.Ocupada || b.Estado == Butaca.Disponibilidad.FueraServicio)
                contador++;
        return contador;
    }
}