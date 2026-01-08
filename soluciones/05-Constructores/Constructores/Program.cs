// See https://aka.ms/new-console-template for more information

using Constructores;

Console.WriteLine("Hola Constructores!");

var persona1 = new Persona(); // Llamada al constructor por defecto
persona1.Nombre = "María";
persona1.Edad = 1;
Console.WriteLine(persona1);
var persona2 = new Persona { Nombre = "Juan", Edad = 1 };
Console.WriteLine(persona2);

var persona3 = new PersonaConstructor("Hola", 8); // Llamada al constructor por defecto
Console.WriteLine(persona3);
var persona4 = new PersonaConstructor("Ana");
Console.WriteLine(persona4);
var persona5 = new PersonaConstructor(25);
Console.WriteLine(persona5);
var persona6 = new PersonaConstructor("Luis");
Console.WriteLine(persona6);
var persona7 = new PersonaConstructor(30);
Console.WriteLine(persona7);
var persona8 = new PersonaConstructor(persona4);
Console.WriteLine(persona8);
var persona9 = new PersonaConstructor("Hola", 8) { Nombre = "Pedro", Edad = 40 };
Console.WriteLine(persona9);
try {
    var personaError = new PersonaConstructor("X", 15);
    Console.WriteLine(personaError);
}
catch (ArgumentException ex) {
    Console.WriteLine($"Error al crear la persona: {ex.Message}");
}