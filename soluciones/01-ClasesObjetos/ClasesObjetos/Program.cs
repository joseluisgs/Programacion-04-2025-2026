// See https://aka.ms/new-console-template for more information

using ClasesObjetos;

Console.WriteLine("Hola POO en C#!");

var g1 = new Gato();
g1.Nombre = "Gato 1";
g1.Maullar();

var g2 = new Gato();
g2.Nombre = "Gato 2";
g2.Maullar();
Console.WriteLine($"La edad de {g1.Nombre} es {g1.GetEdad()} años");

var miCaja = new Caja();
miCaja.Tipo = "Madera"; // Ok
// miCaja._contenido = "Juguetes"; // Error: no se puede acceder directamente
miCaja.Guardar("Juguetes"); // Ok, usando método público
//miCaja._peso(); // Error: método p


// var u = new Utilidades(); // Error: no se puede instanciar porque no le hemos dado valor a un campo requerido

var gatoParcial = new GatoParcial();
gatoParcial.Maullar();