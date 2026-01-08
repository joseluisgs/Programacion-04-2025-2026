using System.Text;
using System.Text.RegularExpressions;
using Cine.Config;
using Cine.Enums;
using Cine.Services;
using Cine.Structs;
using Cine.Utility;
using Serilog;
using Serilog.Events;


// Main Program
// 1. CONFIGURACIÓN DE LOGS a lo GOD MODE 
// Logger profesional a lo bestia!
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)

    // --- CONSOLA: Limpia (solo Information y superior) ---
    .WriteTo.Console(
        LogEventLevel.Information
    )

    // --- FICHERO LOG ROTATIVO (.log) ---
    .WriteTo.File(
        "logs/cine_debug-.log", // Usará cine_debug-YYYYMMDD.log
        rollingInterval: RollingInterval.Day,
        retainedFileCountLimit: 7,
        // Formato para logs legibles por humanos
        outputTemplate: "{Timestamp:HH:mm:ss.fff zzz} [{Level:u3}] [{SourceContext}] {Message:lj}{NewLine}{Exception}"
    )
    .CreateLogger();

Log.Information("🎬 Sistema de cine iniciado. Logs diarios (.log) en la carpeta logs/.");

// 2. INICIO (Top-level Execution)
Console.Title = "CineDaw - Gestión de Sala de Cine";
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();


// Inicializar el servicio de cine
// var cineService = new CineService();
var cineService = CineService.GetInstance();
CineService.GetInstance();

Console.WriteLine("🎞️ Bienvenido a CineDaw 🎞️");
Console.WriteLine("=========================");
Console.WriteLine();


int opcion;

do {
    // Barra de progreso
    Console.WriteLine("Estado de ocupación de la sala:");
    DibujarBarraProgreso(cineService.CalcularButacasOcupadasOFueraServicio(),
        Configuracion.TamFilas * Configuracion.TamColumnas);
    Console.WriteLine("\n");
    Console.WriteLine("Menú:");
    Console.WriteLine($"{(int)MenuOpcion.Mostrar}. Mostrar miSala");
    Console.WriteLine($"{(int)MenuOpcion.Comprar}. Comprar entrada");
    Console.WriteLine($"{(int)MenuOpcion.Devolver}. Devolver entrada");
    Console.WriteLine($"{(int)MenuOpcion.Informe}. Informe final (Estadísticas)");
    Console.WriteLine($"{(int)MenuOpcion.Salir}. Salir");
    Console.Write("Ingrese una opción: ");

    var inputOpcion = Console.ReadLine()?.Trim() ?? "";

    var patronOpcion = @"^[1-5]$";
    var regexOpcion = new Regex(patronOpcion);

    opcion = 0;
    if (regexOpcion.IsMatch(inputOpcion))
        opcion = int.Parse(inputOpcion);

    switch (opcion) {
        case (int)MenuOpcion.Mostrar:
            MostrarSala(cineService);
            break;
        case (int)MenuOpcion.Comprar:
            ComprarEntrada(cineService);
            break;
        case (int)MenuOpcion.Devolver:
            DevolverEntrada(cineService);
            break;
        case (int)MenuOpcion.Informe:
            GenerarInforme(cineService);
            break;
        case (int)MenuOpcion.Salir:
            Console.WriteLine("¡Hasta luego!");
            break;
        default:
            Console.WriteLine("Opción no válida");
            break;
    }

    Console.WriteLine();
} while (opcion != (int)MenuOpcion.Salir);


Console.WriteLine("\n👋 Presiona una tecla para salir...");
Console.ReadKey();
return;

// Muestra el estado de la sala de cine
void MostrarSala(CineService service) {
    Log.Information("Mostrando miSala");
    service.MostrarSala();
}

// Compra una entrada para el usuario
void ComprarEntrada(CineService service) {
    Log.Information("Comprando entrada");
    if (!service.HayButacaPorEstado(Butaca.Disponibilidad.Libre)) {
        Console.WriteLine("❌ No hay butacas libres.");
        return;
    }

    bool repetir;
    do {
        var pos = LeerPosicionButaca("Ingrese butaca (A-E:1-6): ");
        if (service.ButacaPorPosicion(pos).Estado != Butaca.Disponibilidad.Libre) {
            Console.WriteLine("❌ La butaca no está disponible.");
            repetir = true;
            //return; // salimos del bucle para volver al menú principal
        }
        else {
            var precioStr = service.ButacaPorPosicion(pos).Precio.ToString("C2", Configuracion.Locale);
            if (Confirmar(
                    $"⚠️ Confirmar compra de ({Utility.Letra(pos.Fila)},{pos.Columna + 1}) por {precioStr} (s/n): ")) {
                ComprarButaca(service, pos);
                repetir = false;
            }
            else {
                Console.WriteLine("❌ Compra cancelada.");
                repetir = false;
            }
        }
    } while (repetir);
}

