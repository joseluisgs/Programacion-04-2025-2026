// Plantilla de Examen C#

// 1. DIRECTIVAS USING

using System.Text;
using PuertoDarsenas.Enums;
using PuertoDarsenas.Services;
using Serilog;

// 1. INICIALIZACIÓN DE SERILOG Y CULTURA
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
Log.Information("Serilog iniciado y configurado para consola.");

// 2. INICIO (Top-level Execution)
Console.Title = "Puerto de Carga de Nevarro";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

// Menú principal
Main(args);

Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return; // Fin del TLI

// 3. MÉTODO MAIN 
void Main(string[] args) {
    Log.Debug("Iniciando la ejecución principal (Main).");
    var puerto = new ServicioPuerto();
    MenuPrincipal(puerto);
}

void MenuPrincipal(ServicioPuerto puerto) {
    Log.Debug("Entrando a la función MenuPrincipal.");
    OpcionMenu opcion;
    do {
        Console.WriteLine("\n🚀 --- Puerto de Carga de Nevarro --- ⚙️");
        Console.WriteLine($"1. 🗺️ Ver el estado del puerto de carga (Ocupadas: {puerto.ContarNavesOcupadas()})");
        Console.WriteLine("2. 📥 Asignar puerta de embarque");
        Console.WriteLine("3. 🔍 Buscar nave");
        Console.WriteLine("4. 📤 Despegar nave");
        Console.WriteLine("5. 📜 Listado de naves (Orden descendente por valor)");
        Console.WriteLine("6. ❌ Salir");
        Console.Write("Seleccione una opción: ");

        var input = Console.ReadLine()?.Trim() ?? "";

        if (!int.TryParse(input, out var inputOpcion) || inputOpcion < 1 || inputOpcion > 6) {
            Log.Error("Opción de menú inválida: {Input}", input);
            opcion = 0;
            continue;
        }

        opcion = (OpcionMenu)inputOpcion;

        switch (opcion) {
            case OpcionMenu.VerEstado: puerto.VerEstadoPuerto(); break;
            case OpcionMenu.AsignarPuerta: puerto.AsignarPuerta(); break;
            case OpcionMenu.BuscarNave: puerto.BuscarNave(); break;
            case OpcionMenu.DespegarNave: puerto.DespegarNave(); break;
            case OpcionMenu.ListadoNaves: puerto.ListadoNaves(); break;
            case OpcionMenu.Salir: Log.Information("Sistema cerrado."); break;
            default: Log.Error("Opción no reconocida: {Opcion}", opcion); break;
        }
    } while (opcion != OpcionMenu.Salir);
}