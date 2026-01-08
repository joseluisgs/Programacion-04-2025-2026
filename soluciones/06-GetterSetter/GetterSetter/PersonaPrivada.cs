using System.Text.RegularExpressions;

namespace GetterSetter;

public class PersonaPrivada{
    // Esto de tener todo public,
    // es una violación del estado del objeto
    private String Nombre;
    private int Edad;

    public PersonaPrivada(String nombre ="Desconocido", int edad = 0) {
        if (edad < 0)
            throw new ArgumentException("La edad no puede ser negativa");
        Edad = edad;
        if (!IsNombreValido(nombre))
            throw new ArgumentException("El nombre debe tener 3 letras y solo letras");
        Nombre = nombre;
    }
    
    // Getters y Setters
    public string GetNombre() {
        return Nombre;
    }
    public void SetNombre(string nombre) {
        if (!IsNombreValido(nombre))
            throw new ArgumentException("El nombre debe tener 3 letras y solo letras");
        Nombre = nombre;
    }
    
    public int GetEdad() {
        return Edad;
    }
    public void SetEdad(int edad) {
        if (!IsEdadValida(edad))
            throw new ArgumentException("La edad debe estar entre 0 y 120");
        Edad = edad;
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