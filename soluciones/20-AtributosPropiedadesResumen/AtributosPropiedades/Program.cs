// See https://aka.ms/new-console-template for more information

using AtributosPropiedades;

Console.WriteLine("Hello, World!");
var p0 = new Persona0("Juan", 30, "12345678A");
var pa = new PersonaA("Juan", 30, "12345678A");
var pb = new PersonaB("Juan", 30, "12345678A");
var pc = new PersonaC("Juan", 30, "12345678A"); 
var pd = new PersonaD("Juan", 30, "12345678A");
var pe = new PersonaE("Juan", 30, "12345678A");

// Una cosa es el constructor
var p1 = new PersonaC(nombre: "Ana", edad: 25, dni: "87654321B");
// otra cosa es el inicializador
// en este caso no funciona porque tenemos un constructor que obliga a pasar los 3 parámetros
// si le pones Required a las propiedades, estamos obligados a definirlas
// En el inicializador
var p2 = new PersonaC { Nombre = "Luis", Edad = 40, Dni = "11223344C" };