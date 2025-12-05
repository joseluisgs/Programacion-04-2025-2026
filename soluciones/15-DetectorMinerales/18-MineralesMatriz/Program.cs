using System.Text;
using _18_MineralesMatriz.Services;


// ----------------------------------------------------
// BLOQUE PRINCIPAL (MAIN)
// ----------------------------------------------------
Console.OutputEncoding = Encoding.UTF8;
Console.Clear();

Console.WriteLine("🤖 Detector de Minerales 2D 🤖");

var servicioSonda = new SondaEspacialService();

Console.WriteLine("--- Mapa Inicial de Minerales (Valores) ---");
servicioSonda.PrintMapMinerales();
var result = servicioSonda.IniciarExploracion();


// Resultado final
Console.Clear();
Console.WriteLine("--------------------------------------");
Console.WriteLine("FIN DE LA EXPLORACION 🤖");
Console.WriteLine("--------------------------------------");
Console.WriteLine($"Tiempo Final: {result.Tiempo - 1}");
Console.WriteLine($"Cantidad de Mineral Total: {result.Cantidad} 💎");
Console.WriteLine(result.Posicion + " 🤖");
Console.WriteLine("--- Mapa Final de Minerales (Valores) ---");
servicioSonda.PrintMapMinerales();

Console.WriteLine("👋 Presiona una tecla para salir...");
Console.ReadKey();