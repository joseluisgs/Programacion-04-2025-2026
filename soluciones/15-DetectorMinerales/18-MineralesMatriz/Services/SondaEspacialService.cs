using _18_MineralesMatriz.Models;
using _18_MineralesMatriz.Structs;

namespace _18_MineralesMatriz.Services;

public class SondaEspacialService {
    private readonly Mineral[,] _mapa;
    private readonly Random _random = Random.Shared;

    public SondaEspacialService() {
        _mapa = CrearMapa(Configuracion.Size, Configuracion.MaxValue, Configuracion.ProbMineral);
    }

    public Resultado IniciarExploracion() {
        var time = 1;
        var cantidadMineral = 0;

        // Inicializamos posición y dirección
        var direccionBusqueda = GetRandomDirection();
        var posicionActual = GetInitialPosition();

        do {
            Console.Clear(); // Limpia la consola para mostrar solo el paso actual
            Console.WriteLine($"--- Paso {time} / {Configuracion.MaxTime} ---");
            Console.WriteLine($"Mineral Recolectado: {cantidadMineral} 💎");
            Console.WriteLine(GetPosicionActual(posicionActual) + " 🤖");
            Console.WriteLine(GetDireccionBusqueda(direccionBusqueda));

            // Imprime el mapa con la barra de mineral por casilla
            PrintTablero(posicionActual);

            cantidadMineral += BuscarMineral(posicionActual);

            // Decisión de cambio de dirección
            if (time % 2 == 0)
                direccionBusqueda = GetAndThinkNewDirection(direccionBusqueda);

            // Evitar salir del mapa
            while (IsEndMap(posicionActual, direccionBusqueda)) {
                Console.WriteLine("⚠️ Límite alcanzado. Generando nueva dirección...");
                direccionBusqueda = GetRandomDirection();
            }

            // Movimiento
            posicionActual = GetNewPosicion(posicionActual, direccionBusqueda);

            // Dibujamos la barra de progreso **después del mapa**
            Console.WriteLine(); // Línea vacía antes de la barra
            DibujarBarraProgreso(time, Configuracion.MaxTime);

            Thread.Sleep(Configuracion.PauseTime);
            time++;
        } while (HayMineral() && time <= Configuracion.MaxTime);

        return new Resultado(time, cantidadMineral, GetPosicionActual(posicionActual));
    }


    private Mineral[,] CrearMapa(int size, int maxValue, int probMineral) {
        var mapa = new Mineral[size, size];
        for (var i = 0; i < size; i++)
            for (var j = 0; j < size; j++)
                mapa[i, j] = new Mineral(_random.Next(100) < probMineral ? _random.Next(1, maxValue + 1) : 0);
        return mapa;
    }

    private void PrintTablero(Posicion pos) {
        Console.WriteLine($"--- Tablero ({Configuracion.Size}x{Configuracion.Size}) ---");
        for (var i = 0; i < Configuracion.Size; i++) {
            for (var j = 0; j < Configuracion.Size; j++)
                if (i == pos.Fila && j == pos.Columna)
                    Console.Write("🤖"); // Robot
                else if (_mapa[i, j].Cantidad > 0)
                    Console.Write("⛏️"); // Pico, hay mineral
                else
                    Console.Write(" . "); // Vacío

            Console.WriteLine();
        }
    }

    private bool IsEndMap(Posicion pos, Direccion dir) {
        var nueva = new Posicion {
            Fila = pos.Fila + dir.Fila,
            Columna = pos.Columna + dir.Columna
        };
        return nueva.Fila < 0 || nueva.Fila >= Configuracion.Size || nueva.Columna < 0 ||
               nueva.Columna >= Configuracion.Size;
    }


    private int BuscarMineral(Posicion pos) {
        if (_mapa[pos.Fila, pos.Columna].Cantidad > 0) {
            Console.WriteLine($"⛏️ Mineral encontrado en f:{pos.Fila + 1}, c:{pos.Columna + 1}");
            if (_random.Next(0, 100) < Configuracion.ProbTakeMineral) {
                var tomar = Configuracion.NumMineralsTaken;
                if (_mapa[pos.Fila, pos.Columna].Cantidad < Configuracion.NumMineralsTaken)
                    tomar = _mapa[pos.Fila, pos.Columna].Cantidad;
                Console.WriteLine($"✅ Mineral tomado (-{tomar})");
                _mapa[pos.Fila, pos.Columna] = new Mineral(_mapa[pos.Fila, pos.Columna].Cantidad - tomar);
                return tomar;
            }

            Console.WriteLine("❌ Mineral no tomado (falló probabilidad)");
            return 0;
        }

        Console.WriteLine($"⚠️ No hay mineral en f:{pos.Fila + 1}, c:{pos.Columna + 1}");
        return 0;
    }

