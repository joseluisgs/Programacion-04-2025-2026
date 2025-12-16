// See https://aka.ms/new-console-template for more information

using Inicializadores;

Console.WriteLine("Inicializadores en C#");

var user = new Usuario { Nombre = "Marta" };
Console.WriteLine(user.Nombre);

var otro = new Usuario();
Console.WriteLine(otro.Nombre ?? "Nombre no especificado");
otro.Nombre = "Carlos";
Console.WriteLine(otro.Nombre);

Usuario u2= new() { Nombre = "Ana" };
Console.WriteLine(u2.Nombre);
Usuario u3= new();
Console.WriteLine(u3.Nombre ?? "Nombre no especificado");
u3.Nombre = "Luis";
Console.WriteLine(u3.Nombre);