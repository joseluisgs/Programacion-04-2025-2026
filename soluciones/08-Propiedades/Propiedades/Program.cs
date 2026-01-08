// See https://aka.ms/new-console-template for more information

using Propiedades.Models;

var p1 = new ProductoA();
var p2 = new ProductoB();
var p3 = new ProductoC { Categoria = "Electronica" }; // required obliga a inicializarla
// p3.Categoria = "Alimentos"; // Esto no es válido porque es un set init
p3.Precio = -12; // Esto lanza una excepción