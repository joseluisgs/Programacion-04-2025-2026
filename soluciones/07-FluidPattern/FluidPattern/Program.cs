// See https://aka.ms/new-console-template for more information

using GetterSetter;

Console.WriteLine("Hola Patron Fluido!");


// Crear una instancia de la clase Estudiante con propiedades y métodos de acceso y modificación (Getters y Setters)
// Aquí se puede usar el patrón de diseño de objetos fluido (Fluent Interface) para simplificar la creación de objetos.
// Se usa para encadenar llamadas a métodos de configuración (setters) de manera más legible.
// Y poder crear objetos complejos de manera más sencilla.
// Así como solo pasar los setter que se necesitan en el orden que quieras.
var persona = new Estudiante()
    .SetNombre("Juan")
    .SetEdad(20)
    .SetCalificacion(8.5);


Console.WriteLine(persona);