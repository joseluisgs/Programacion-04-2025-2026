using Aliens.Config;
using Aliens.Models;

namespace Aliens.Services;

/// <summary>
///     Servicio que simula la interacción de aliens en un espacio bidimensional
///     utilizando la técnica de Doble Buffer (Double Buffering) con arrays para
///     calcular el estado (T+1) a partir del estado actual (T).
/// </summary>
public class AliensSimuladorService {
    private readonly Alien?[] _aliensDisponibles;
    private readonly Random _random = Random.Shared;

    // Buffer de escritura (T+1). Se calcula el siguiente estado aquí.
    private Alien?[,] _back;

    // Buffer de lectura (T). Se lee el estado actual para tomar decisiones.
    private Alien?[,] _front;

    /// <summary>
    ///     Inicializa una nueva instancia del servicio de simulación.
    ///     Crea el espacio inicial y los dos buffers (front y back).
    /// </summary>
    public AliensSimuladorService() {
        _aliensDisponibles = new Alien[Configuracion.NumAliens];
        _front = CrearEspacio(Configuracion.SpaceSize, Configuracion.NumAliens);
        _back = new Alien?[Configuracion.SpaceSize, Configuracion.SpaceSize];
        Estado = new Marcador {
            Tiempo = 0,
            VidasJugador = Configuracion.Lives,
            AliensEliminados = 0
        };
    }

    // El estado del juego (vidas, tiempo, eliminados).
    public Marcador Estado { get; }


    // --------------------------------------------------------------------------------
    // PRINCIPAL
    // --------------------------------------------------------------------------------

    /// <summary>
    ///     Ejecuta el bucle principal de la simulación, alternando entre los buffers
    ///     para calcular el estado en cada ciclo de tiempo.
    /// </summary>
    public void Simulacion() {
        var size = _front.GetLength(0);

        // Aseguramos que _back tiene el tamaño correcto al inicio.
        if (_back.GetLength(0) != size || _back.GetLength(1) != size)
            _back = new Alien?[size, size];

        var simulacionActiva = true;
        do {
            // 0. Imprimir estado actual
            Console.Clear();
            Console.WriteLine($"\n--- ⏱️ Ciclo {Estado.Tiempo + 1} ---");
            PrintSpace();
            DibujarBarraProgreso(Estado.Tiempo, Configuracion.MaxTime);

            // 1. COPIA NECESARIA (Coherencia T+1)
            // Copiamos _front a _back para que _back contenga todos los elementos 
            // no modificados del estado T (los que no se mueven ni son disparados).
            Array.Copy(_front, _back, _front.Length); // Uso óptimo de Array.Copy

            // 2. Calcular siguiente estado: leer de '_front' y escribir en '_back'
            if (Estado.Tiempo % Params.TimeToDefend == 0)
                AimAndFire();

            if (Estado.Tiempo % Params.TimeToMove == 0) {
                MoveAliens();
                Console.WriteLine("➡️  Los aliens se han movido!");
            }

            if (Estado.Tiempo % Params.TimeToAttack == 0) {
                if (AlienAttack()) {
                    Estado.VidasJugador--;
                    Console.WriteLine("💥 Los aliens han atacado!");
                    Console.WriteLine($"Vidas restantes: {Estado.VidasJugador}");
                }
                else {
                    Console.WriteLine("❌ Los aliens han fallado al atacar!");
                }
            }

            // 3. Swap de referencias (Doble Buffer)
            // _back (T+1) pasa a ser _front (T) para la siguiente iteración
            (_front, _back) = (_back, _front);

            // 4. Incrementar tiempo y condiciones de fin
            Estado.Tiempo += 1;
            // La simulación termina si se excede el tiempo, si el jugador pierde las vidas, 
            // o si todos los aliens han sido eliminados.
            if (Estado.Tiempo >= Configuracion.MaxTime || Estado.VidasJugador <= 0 ||
                ContarAliensVivos() == 0) // Usar ContarAliensVivos() para el estado final correcto
                simulacionActiva = false;

            Thread.Sleep(Params.PauseTime);
        } while (simulacionActiva);

        Console.WriteLine();
    }

    // --------------------------------------------------------------------------------
    // ACCIONES DE LA SIMULACIÓN
    // --------------------------------------------------------------------------------

