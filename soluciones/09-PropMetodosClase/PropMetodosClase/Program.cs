// See https://aka.ms/new-console-template for more information

using Microsoft.VisualBasic;
using PropMetodosClase;

var p1 = new Persona {
    Nombre = "Juan",
    Edad = 30
};
p1.Saludar();
Console.WriteLine(p1);
Persona.MensajeCompartido = "Nuevo mensaje compartido.";
Console.WriteLine(Persona.ContadorPersonas);
var p2 = new Persona {
    Nombre = "María",
    Edad = 25
};
p2.Saludar();
Console.WriteLine(p2);
Console.WriteLine(Persona.ContadorPersonas);
Console.WriteLine(Persona.MensajeCompartido);
Persona.MostrarContadorPersonas();

Console.WriteLine(Utilidades.EsPalindromo("radar")); // True
Console.WriteLine(Utilidades.EsPalindromo("hola"));  // False