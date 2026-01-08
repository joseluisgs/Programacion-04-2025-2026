using System.Globalization;
using System.Text.RegularExpressions;
using PuertoDarsenas.Models;
using PuertoDarsenas.Structs;
using PuertoDarsenas.Utils;
using Serilog;

namespace PuertoDarsenas.Services;

/// <summary> Servicio para la gestión del puerto espacial. </summary>
public class ServicioPuerto {
    private const int MaxDarsenas = 4; // A, B, C, D
    private const int MaxPuertas = 7; // 1 a 7

    // Matriz 4x7 de naves (Nave? para permitir null)
    private readonly Nave?[,] _puerto = new Nave?[MaxDarsenas, MaxPuertas];

    /// <summary> Obtiene el carácter correspondiente a la dársena a partir de su índice. </summary>
    /// <param name="indice">Índice de la dársena (0-3)</param>
    /// <returns>Carácter de la dársena (A-D)</returns>
    private char IndiceADarsena(int indice) {
        return (char)('A' + indice);
    }

    /// <summary> Cuenta el número de naves actualmente ocupando puertas en el puerto. </summary>
    /// <returns>Número de naves ocupadas.</returns>
    public int ContarNavesOcupadas() {
        var ocupadas = 0;
        for (var i = 0; i < MaxDarsenas; i++) {
            for (var j = 0; j < MaxPuertas; j++)
                // Si es distinto de null, está ocupada
                if (_puerto[i, j] != null)
                    ocupadas++;
        }

        return ocupadas;
    }

    /// <summary> Busca la posición de una nave por su ID y devuelve si se encontró y la posición. </summary>
    /// <param name="id">Id de la nave a buscar</param>
    /// <returns>Tupla con (encontrada (bool), Posicion? (fila, columna)</returns>
    private (bool encontrada, Posicion? pos) BuscarPosicion(string id) {
        for (var i = 0; i < MaxDarsenas; i++) {
            for (var j = 0; j < MaxPuertas; j++)
                // Uso de is { } para verificar que el Nave? no es null antes de acceder a IdRepublica
                if (_puerto[i, j]?.IdRepublica == id)
                    return (true, new Posicion { Fila = i, Columna = j });
        }

        return (false, null);
    }

    /// <summary> Asigna la primera puerta libre a una nueva nave. </summary>
    /// <param name="nuevaNave">Nave a asignar</param>
    /// <returns>Tupla con éxito (bool) y posición asignada (Posicion?)</returns>
    private (bool Exito, Posicion? posicion) AsignarPrimeraPuertaLibre(Nave nuevaNave) {
        Log.Debug("Buscando primera puerta libre para asignación.");
        for (var i = 0; i < MaxDarsenas; i++) {
            for (var j = 0; j < MaxPuertas; j++)
                if (_puerto[i, j] == null) {
                    _puerto[i, j] = nuevaNave;
                    Log.Information("Nave {ID} asignada en {Darsena}:{Puerto}", nuevaNave.IdRepublica,
                        IndiceADarsena(i), j + 1);
                    return (true, new Posicion { Fila = i, Columna = j });
                }
        }

        return (false, null); // Puerto lleno
    }

    /// <summary> Muestra el estado actual del puerto espacial. </summary>
    public void VerEstadoPuerto() {
        Log.Debug("Entrando a la función VerEstadoPuerto.");
        var ocupadas = ContarNavesOcupadas();
        Console.WriteLine("\n🗺️ --- MAPA DEL PUERTO DE CARGA ---");
        Console.Write("Dársena | ");
        for (var j = 1; j <= MaxPuertas; j++)
            Console.Write($" P{j} |");
        Console.WriteLine("\n--------+" + new string('-', MaxPuertas * 5));

        for (var i = 0; i < MaxDarsenas; i++) {
            Console.Write($"   {IndiceADarsena(i)}    | ");
            for (var j = 0; j < MaxPuertas; j++)
                // Operador ternario para elegir el emoji, if else simplificado
                Console.Write(_puerto[i, j] != null ? " [🛸] |" : " [◽] |"); // Espacio libre
            // Emoji para nave ocupada
            Console.WriteLine();
        }

        var totalPlazas = MaxDarsenas * MaxPuertas;
        Console.WriteLine($"\n✅ Plazas Libres: {totalPlazas - ocupadas} | 🅿️ Plazas Ocupadas: {ocupadas}");
        Log.Information("Estado del puerto visualizado.");
    }