// Lee una posición de butaca desde la entrada del usuario o devuelve null si es inválida
Posicion LeerPosicionButaca(string mensaje) {
    Log.Debug("Leyendo posición de butaca");
    string input;
    bool inputOk;
    do {
        Console.Write(mensaje);
        input = (Console.ReadLine() ?? "").Trim().ToUpper();
        inputOk = Regex.IsMatch(input, @"^([A-E]):([1-6])$");
        if (!inputOk)
            Console.WriteLine("Formato incorrecto. Usa A-E:1-6 (ej. B:3).");
    } while (!inputOk);

    var partes = input.Split(':');

    var pos = new Posicion {
        Fila = Utility.Indice(partes[0][0]), // "A", 'A'
        Columna = int.Parse(partes[1]) - 1
    };
    return pos;
}


// Compra la butaca en la posición dada
void ComprarButaca(CineService service, Posicion posicion) {
    Log.Debug("Comprando butaca en la posición ({posicion.Fila}, {posicion.Columna})", posicion.Fila,
        posicion.Columna);

    var entrada = service.OcuparButaca(posicion);

    Console.WriteLine("\n--- 🎟️ ENTRADA DE CINE ---");
    Console.WriteLine(
        $"Butaca: {Utility.Letra(entrada.Butaca.Posicion.Fila)}:{entrada.Butaca.Posicion.Columna + 1}");
    Console.WriteLine($"Categoría: {entrada.Categoria}");
    Console.WriteLine($"Precio: {entrada.Precio.ToString("C2", Configuracion.Locale)}");
    Console.WriteLine($"Fecha:  {entrada.FechaCompra.ToString("g", Configuracion.Locale)}");
    Console.WriteLine("------------------------------\n");
}

void DevolverEntrada(CineService service) {
    Log.Information("Devolviendo entrada");
    if (!service.HayButacaPorEstado(Butaca.Disponibilidad.Ocupada)) {
        Console.WriteLine("❌ No hay butacas ocupadas.");
        return;
    }

    bool repetir;
    do {
        var pos = LeerPosicionButaca("Ingrese butaca a devolver (A-E:1-6): ");
        if (!service.IsOcupada(pos)) {
            Console.WriteLine("❌ La butaca no estaba ocupada.");
            repetir = true;
        }
        else {
            var precioStr = service.ButacaPorPosicion(pos).Precio.ToString("C2", Configuracion.Locale);
            if (Confirmar(
                    $"⚠️ Confirmar la devolución de ({Utility.Letra(pos.Fila)},{pos.Columna + 1}) por {precioStr} (s/n): ")) {
                DevolverButaca(service, pos);
                repetir = false;
            }
            else {
                Console.WriteLine("❌ Operación cancelada.");
                repetir = false;
            }
        }
    } while (repetir);
}

// Devuelve la entrada para el usuario
void DevolverButaca(CineService service, Posicion posicion) {
    Log.Debug("Devolviendo butaca en la posición ({posicion.Fila}, {posicion.Columna})", posicion.Fila,
        posicion.Columna);
    var importe = service.ButacaPorPosicion(posicion).Precio;
    service.LiberarButaca(posicion);
    Console.WriteLine("\n--- DEVOLUCIÓN DE ENTRADA ---");
    Console.WriteLine(
        $"✅ Devolución realizada de la entrada ({Utility.Letra(posicion.Fila)},{posicion.Columna + 1}).");
    Console.WriteLine($"💰 Importe devuelto: {importe.ToString("C2", Configuracion.Locale)}\n");
}


// Solicita confirmación al usuario
bool Confirmar(string mensaje) {
    Log.Debug("Solicitando confirmación");
    Console.Write(mensaje);
    var k = Console.ReadKey(true).KeyChar;
    Console.WriteLine();
    return k == 's' || k == 'S'; // Si la respuesta es's' o 'S'
}

void GenerarInforme(CineService service) {
    Log.Information("Generando informe de la sala");
    var informe = service.InformeDeSala();


    Console.WriteLine("\n--- 📈 INFORME CINEDAW 📈 ---");
    Console.WriteLine($"🎟️ Entradas Vendidas: {informe.EntradasVendidas}");
    Console.WriteLine($"💺 Asientos Libres: {informe.AsientosLibres}");
    Console.WriteLine($"🚫 Fuera de Servicio: {informe.AsientosFueraDeServicio}");
    Console.WriteLine($"📽️ Ocupación: {informe.PorcentajeDeOcupacion.ToString("F2", Configuracion.Locale)}%");
    Console.WriteLine($"💵 Recaudación Total: {informe.RecaudacionTotal.ToString("C2", Configuracion.Locale)}\n");
}

void DibujarBarraProgreso(int actual, int maximo) {
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