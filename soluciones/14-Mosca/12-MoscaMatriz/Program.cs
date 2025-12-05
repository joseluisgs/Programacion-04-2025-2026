using System.Text;
using System.Text.RegularExpressions;
using _12_MoscaMatriz.Model;
using _12_MoscaMatriz.Services;
using _12_MoscaMatriz.Structs;

// Constantes para las condiciones de inicio del juego
const int TamDefault = 8; // Tamaño predeterminado de la matriz: 8x8
const int NumIntentosDefault = 5; // Número de oportunidades para cazar la mosca
const int NumVidasMoscaDefault = 2; // Número de vidas de la mosca

// Main program
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();


// ----------------------------------------------------
// BLOQUE PRINCIPAL (Top-Level Statements)
// ----------------------------------------------------

// **INICIO DEL PROGRAMA PRINCIPAL**

// 0. Limpieza de la consola al iniciar
Console.Clear();

// 0.1. Validación de la entrada del tamaño de la matriz y el número de intentos por argumentos
var configuracion = ValidarArgumentosEntrada(args);
var servicioJuego = new JuegoMoscaService(configuracion);

// 1. Presentación del juego
Console.WriteLine("=============================================");
Console.WriteLine("    🎮 INICIANDO JUEGO CAZAR LA MOSCA 🪰    ");
Console.WriteLine("=============================================");
Console.WriteLine("Parámetros del juego:");
Console.WriteLine($"\t- Tamaño de la matriz: {configuracion.Tamaño}x{configuracion.Tamaño}");
Console.WriteLine($"\t- Número de intentos: {configuracion.VidasJugador}");
Console.WriteLine($"\t- Vidas de la mosca: {configuracion.VidasMosca}");
Console.WriteLine("=============================================");
Console.WriteLine("👀 ¡Prepárate para cazar la mosca!");
Console.WriteLine();


// 3. Ejecución del juego
// Los arrays se pasan por referencia implícita en C#, lo que permite modificar 'matriz'
// dentro de la función 'JugarCazarMosca'.
var result = servicioJuego.JugarCazarMosca();
// Console.WriteLine(result);

// 4. Mostrar el resultado final
if (result == Mosca.Estado.Muerta) {
    Console.WriteLine("=============================================");
    Console.WriteLine("✅ ¡HAS GANADO! Has cazado la mosca.");
    Console.WriteLine("=============================================");
}
else {
    Console.WriteLine("=============================================");
    Console.WriteLine("❌ ¡HAS PERDIDO! Se agotaron los intentos.");
    Console.WriteLine("=============================================");
}

// 5. Imprimir la matriz final para que el jugador vea dónde estaba la mosca
Console.WriteLine("\n--- Posición Final de la Mosca ---");
servicioJuego.ImprimirTablero();

Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();
return;
// End of main program

// ----------------------------------------------------
// FUNCIONES Y PROCEDIMIENTOS AUXILIARES
// ----------------------------------------------------

// Valida los argumentos de entrada del programa (tamaño y número de intentos).
// Parámetros:
// - args: Array de strings con los argumentos de entrada.
// Devuelve: Una estructura Configuracion con los valores validados o por defecto.
Configuracion ValidarArgumentosEntrada(string[] args) {
    // Analizamos si vienen dos argumentos
    if (args.Length != 3) {
        Console.WriteLine("❌ Error: Debe ingresar dos argumentos: jugador:X tam:Y mosca:Z");
        return PedirConfiguracion();
    }

    // Analizamos los argumentos de entrada
    // Primero jugador:X
    var jugador = args[0].Split(':');
    if (jugador.Length != 2 || !int.TryParse(jugador[1], out var vidasJugador) || vidasJugador <= 0 ||
        vidasJugador > NumIntentosDefault) {
        Console.WriteLine(
            $"❌ Error: El argumento '{args[0]}' no es válido. Debe ser jugador:X, donde X es un entero entre 1 y {NumIntentosDefault}.");
        return PedirConfiguracion();
    }

    // Luego tam:Y
    var tam = args[1].Split(':');
    if (tam.Length != 2 || !int.TryParse(tam[1], out var tamTablero) || tamTablero <= 0 || tamTablero > TamDefault) {
        Console.WriteLine(
            $"❌ Error: El argumento '{args[1]}' no es válido. Debe ser tam:Y, donde Y es un entero entre 1 y {TamDefault}.");
        return PedirConfiguracion();
    }

    var mosca = args[2].Split(':');
    if (mosca.Length != 2 || !int.TryParse(mosca[1], out var vidasMosca) || vidasMosca <= 0 ||
        vidasMosca > NumVidasMoscaDefault) {
        Console.WriteLine(
            $"❌ Error: El argumento '{args[0]}' no es válido. Debe ser mosca:Z, donde Z es un entero entre 1 y {NumVidasMoscaDefault}.");
        return PedirConfiguracion();
    }

    // Si todo es correcto, asignamos los valores
    return new Configuracion {
        VidasJugador = vidasJugador,
        Tamaño = tamTablero,
        VidasMosca = vidasMosca
    };
}

// Pide al usuario la configuración del juego (vidas y tamaño) si los argumentos de entrada son inválidos.
// Devuelve: Una estructura Configuracion con los valores ingresados por el usuario
Configuracion PedirConfiguracion() {
    Console.WriteLine("--- Configuración del Juego ---");
    Console.WriteLine(
        $"Por favor ingrese los parametros vidas y tamaño, de la siguiente forma: vidas:[1-{NumIntentosDefault}] tam:[1-{TamDefault}] mosca:[1-{NumVidasMoscaDefault}]");

    var regex = new Regex(
        $@"^vidas:([1-{NumIntentosDefault}])\s+tam:([1-{TamDefault}])\s+mosca:([1-{NumVidasMoscaDefault}])$");

    var input = (Console.ReadLine() ?? "").Trim();
    while (!regex.IsMatch(input)) {
        Console.WriteLine(
            $"❌ Error: Entrada inválida. Inténtalo de nuevo. Formato correcto: vidas:[1-{NumIntentosDefault}] tam:[1-{TamDefault}] mosca:[1-{NumVidasMoscaDefault}]");
        input = (Console.ReadLine() ?? "").Trim();
    }

    var match = regex.Match(input);
    var vidasJugador = int.Parse(match.Groups[1].Value);
    var tamTablero = int.Parse(match.Groups[2].Value);
    var vidasMosca = int.Parse(match.Groups[3].Value);

    return new Configuracion {
        VidasJugador = vidasJugador,
        Tamaño = tamTablero,
        VidasMosca = vidasMosca
    };
}