    /// <summary> Asigna una puerta a una nueva nave tras validaciones y confirmación. </summary>
    public void AsignarPuerta() {
        Log.Debug("Entrando a la función AsignarPuerta.");
        if (ContarNavesOcupadas() >= MaxDarsenas * MaxPuertas) {
            Log.Warning("Intento de asignación: puerto lleno.");
            Console.WriteLine("🛑 ¡El puerto está lleno! Intento de asignación fallido.");
            return;
        }

        var id = LeerIdNave();

        if (BuscarPosicion(id).encontrada) {
            Log.Warning("Nave con ID {ID} ya se encuentra en el puerto.", id);
            Console.WriteLine($"❌ La nave con ID {id} ya está en el puerto.");
            return;
        }

        var tipo = LeerTipoNave();
        var manifiesto = LeerManifiestoNave(tipo);

        // Confirmación S/N
        Console.Write("¿Desea almacenar la nave (S/N)? ");
        var key = Console.ReadKey();
        if (key.KeyChar.ToString().ToUpper() != "S") {
            Console.WriteLine("\n🚫 Asignación cancelada por el usuario.");
            Log.Information("Asignación de nave {ID} cancelada por el usuario.", id);
            return;
        }

        Console.WriteLine();

        var nuevaNave = new Nave { IdRepublica = id, Tipo = tipo, Manifiesto = manifiesto };
        var (exito, posicion) = AsignarPrimeraPuertaLibre(nuevaNave);

        if (exito && posicion is { } pos) {
            var darsena = IndiceADarsena(pos.Fila);
            var puertoNum = pos.Columna + 1;
            Console.WriteLine($"✅ Nave **{id}** asignada en la posición: **{darsena}:{puertoNum}**");
        }
        else {
            Log.Error("Error lógico: Intento de asignación fallido en puerto lleno.");
            Console.WriteLine("🛑 Error crítico al intentar asignar la nave.");
        }
    }

    /// <summary> Lee y valida el manifiesto de una nave de tipo Carga. </summary>
    /// <param name="tipo">Tipo de nave</param>
    /// <returns>Manifiesto válido o null</returns>
    private Manifiesto? LeerManifiestoNave(Nave.TipoNave tipo) {
        Manifiesto? manifiesto = null;
        // Early return si no es de tipo Carga
        if (tipo != Nave.TipoNave.Carga)
            return manifiesto;

        var peso = LeerPesoManifiesto();

        var valor = LeerValorManifiesto();

        manifiesto = new Manifiesto { PesoToneladas = peso, ValorEuros = valor };

        return manifiesto;
    }

    /// <summary> Lee y valida el valor del manifiesto introducido por el usuario. </summary>
    /// <returns>Valor válido del manifiesto</returns>
    private decimal LeerValorManifiesto() {
        var valorOk = false;
        var valor = 0.0M;
        do {
            Console.Write("Ingrese VALOR total (99,99 - 999.999,99€): ");
            var valorStr = Console.ReadLine() ?? "";

            if (!Regex.IsMatch(valorStr, Utilidades.RegexValor)) {
                Log.Error("Valor inválido: Formato incorrecto {Valor}", valorStr);
                Console.WriteLine("⚠️ Valor inválido. Use el formato X,XX o XXXXXX,XX (con coma decimal).");
                valorOk = false;
            }

            if (!decimal.TryParse(valorStr, NumberStyles.Currency, Utilidades.LocaleEs, out valor) ||
                valor < 99.99M ||
                valor > 999999.99M) {
                Log.Error("Valor inválido: Fuera de rango {Valor}", valorStr);
                Console.WriteLine("⚠️ Valor inválida. Debe estar entre 99,99€ y 999.999,99€.");
                valorOk = false;
            }

            valorOk = true;
        } while (!valorOk);

        return valor;
    }

    /// <summary> Lee y valida el peso del manifiesto introducido por el usuario. </summary>
    /// <returns>Peso válido del manifiesto</returns>
    private int LeerPesoManifiesto() {
        var pesoOk = false;
        var peso = 0;
        do {
            Console.Write("Ingrese PESO total (100-10000 toneladas): ");
            var pesoStr = Console.ReadLine()?.Trim() ?? "";

            if (!Regex.IsMatch(pesoStr, Utilidades.RegexPeso) || !int.TryParse(pesoStr, out peso)) {
                Log.Error("Peso inválido en bucle: {Peso}", pesoStr);
                Console.WriteLine("⚠️ Peso inválido. Debe ser un número entre 100 y 10000.");
                pesoOk = false;
            }
            else {
                pesoOk = true;
            }
        } while (!pesoOk);

        return peso;
    }

