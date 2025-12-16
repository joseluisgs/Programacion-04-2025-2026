//  .\Aliens.exe space:12 aliens:8 lives:4 tiempo:80

using System.Text;
using Aliens.Config;
using Aliens.Services;


// -----------------------------------------------------
// INICIO (Top-level)
// -----------------------------------------------------
Console.Title = "Juego Aliens - Doble Buffer con Swap";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Main(args);

Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

void Main(string[] args) {
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine(" 👾 Simulación Aliens (Doble Buffer) - DAW Edition");
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine();

    ProcesarArgumentos(args);
    var service = new AliensSimuladorService();

    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine(" ⚙️ Configuración:");
    Console.WriteLine($"- Tamaño del espacio: {Configuracion.SpaceSize}x{Configuracion.SpaceSize}");
    Console.WriteLine($"- Aliens iniciales: {Configuracion.NumAliens}");
    Console.WriteLine($"- Vidas iniciales: {Configuracion.Lives}");
    Console.WriteLine($"- Tiempo máximo (ciclos): {Configuracion.MaxTime}");
    Console.WriteLine("----------------------------------------------------");
    Console.WriteLine("\nPulse una tecla para iniciar la simulación...");
    Console.ReadKey();


    // Ejecutar simulación (los buffers se manejan localmente)
    Simulacion(service);

    // Mostrar resultados finales
    InformeFinal(service);
}


/* ===================================================================
   FUNCIONES PRINCIPALES (resumen)
   =================================================================== */

void ProcesarArgumentos(string[] args) {
    Console.WriteLine("------------ ⚙️ Procesando Configuración -----------");

    string? v;
    v = BuscarValorEnArgs(args, "space");
    if (v != null && int.TryParse(v, out var s) && s > 0)
        Configuracion.SpaceSize = s;
    else if (v != null)
        Console.WriteLine($"⚠️ 'space' inválido ('{v}'), usando {Params.DefaultSpaceSize}");
    v = BuscarValorEnArgs(args, "aliens");
    if (v != null && int.TryParse(v, out var a) && a >= 0)
        Configuracion.NumAliens = a;
    else if (v != null)
        Console.WriteLine($"⚠️ 'aliens' inválido ('{v}'), usando {Params.DefaultNumAliens}");
    v = BuscarValorEnArgs(args, "lives");
    if (v != null && int.TryParse(v, out var l) && l >= 0)
        Configuracion.Lives = l;
    else if (v != null)
        Console.WriteLine($"⚠️ 'lives' inválido ('{v}'), usando {Params.DefaultLives}");
    v = BuscarValorEnArgs(args, "tiempo") ?? BuscarValorEnArgs(args, "time");
    if (v != null && int.TryParse(v, out var t) && t > 0)
        Configuracion.MaxTime = t;
    else if (v != null) Console.WriteLine($"⚠️ 'tiempo' inválido ('{v}'), usando {Params.DefaultMaxTime}");
    Console.WriteLine("----------------------------------------------------");
}

string? BuscarValorEnArgs(string[] args, string claveBuscada) {
    var claveNormalizada = claveBuscada.ToLower().Trim();
    foreach (var arg in args) {
        var parts = arg.Split(':');
        if (parts.Length == 2) {
            var claveActual = parts[0].ToLower().Trim();
            if (claveActual == claveNormalizada) return parts[1].Trim();
        }
    }

    return null;
}

void Simulacion(AliensSimuladorService service) {
    service.Simulacion();
}


// Informe final de la simulación
void InformeFinal(AliensSimuladorService service) {
    Console.WriteLine();
    service.PrintSpace();
    var aliensVivos = service.ContarAliensVivos();
    Console.WriteLine(
        $"⏱️ Tiempo total: {Math.Min(Configuracion.MaxTime, service.Estado.Tiempo)} (Máx: {Configuracion.MaxTime})");

    if (aliensVivos == 0) {
        Console.WriteLine("🏆 Has aniquilado a todos los aliens!");
    }
    else {
        Console.WriteLine("🚨 Hay aliens vivos que regresarán a por ti!");
        Console.WriteLine($"👾 Aliens restantes: {aliensVivos}"); // ⬅️ CAMBIO AQUÍ
    }

    Console.WriteLine($"☠️ Has eliminado un total de {service.Estado.AliensEliminados} aliens.");

    Console.WriteLine(
        $"🔫 Disparos realizados: {service.Estado.NumDisparos} - Disparos acertados: {service.Estado.NumDisparosAcertados}");
    Console.WriteLine(
        $"🎯 Precisión de disparos: {service.Estado.PrecisionDisparos:F2}% ({service.Estado.NumDisparosAcertados}/{service.Estado.NumDisparos})");

    if (service.Estado.VidasJugador == 0) {
        Console.WriteLine("💀 Has muerto en esta batalla!");
    }
    else {
        Console.WriteLine($"❤️ Vidas restantes: {service.Estado.VidasJugador}");
        Console.WriteLine("💪 Has sobrevivido, y vives para luchar otro día!");
    }

    var alienesEliminados = service.AliensMuertos();
    if (alienesEliminados.Length > 0) {
        Console.WriteLine("\n👾 Alienígenas eliminados");
        foreach (var alien in alienesEliminados)
            Console.WriteLine(alien);
    }
}