    public void PrintMapMinerales() {
        // Determinamos ancho fijo: por ejemplo 4 caracteres por celda "[xx]"
        for (var i = 0; i < Configuracion.Size; i++) {
            for (var j = 0; j < Configuracion.Size; j++) {
                if (_mapa[i, j].Cantidad > 0)
                    // Alineamos a la derecha el número dentro de los corchetes
                    Console.Write($"[{_mapa[i, j].Cantidad,2}]"); // 2 espacios para el número
                else
                    Console.Write("[  ]"); // mismo ancho aunque esté vacío
                Console.Write(" "); // espacio entre columnas
            }

            Console.WriteLine();
        }
    }

    private bool HayMineral() {
        for (var i = 0; i < Configuracion.Size; i++)
            for (var j = 0; j < Configuracion.Size; j++)
                if (_mapa[i, j].Cantidad > 0)
                    return true;
        return false;
    }

    private Posicion GetInitialPosition() {
        return new Posicion
            { Fila = _random.Next(0, Configuracion.Size), Columna = _random.Next(0, Configuracion.Size) };
    }

    private Direccion GetRandomDirection() {
        var nuevaDireccion = new Direccion {
            Fila = _random.Next(-1, 2), // -1,0,1
            Columna = _random.Next(-1, 2)
        };
        while (nuevaDireccion.Fila == 0 && nuevaDireccion.Columna == 0) {
            nuevaDireccion.Fila = _random.Next(-1, 2);
            nuevaDireccion.Columna = _random.Next(-1, 2);
        }

        return nuevaDireccion;
    }

    private Posicion GetNewPosicion(Posicion actual, Direccion direccion) {
        return new Posicion {
            Fila = actual.Fila + direccion.Fila,
            Columna = actual.Columna + direccion.Columna
        };
    }

    private Direccion GetAndThinkNewDirection(Direccion direccion) {
        if (_random.Next(0, 100) < Configuracion.ProbDecision) {
            Console.WriteLine("💭 He decidido cambiar de Dirección...");
            var nueva = GetRandomDirection();
            if (nueva.Fila == direccion.Fila && nueva.Columna == direccion.Columna) {
                Console.WriteLine("...No cambio de dirección");
                return direccion;
            }

            Console.WriteLine("↗️ Cambio de dirección");
            return nueva;
        }

        return direccion;
    }

    private string GetDireccionBusqueda(Direccion dir) {
        var simbolo = "Dirección de búsqueda: ";
        if (dir.Fila == -1 && dir.Columna == -1) simbolo += "↖️ NW";
        else if (dir.Fila == -1 && dir.Columna == 0) simbolo += "⬆️ N";
        else if (dir.Fila == -1 && dir.Columna == 1) simbolo += "↗️ NE";
        else if (dir.Fila == 0 && dir.Columna == -1) simbolo += "⬅️ W";
        else if (dir.Fila == 0 && dir.Columna == 1) simbolo += "➡️ E";
        else if (dir.Fila == 1 && dir.Columna == -1) simbolo += "↙️ SW";
        else if (dir.Fila == 1 && dir.Columna == 0) simbolo += "⬇️ S";
        else if (dir.Fila == 1 && dir.Columna == 1) simbolo += "↘️ SE";
        return simbolo;
    }

    public string GetPosicionActual(Posicion pos) {
        return $"Posición Actual: f:{pos.Fila + 1}, c:{pos.Columna + 1}";
    }

    private void DibujarBarraProgreso(int actual, int maximo) {
        var largo = 30; // ancho de la barra
        var porcentaje = actual / (double)maximo;
        var llenado = (int)(largo * porcentaje);

        // Construimos la barra
        var barra = new string('■', llenado).PadRight(largo, '─');

        // Color simulado con ANSI (opcional)
        string color;
        if (porcentaje < 0.5) color = "\u001b[32m"; // verde
        else if (porcentaje < 0.8) color = "\u001b[33m"; // amarillo
        else color = "\u001b[31m"; // rojo

        var reset = "\u001b[0m";

        // Imprimimos en la misma línea, es el /r
        Console.Write($"\r{color}[{barra}]{reset} {(int)(porcentaje * 100)}%");
    }
}