    /// <summary>
    ///     Mueve los aliens: lee la posición original de <c>_front</c> y escribe
    ///     la nueva posición en <c>_back</c>.
    /// </summary>
    private void MoveAliens() {
        var size = _front.GetLength(0);
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++)
                // 1. Verificamos que existe un alien en el presente (_front)
                // 2. Verificamos que NO haya sido eliminado ya en el futuro (_back) por un disparo.
                if (_front[i, j] != null && _back[i, j] != null)
                    MoveAlienToANewPosition(i, j);
        }
    }

    /// <summary>
    ///     Intenta mover el alien en la posición (fil, col) a una posición adyacente aleatoria.
    ///     Se garantiza la lectura de <c>_front</c> y la escritura en <c>_back</c>.
    ///     Incluye protección contra el borrado accidental de otros aliens.
    /// </summary>
    /// <param name="fil">Fila actual del alien.</param>
    /// <param name="col">Columna actual del alien.</param>
    private void MoveAlienToANewPosition(int fil, int col) {
        var size = _front.GetLength(0);
        var intentos = 0;
        var isStored = false;
        int newFil = fil, newCol = col;

        // Obtenemos la referencia del alien que queremos mover desde el buffer de LECTURA (_front)
        var alienToMove = _front[fil, col];

        do {
            // Generar desplazamiento aleatorio (-1, 0, 1) para fila y columna
            var df = _random.Next(-1, 2);
            var dc = _random.Next(-1, 2);

            // Si el desplazamiento es 0,0 (no moverse), pasamos al siguiente intento
            if (df == 0 && dc == 0) {
                intentos++;
                continue;
            }

            newFil = fil + df;
            newCol = col + dc;

            // Verificamos si la nueva posición es válida y si está vacía en el buffer de ESCRITURA (_back).
            if (IsValidPosition(newFil, newCol, size) && _back[newFil, newCol] == null) {
                // 1. Movemos el alien a la nueva posición en _back
                _back[newFil, newCol] = alienToMove;

                // -----------------------------------------------------------------------
                // 🛡️ CORRECCIÓN CRÍTICA (Simplificación de Limpieza del Origen)
                // -----------------------------------------------------------------------
                // Si el movimiento fue exitoso, limpiamos INCONDICIONALMENTE 
                // la celda de origen en el buffer de escritura.
                // La verificación de duplicación de zombies ya está en MoveAliens.
                _back[fil, col] = null;
                // -----------------------------------------------------------------------

                isStored = true;
            }

            intentos++;
        } while (!isStored && intentos < Params.MaxTriesToMove);

        if (!isStored)
            Console.WriteLine(
                $"⚠️ Alien id:{_front[fil, col]?.Id} en [{fil + 1},{col + 1}] no se ha podido mover (bloqueado).");
        else
            Console.WriteLine(
                $"➡️ Alien id:{_front[fil, col]?.Id} se desplaza desde [{fil + 1},{col + 1}] a [{newFil + 1},{newCol + 1}]");
    }

    /// <summary>
    ///     Intenta disparar a una posición aleatoria en el espacio.
    ///     Lee de <c>_front</c> y actualiza la vida del alien en <c>_back</c>.
    /// </summary>
    /// <returns>True si un alien fue dañado o eliminado, false si se falló o estaba vacío.</returns>
    private bool AimAndFire() {
        var size = _front.GetLength(0);
        Console.WriteLine("🎯 Apuntando...");
        var x = _random.Next(0, size); // Fila
        var y = _random.Next(0, size); // Columna

        // Disparo a la posición (x, y)
        Estado.NumDisparos++;

        // 1. Leemos el estado actual (_front) para ver si hay un alien allí.
        if (_front[x, y] == null) {
            Console.WriteLine($"Has disparado a [{x + 1},{y + 1}] y es una posición vacía.");
            return false;
        }

        // 2. Comprobamos la probabilidad de acierto.
        if (_random.Next(100) < Params.ProbAccuracy) {
            Console.WriteLine($"🎯 Has acertado en [{x + 1},{y + 1}]!");

            // 3. Actuamos en el buffer de escritura (_back)
            // Usamos el operador ? para evitar NullReferenceException
            _back[x, y]?.RecibirDisparo();
            Estado.NumDisparosAcertados++;

            // 4. Verificamos si el alien ha muerto
            if (_back[x, y]?.IsMuerto == true) {
                _back[x, y] = null; // Eliminamos el alien de _back
                Estado.AliensEliminados++; // Incrementamos el número de aliens eliminados
                Console.WriteLine($"💀 Alien id:{_back[x, y]?.Id} eliminado en [{x + 1},{y + 1}]!");
                return true;
            }

            // Si sobrevivió
            Console.WriteLine(
                $"👾 Alien id:{_back[x, y]?.Id} en [{x + 1},{y + 1}] ha recibido un disparo, le quedan {_back[x, y]?.Vidas} vidas.");
            return true;
        }

        // 5. Disparo fallido
        Console.WriteLine($"❌ Has fallado en [{x + 1},{y + 1}]!");
        return false;
    }

    /// <summary>
    ///     Determina si los aliens atacan al jugador en este ciclo, basado en la probabilidad.
    /// </summary>
    /// <returns>True si el ataque es exitoso, false en caso contrario.</returns>
    private bool AlienAttack() {
        return _random.Next(100) < Params.ProbAttack;
    }

    /// <summary>
    ///     Calcula el número exacto de aliens restantes escaneando el array actual (_front).
    /// </summary>
    public int ContarAliensVivos() {
        var count = 0;
        var size = _front.GetLength(0);
        for (var i = 0; i < size; i++) {
            for (var j = 0; j < size; j++)
                if (_front[i, j] != null)
                    count++;
        }

        return count;
    }

    // --------------------------------------------------------------------------------
    // UTILIDADES
    // --------------------------------------------------------------------------------

    /// <summary>
    ///     Inicializa el espacio de simulación colocando aliens aleatoriamente.
    /// </summary>
    /// <param name="size">Tamaño N del array N x N.</param>
    /// <param name="numAliens">Número de aliens a colocar.</param>
    /// <returns>Array bidimensional inicializado con aliens.</returns>
    private Alien?[,] CrearEspacio(int size, int numAliens) {
        var space = new Alien?[size, size];
        var colocados = 0;
        while (colocados < numAliens) {
            var x = _random.Next(0, size);
            var y = _random.Next(0, size);
            if (space[x, y] == null) {
                var alien = new Alien { Vidas = 1 }; // Asumo que Alien tiene un constructor o inicializador.
                // Se añade el alien al espacio y se incrementa el número de aliens colocados.
                _aliensDisponibles[colocados] = alien;
                space[x, y] = alien;
                colocados++;
            }
        }

        return space;
    }

    /// <summary>
    ///     Verifica si una posición (fila, columna) está dentro de los límites del espacio.
    /// </summary>
    /// <param name="fil">Fila a verificar.</param>
    /// <param name="col">Columna a verificar.</param>
    /// <param name="size">Tamaño N del array N x N.</param>
    /// <returns>True si la posición es válida, false en caso contrario.</returns>
    private bool IsValidPosition(int fil, int col, int size) {
        return fil >= 0 && fil < size && col >= 0 && col < size;
    }

    /// <summary>
    ///     Imprime el espacio de simulación actual (<c>_front</c>) en la consola.
    /// </summary>
    public void PrintSpace() {
        var n = _front.GetLength(0);
        for (var i = 0; i < n; i++) {
            for (var j = 0; j < n; j++)
                if (_front[i, j] != null)
                    // ⚠️ CORRECCIÓN DE VISUALIZACIÓN: Se añade un espacio para alinear el emoji.
                    Console.Write("👾 ");
                else
                    // ⚠️ CORRECCIÓN DE VISUALIZACIÓN: Se añade un espacio para alinear el emoji.
                    Console.Write("◻️ ");
            Console.WriteLine();
        }
    }

    /// <summary>
    ///     Obtiene un array con los aliens que han sido eliminados hasta el momento.
    /// </summary>
    /// <returns></returns>
    public Alien[] AliensMuertos() {
        var muertos = new Alien[Estado.AliensEliminados];
        var index = 0;
        foreach (var alien in _aliensDisponibles)
            if (alien != null && alien.IsMuerto) {
                muertos[index] = alien;
                index++;
            }

        OrdenarAlienes(muertos);
        return muertos;
    }

    /// <summary>
    ///     Ordena los aliens eliminados por el momento de su muerte.
    /// </summary>
    /// <param name="muertos">Vector de Aliens muertos</param>
    private void OrdenarAlienes(Alien[] muertos) {
        // Ordenación simple por el momento de muerte (burbuja)
        for (var i = 0; i < muertos.Length - 1; i++) {
            for (var j = 0; j < muertos.Length - i - 1; j++)
                if (muertos[j].MomentoDeMuerte > muertos[j + 1].MomentoDeMuerte)
                    // Intercambio
                    (muertos[j], muertos[j + 1]) = (muertos[j + 1], muertos[j]);
        }
    }

    /// <summary>
    ///     Dibuja una barra de progreso que indica el avance de la simulación.
    /// </summary>
    /// <param name="actual">Valor actual del progreso (tiempo).</param>
    /// <param name="maximo">Valor máximo del progreso (tiempo máximo).</param>
    private void DibujarBarraProgreso(int actual, int maximo) {
        if (maximo <= 0) maximo = 1;
        var porcentaje = actual / (double)maximo;
        porcentaje = Math.Clamp(porcentaje, 0.0, 1.0); // Asegurar rango [0,1]
        var llenado = (int)(Params.ProgressBarWidth * porcentaje);
        var barra = new string('■', llenado).PadRight(Params.ProgressBarWidth, '─');

        // Códigos ANSI para colores
        var color = porcentaje < 0.5 ? "\u001b[32m" : porcentaje < 0.8 ? "\u001b[33m" : "\u001b[31m";
        var reset = "\u001b[0m";

        Console.Write($"\r{color}[{barra}]{reset} {(int)(porcentaje * 100)}%\n");
    }
}