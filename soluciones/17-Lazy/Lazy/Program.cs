// See https://aka.ms/new-console-template for more information

using Lazy;

Console.WriteLine("Hola Lazy!");

Console.WriteLine("\n--- Dashboard sin Lazy ---");
var d1 = new Dashboard(); // Prueba sin Lazy
// Se crean ambos reportes al instanciar el Dashboard
// Por lo que si uno de los reportes no se usa, se pierde tiempo y recursos
d1.DatosReporteSemanal.MostrarResumen();
d1.DatosReporteMensual.MostrarResumen();

Console.WriteLine("\n--- Dashboard con Lazy ---");
var d2 = new DashboardLazy();
d2.DatosReporteSemanal.MostrarResumen(); 
d2.DatosReporteMensual.MostrarResumen(); // Ahora se crea el reporte mensual solo al acceder a esta propiedad