    /// <summary> Lee y valida el tipo de nave introducido por el usuario. </summary>
    /// <returns>Tipo de nave válido</returns>
    private Nave.TipoNave LeerTipoNave() {
        var tipoOk = false;
        var tipoStr = "";

        do {
            Console.Write("Ingrese tipo de nave (Carga/Batalla): ");
            tipoStr = Console.ReadLine()?.Trim()?.ToLower() ?? "";

            if (!Regex.IsMatch(tipoStr, Utilidades.RegexTipo)) {
                Log.Error("Tipo de nave inválido: {Tipo}", tipoStr);
                Console.WriteLine("⚠️ Tipo de nave inválido. Use 'Carga' o 'Batalla'.");
                tipoOk = false;
            }
            else {
                tipoOk = true;
            }
        } while (!tipoOk);

        var tipo = (Nave.TipoNave)Enum.Parse(typeof(Nave.TipoNave), tipoStr, true);
        return tipo;
    }

    /// <summary> Lee y valida el ID de la nave introducido por el usuario. </summary>
    /// <returns>ID de nave válido</returns>
    private string LeerIdNave() {
        var idOk = false;
        var id = "";
        do {
            Console.Write("Ingrese ID de la nave (LLLNNNL): ");
            id = Console.ReadLine()?.Trim()?.ToUpper() ?? "";

            if (!Regex.IsMatch(id, Utilidades.RegexId)) {
                Log.Error("ID inválido en asignación: {ID}", id);
                Console.WriteLine("⚠️ ID inválido. Debe ser LLLNNNL.");
                idOk = false;
            }
            else {
                idOk = true;
            }
        } while (!idOk);

        return id;
    }

    /// <summary> Busca una nave por su ID y muestra su posición si existe. </summary>
    public void BuscarNave() {
        Log.Debug("Entrando a la función BuscarNave.");
        Console.Write("Ingrese ID de la nave a buscar: ");
        var id = Console.ReadLine()?.Trim()?.ToUpper() ?? "";
        var resultado = BuscarPosicion(id);

        if (resultado.encontrada && resultado.pos is { } pos) {
            var darsena = IndiceADarsena(pos.Fila);
            var puertoNum = pos.Columna + 1;
            Console.WriteLine($"🎯 Nave **{id}** encontrada en la posición: **{darsena}:{puertoNum}**");
            Log.Information("Nave {ID} encontrada en {Darsena}:{Puerto}", id, darsena, puertoNum);
        }
        else {
            Console.WriteLine($"❓ La nave **{id}** no está en el puerto.");
            Log.Information("Búsqueda: Nave {ID} no encontrada.", id);
        }
    }

    /// <summary> Permite que una nave abandone el puerto tras confirmación. </summary>
    public void DespegarNave() {
        Log.Debug("Entrando a la función DespegarNave.");
        Console.Write("Ingrese ID de la nave que despega: ");
        var id = Console.ReadLine()?.ToUpper() ?? "";
        var resultado = BuscarPosicion(id);

        if (resultado.encontrada && resultado.pos is { } pos) {
            var darsena = IndiceADarsena(pos.Fila);
            var puertoNum = pos.Columna + 1;

            // Confirmación S/N
            Console.Write($"¿Desea que la nave {id} abandone el puerto (S/N)? ");
            var key = Console.ReadKey();
            if (key.KeyChar.ToString().ToUpper() != "S") {
                Console.WriteLine("\n🚫 Despegue cancelado por el usuario.");
                Log.Information("Despegue de nave {ID} cancelado por el usuario.", id);
                return;
            }

            Console.WriteLine();

            // Liberación
            _puerto[pos.Fila, pos.Columna] = null;

            Log.Warning("Nave {ID} despegó de {Darsena}:{Puerto}", id, darsena, puertoNum);
            Console.WriteLine($"✅ Nave **{id}** ha despegado. Posición {darsena}:{puertoNum} libre.");
        }
        else {
            Log.Error("Despegue fallido: Nave {ID} no encontrada.", id);
            Console.WriteLine($"❌ La nave **{id}** no está en el puerto.");
        }
    }

