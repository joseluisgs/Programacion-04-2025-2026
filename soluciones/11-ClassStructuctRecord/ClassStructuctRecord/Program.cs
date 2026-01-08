// See https://aka.ms/new-console-template for more information

using ClassStructuctRecord;
using ClassStructuctRecord.Nave;
using static System.Console;

// UTF-8
OutputEncoding = System.Text.Encoding.UTF8;

WriteLine("Hola Class, Structs y Records!");

WriteLine("Persona Class:");
var p1 = new PersonaClass { Nombre = "John", Edad = 30 };  
var p2 = new PersonaClass { Nombre = "John", Edad = 30 };
WriteLine($"Persona p1: {p1}");
WriteLine($"Persona p2: {p2}");
WriteLine($"p1 == p2: {p1 == p2}"); // false
WriteLine($"p1 ReferenceEquals p2: {ReferenceEquals(p1,p2)}"); // false
WriteLine($"p1.Equals(p2): {p1.Equals(p2)}"); // true

p1.Nombre = "Jane";

WriteLine("\nPersona Struct:");
var s1 = new PersonaStruct { Nombre = "Jane", Edad = 25 };
var s2 = new PersonaStruct { Nombre = "Jane", Edad = 25 };
WriteLine($"Persona s1: {s1}");
WriteLine($"Persona s2: {s2}");
//WriteLine($"s1 == s2: {s1 == s2}"); // No se puede comparar referencias en algo que pasa por valor
//WriteLine($"s1 ReferenceEquals s2: {ReferenceEquals(s1,s2)}"); // No se puede comparar referencias en algo que pasa por valor
WriteLine($"s1.Equals(s2): {s1.Equals(s2)}"); // true

// Los structs son inmutables generalmente
//s1.Nombre = "John"; // Error de compilacion

WriteLine("\nPersona Record:");
var r1 = new PersonaRecord("Alice", 28);
var r2 = new PersonaRecord("Alice", 28);
WriteLine($"Persona r1: {r1}");
WriteLine($"Persona r2: {r2}");
WriteLine($"r1 == r2: {r1 == r2}"); // true
WriteLine($"r1 ReferenceEquals r2: {ReferenceEquals(r1,r2)}"); // false
WriteLine($"r1.Equals(r2): {r1.Equals(r2)}"); // true

// Los records son inmutables generalmente
// r1.Nombre = "Bob"; // Error de compilacion
// Usamos 'with' para crear una nueva instancia con cambios
var r3 = r1 with { Nombre = "Bob" }; // Crea una nueva instancia con el cambio
WriteLine($"Persona r3 (modificado): {r3}");

// Jugando con las naves
WriteLine("\nNaves:");
var n1 = new Nave("ABC123D", Nave.Tipo.Carga, new Manifiesto { PesoCarga = 500, ValorTotal = 25000.50m });
WriteLine(n1);
