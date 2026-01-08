using System.Text.RegularExpressions;

namespace Constructores;

public class PersonaConstructor {
    public int Edad;

    public string Nombre;
    // public readonly int Edad; // Si no quiero que se modifique después de la inicialización
    // public required string Nombre = "Desconocido"; // C# 11, obliga a inicializar el valor en el constructor o al crear el objeto

    // A veces me puede interesar inicializar ciertos valores por defecto
    /*public PersonaConstructor() {
        Edad = 0;
        Nombre = "Desconocido";
    }*/

    public PersonaConstructor(string nombre = "Desconocido", int edad = 0) {
        if (edad < 0)
            //Edad = 0;
            throw new ArgumentException("La edad no puede ser negativa");
        Edad = edad;
        if (!IsNombreValido(nombre))
            //Nombre = "Desconocido";
            throw new ArgumentException("El nombre debe tener 3 letras y solo letras");
        Nombre = nombre;
    }

    // Sobrecarga de constructores usando this, llama al constructor principal
    public PersonaConstructor(string nombre = "OtroConstructor") : this(nombre, 18) { }
    public PersonaConstructor(int edad = 0) : this("PruebaConEdad", edad) { }

    // Constructor copia
    public PersonaConstructor(PersonaConstructor otra) : this(otra.Nombre, otra.Edad) { }

    public override string ToString() {
        return $"Nombre: {Nombre}, Edad: {Edad}";
    }

    private bool IsNombreValido(string nombre) {
        // Tres o más letras, solo letras
        var regex = new Regex("^[A-Za-z]{3,}$");
        return regex.IsMatch(nombre);
    }
}