    /// <summary> Extrae todas las naves ocupadas en el puerto a un array temporal. </summary>
    /// <param name="ocupadas">Cantidad de naves ocupadas</param>
    /// <returns>Array de naves ocupadas</returns>
    private Nave[] ExtraerNavesOcupadas(int ocupadas) {
        Log.Debug("Extrayendo naves ocupadas a un array temporal.");
        var listaTemporal = new Nave[ocupadas];
        var k = 0;
        for (var i = 0; i < MaxDarsenas; i++) {
            for (var j = 0; j < MaxPuertas; j++)
                if (_puerto[i, j] is { } nave)
                    listaTemporal[k++] = nave;
        }

        return listaTemporal;
    }

    /// <summary> Ordena el array de naves por valor descendente usando Bubble Sort. </summary>
    /// <param name="listaTemporal">Array de naves a ordenar</param>
    private void OrdenarNavesPorValorDescendente(Nave[] listaTemporal) {
        Log.Debug("Iniciando ordenación por Burbuja (Bubble Sort) descendente por valor.");
        var ocupadas = listaTemporal.Length;

        for (var i = 0; i < ocupadas - 1; i++) {
            for (var j = 0; j < ocupadas - 1 - i; j++) {
                var naveA = listaTemporal[j];
                var naveB = listaTemporal[j + 1];

                var swap = false;

                var valorA = naveA.Manifiesto?.ValorEuros ?? 0.0M;
                var valorB = naveB.Manifiesto?.ValorEuros ?? 0.0M;

                // Reglas de comparación DESCENDENTE por VALOR (NULL/0 va al final)
                if (valorA == 0.0M && valorB > 0.0M)
                    swap = true;
                else if (valorA > 0.0M && valorB > 0.0M && valorA < valorB) swap = true;

                if (swap) {
                    (listaTemporal[j], listaTemporal[j + 1]) = (listaTemporal[j + 1], listaTemporal[j]);
                    Log.Debug("Intercambio: {ID_A} <-> {ID_B}", naveA.IdRepublica, naveB.IdRepublica);
                }
            }
        }
    }

    /// <summary> Muestra el listado ordenado de naves con sus datos y posición. </summary>
    /// <param name="listaTemporal">Array de naves ordenadas</param>
    private void MostrarListadoOrdenado(Nave[] listaTemporal) {
        Log.Debug("Mostrando el listado ordenado.");
        Console.WriteLine("\n📜 --- LISTADO DE NAVES (Orden Descendente por Valor) --- 💰");
        Console.WriteLine("ID\t\tTIPO\t\tVALOR (€)\tPESO (t)\tPOSICIÓN");
        Console.WriteLine("---------------------------------------------------------------------");

        foreach (var nave in listaTemporal) {
            var valorStr = nave.Manifiesto?.ValorEuros.ToString("C", Utilidades.LocaleEs) ?? "N/A";
            var pesoStr = nave.Manifiesto?.PesoToneladas.ToString("N0", Utilidades.LocaleEs) ?? "N/A";

            var asignacion = BuscarPosicion(nave.IdRepublica);
            if (asignacion.encontrada && asignacion.pos is { } position) {
                var darsena = IndiceADarsena(position.Fila);
                var puertoNum = position.Columna + 1;
                var tipoEmoji = nave.Tipo == Nave.TipoNave.Carga ? "📦" : "⚔️";
                Console.WriteLine(
                    $"{nave.IdRepublica}\t{tipoEmoji} {nave.Tipo}\t{valorStr}\t{pesoStr}\t\t{darsena}:{puertoNum}");
            }
            else {
                Log.Error("Error lógico: Nave {ID} no encontrada durante el listado.", nave.IdRepublica);
            }
        }

        Log.Information("Listado de naves generado.");
    }

    /// <summary> Genera y muestra el listado de naves ordenadas por valor descendente. </summary>
    public void ListadoNaves() {
        Log.Debug("Entrando a la función ListadoNaves.");
        var ocupadas = ContarNavesOcupadas();
        if (ocupadas == 0) {
            Console.WriteLine("El puerto está vacío. No hay naves para listar.");
            return;
        }
        
        // De la matriz, sacamos el vector sin los nulos
        var listaTemporal = ExtraerNavesOcupadas(ocupadas);
        // Ordenamos
        OrdenarNavesPorValorDescendente(listaTemporal);
        // Mostramos la lista
        MostrarListadoOrdenado(listaTemporal);
    }
}