// See https://aka.ms/new-console-template for more information

using IgualdadIdentidadToString;

Console.WriteLine("Hola Igualdad, identidad, y ToString!");
var libro1 = new Libro { Titulo = "Cien Años de Soledad" };
Console.WriteLine($"Libro 1: {libro1} y hasCode: {libro1.GetHashCode()}");
var libro2 = new Libro { Titulo = "Cien Años de Soledad" };
Console.WriteLine($"Libro 2: {libro2} y hasCode: {libro2.GetHashCode()}");
var libro3 = libro1;
Console.WriteLine($"Libro 3: {libro3} y hasCode: {libro3.GetHashCode()}");

Console.WriteLine("Libro 1 es igual a libro 2 con ==");
if (libro1 == libro2)
    Console.WriteLine("libro1 y libro2 son igual.");
else
    Console.WriteLine("libro1 y libro2 NO son igual.");

Console.WriteLine("Libro 1 es identicos a libro 2 con ReferenceEquals");
if (ReferenceEquals(libro1, libro2))
    Console.WriteLine("libro1 y libro2 son idénticos (misma referencia en memoria) con ReferenceEquals.");
else
    Console.WriteLine("libro1 y libro2 NO son idénticos (diferente referencia en memoria) con ReferenceEquals.");

Console.WriteLine("Libro 1 es igual a libro2 con Equals()");
if (libro1.Equals(libro2))
    Console.WriteLine("libro1 y libro2 son iguales (misma información).");
else
    Console.WriteLine("libro1 y libro2 NO son iguales (diferente información).");

Console.WriteLine("Libro 1 es igual a Libro 2 con HashCode");
if (libro1.GetHashCode() == libro2.GetHashCode())
    Console.WriteLine("libro1 y libro2 son iguales (mismo HashCode)");
else
    Console.WriteLine("libro1 y libro2 NO son iguales (diferente HashCode).");


Console.WriteLine("Libro 1 es identico a libro 3");
if (libro1 == libro3)
    Console.WriteLine("libro1 y libro3 son idénticos e iguales (misma referencia en memoria).");
else
    Console.WriteLine("libro1 y libro3 NO son idénticos e iguales (diferente referencia en memoria).");

Console.WriteLine("Libro 1 es identico a libro 3 con ReferenceEquals");
if (ReferenceEquals(libro1, libro3))
    Console.WriteLine("libro1 y libro3 son idénticos (misma referencia en memoria) con ReferenceEquals.");
else
    Console.WriteLine("libro1 y libro3 NO son idénticos (diferente referencia en memoria) con ReferenceEquals.");


Console.WriteLine("Libro 1 es igual a libro 3 con Equals()");
if (libro1.Equals(libro3))
    Console.WriteLine("libro1 y libro3 son iguales (misma información).");
else
    Console.WriteLine("libro1 y libro3 NO son iguales (diferente información).");


Console.WriteLine("Libro 2 es identico a libro 3");
if (libro2 == libro3)
    Console.WriteLine("libro2 y libro3 son idénticos (misma referencia en memoria).");
else
    Console.WriteLine("libro2 y libro3 NO son idénticos (diferente referencia en memoria).");

Console.WriteLine("Libro 2 es identico a libro 3 con ReferenceEquals");
if (ReferenceEquals(libro2, libro3))
    Console.WriteLine("libro2 y libro3 son idénticos (misma referencia en memoria) con ReferenceEquals.");
else
    Console.WriteLine("libro2 y libro3 NO son idénticos (diferente referencia en memoria) con ReferenceEquals.");

Console.WriteLine("Libro 2 es igual a libro 3 con Equals()");
if (libro2.Equals(libro3))
    Console.WriteLine("libro2 y libro3 son iguales (misma información).");
else
    Console.WriteLine("libro2 y libro3 NO son iguales (diferente información).");