using System.Text.RegularExpressions;

namespace GetterSetter;

public class PersonaVioladaEnEstado {
    // Esto de tener todo public,
    // es una violación del estado del objeto
    private String Nombre;
    public int Edad;

    public PersonaVioladaEnEstado(String nombre ="Desconocido", int edad = 0) {
        if (edad < 0)
            throw new ArgumentException("La edad no puede ser negativa");
        Edad = edad;
        if (!IsNombreValido(nombre))
            throw new ArgumentException("El nombre debe tener 3 letras y solo letras");
        Nombre = nombre;
    }
    
    public override string ToString() {
        return $"Nombre: {Nombre}, Edad: {Edad}";
    }
    
    private bool IsNombreValido(string nombre) {
        // Tres o más letras, solo letras
        var regex = new Regex("^[A-Za-z]{3,}$");
        return regex.IsMatch(nombre);
    }
    
    private bool IsEdadValida(int edad) {
        return edad >= 0 && edad <= 120;
    }
}