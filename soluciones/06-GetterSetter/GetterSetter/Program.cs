// See https://aka.ms/new-console-template for more information

using GetterSetter;

Console.WriteLine("Hola Getters & Setters!");
var p1 = new PersonaVioladaEnEstado("Ana", 25);
Console.WriteLine(p1);
p1.Edad = -5; // Violación del estado del objeto
Console.WriteLine(p1);

var p2 = new PersonaPrivada("Luis", 30);
Console.WriteLine(p2);
try {
    p2.SetNombre("X"); // No permite violar el estado del objeto
} 
catch (ArgumentException ex) {
    Console.WriteLine($"Error al establecer el nombre: {ex.Message}");
}

try {
    p2.SetEdad(150); // No permite violar el estado del objeto
} 
catch (ArgumentException ex) {
    Console.WriteLine($"Error al establecer la edad: {ex.Message}");
}