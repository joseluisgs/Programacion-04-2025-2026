using System.Text.RegularExpressions;
using _12_MoscaMatriz.Enums;
using _12_MoscaMatriz.Model;
using _12_MoscaMatriz.Structs;

namespace _12_MoscaMatriz.Services;

public class JuegoMoscaService(
    // Lo que hacemos aquí es inyectar la configuración del juego en el servicio
    // Equivale a declarar configuración como una propiedad privada readonly
    // y haber creado el constructor con la inyección de la dependencia
    Configuracion configuracion
) {
    private readonly Mosca?[,] _matriz = new Mosca?[configuracion.Tamaño, configuracion.Tamaño];
    private readonly Mosca _mosca = new() { Vida = configuracion.VidasMosca };
    private readonly Random _random = Random.Shared;


    public Mosca.Estado JugarCazarMosca() {
        var intentos = 0;

        // Sorteamos la posición inicial de la mosca
        SortearPosicionMosca();

        // Bucle principal: se repite mientras la mosca no esté muerta Y queden intentos
        do {
            // this.ImprimirTablero();  // para depurar no seais tramposos
            intentos++;
            Console.WriteLine($"\n--- INTENTO {intentos} de {configuracion.VidasJugador} ---");

            // Pedimos la posición. GetLength(0) devuelve el número de filas (el tamaño).
            var posicion = PedirPosicionValida();

            // Analizamos el resultado del golpeo
            var resultado = AnalizarGolpeo(posicion);

            // Usamos switch para gestionar el flujo de juego según el resultado
            switch (resultado) {
                case Golpeo.Acertado:
                    _mosca.Golpear();
                    if (_mosca.EstadoActual == Mosca.Estado.Muerta) {
                        Console.WriteLine($"✅ ☠️ ¡TE LA HAS CARGADO! Has acertado en el intento {intentos}.");
                        _matriz[posicion.Fila, posicion.Columna] = null;
                    }
                    else {
                        Console.WriteLine($"✅ 🥊 ¡ACERTADO! Has golpeado a la mosca en el intento {intentos}.");
                        Console.WriteLine($"🪰 La mosca tiene {_mosca.Vida} vidas restantes.");
                        Console.WriteLine("🪰 ¡La mosca revolotea y CAMBIA de posición!");
                        LimpiarMatriz();
                        SortearPosicionMosca();
                    }

                    break;

                case Golpeo.Casi:
                    // ¡CASI! La mosca está en un lugar adyacente y se mueve.
                    Console.WriteLine($"💨 ¡CASI! Has estado cerca en el intento {intentos}.");
                    Console.WriteLine("🪰 ¡La mosca revolotea y CAMBIA de posición!");
                    // 1. Limpiamos la posición anterior (la mosca se va).
                    LimpiarMatriz();
                    // 2. Sorteamos una nueva posición.
                    SortearPosicionMosca();
                    break;

                case Golpeo.Fallado:
                    Console.WriteLine($"❌ Has fallado en el intento {intentos}.");
                    break;
            }
        } while (_mosca.EstadoActual == Mosca.Estado.Viva && intentos < configuracion.VidasJugador);

        return _mosca.EstadoActual;
    }

    public void ImprimirTablero() {
        var size = _matriz.GetLength(0);

        Console.Write("   "); // Espacio inicial para alinear con los números de fila
        for (var col = 1; col <= size; col++)
            // {0,5} garantiza que el número de columna ocupe 5 espacios
            // 0, es el argumento (col), 5 es el ancho total
            Console.Write("{0,6}", col);
        Console.WriteLine();

        // Línea separadora superior
        Console.Write("    +");

        // Mejor que el for
        Console.WriteLine(new string('-', size * 6 + 2) + "+");

        /*for (var i = 0; i < size; i++)
            Console.Write("-----"); // 5 guiones para cada celda
        Console.WriteLine("-+");*/

        // Recorremos e imprimimos filas
        for (var i = 0; i < size; i++) {
            // Número de fila (1-indexado), alineado a la derecha en 2 espacios + el separador " |" (Total 4 caracteres)
            Console.Write("{0,3} |", i + 1);

            for (var j = 0; j < size; j++) {
                // Comprobamos si el valor almacenado es el valor numérico del enum Mosca
                var celdaContenido = _matriz[i, j] != null ? " 🪰 " : "    ";

                // {0,5} de la columna: [ + celdaContenido(3) + ] = 5 espacios
                Console.Write("{0,6}", $"[{celdaContenido}]");
            }

            Console.WriteLine(" |"); // Cierre de la fila
        }

        // Línea separadora inferior
        Console.Write("    +");
        Console.WriteLine(new string('-', size * 6 + 2) + "+");
        Console.WriteLine();
    }

    private void LimpiarMatriz() {
        var filas = _matriz.GetLength(0);
        var columnas = _matriz.GetLength(1);

        // Almacenamos el valor entero de Vacio
        for (var i = 0; i < filas; i++) {
            for (var j = 0; j < columnas; j++)
                _matriz[i, j] = null;
        }
    }

    private void SortearPosicionMosca() {
        // 1. Aseguramos que el tablero está limpio antes de colocar una nueva mosca
        LimpiarMatriz();

        var size = _matriz.GetLength(0);

        // 2. Sorteamos la posición (0-indexada). 
        // Next(size) devuelve un valor entre 0 (inclusivo) y size (exclusivo), es decir, 0 a size-1.
        var posicionMoscaFila = _random.Next(0, size);
        var posicionMoscaColumna = _random.Next(size);

        // 3. Colocamos la mosca. Almacenamos el valor entero del enum.
        _matriz[posicionMoscaFila, posicionMoscaColumna] = _mosca;
    }

    private Golpeo AnalizarGolpeo(Posicion posicion) {
        // Obtenemos las dimensiones de la matriz
        var filas = _matriz.GetLength(0);
        var columnas = _matriz.GetLength(1);

        // 1. ACERTADO: Comprobación directa.
        // Comparamos el valor entero de la celda con el valor entero del enum (Celda.Mosca).
        if (_matriz[posicion.Fila, posicion.Columna] != null)
            return Golpeo.Acertado;

        // 2. CASI: Comprobación adyacente (vecindario 3x3).
        // Los bucles de -1 a 1 permiten iterar sobre todas las casillas adyacentes.
        for (var i = -1; i <= 1; i++) {
            for (var j = -1; j <= 1; j++) {
                // Omitimos el centro (i=0, j=0) porque ya se comprobó arriba
                if (i == 0 && j == 0) continue;

                var nuevaPosicion = new Posicion {
                    Fila = posicion.Fila + i,
                    Columna = posicion.Columna + j
                };

                // Verificamos que la nueva posición no esté fuera de los límites de la matriz (0 a size-1)
                if (nuevaPosicion.Fila >= 0 && nuevaPosicion.Fila < filas &&
                    nuevaPosicion.Columna >= 0 && nuevaPosicion.Columna < columnas)
                    // Si la mosca está en alguna de las casillas adyacentes, devolvemos CASI.
                    if (_matriz[nuevaPosicion.Fila, nuevaPosicion.Columna] != null)
                        return Golpeo.Casi;
            }
        }

        // 3. FALLADO: Si no se encontró en el punto ni adyacente.
        return Golpeo.Fallado;
    }

    private Posicion PedirPosicionValida() {
        var inputIsOk = false;
        var regexPosicion = new Regex($@"^([1-{configuracion.Tamaño}]):([1-{configuracion.Tamaño}])$");
        var nuevaFila = -1;
        var nuevaColumna = -1;

        do {
            Console.Write("Introduce una posición válida como fila:columna (ej. 3:5): ");
            // Leemos la entrada y quitamos espacios en blanco
            var input = Console.ReadLine()?.Trim() ?? "";

            // Intentamos casar la entrada con el patrón. Si es válido, inputRegex.IsMatch(input) es true.
            if (regexPosicion.IsMatch(input)) {
                // --- MÉTODO DE EXTRACCIÓN 1 (Usando Split, el método actual y más simple) ---
                // La Regex ya asegura que hay dos números separados por ':'
                var partes = input.Split(':');

                // Convertimos las partes a enteros de forma segura
                if (int.TryParse(partes[0], out var fila) && int.TryParse(partes[1], out var columna)) {
                    // Convertimos el valor (1-indexado por el usuario) a 0-indexado para la matriz
                    nuevaFila = fila - 1;
                    nuevaColumna = columna - 1;
                    inputIsOk = true;
                }
            }

            if (!inputIsOk)
                Console.WriteLine(
                    $"❌ La posición no es válida. Inténtalo de nuevo con valores entre 1 y {configuracion.Tamaño}.");
        } while (!inputIsOk);

        return new Posicion {
            Fila = nuevaFila,
            Columna = nuevaColumna
        };
    }
}