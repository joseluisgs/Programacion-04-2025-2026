// See https://aka.ms/new-console-template for more information

var tupla = (Nombre: "Carlos", Edad: 40, Ciudad: "Madrid");
Console.WriteLine($"Tupla: Nombre={tupla.Nombre}, Edad={tupla.Edad}, Ciudad={tupla.Ciudad}");

var objetoAnonimo = new { Nombre = "Ana", Edad = 35, Ciudad = "Barcelona" };
Console.WriteLine($"Objeto Anónimo: Nombre={objetoAnonimo.Nombre}, Edad={objetoAnonimo.Edad}, Ciudad={objetoAnonimo.Ciudad}");


// Se puede usar una tupla para devolver múltiples valores desde un método
(string, int) ObtenerNombreYEdad() {
    return ("Luis", 29);
}
var (nombre, edad) = ObtenerNombreYEdad();
Console.WriteLine($"Desde método: Nombre={nombre}, Edad={edad}");

// Se puede usar como parametros de métodos
void MostrarInfo((string Nombre, int Edad) persona) {
    Console.WriteLine($"MostrarInfo - Nombre={persona.Nombre}, Edad={persona.Edad}");
}
MostrarInfo(("Marta", 32));

// Se pueden usar objetos anonimos para agrupar datos sin definir una clase
// Pues no, pero ya veremos otras cosas mas adelante