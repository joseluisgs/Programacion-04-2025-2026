using System.Text.RegularExpressions;

namespace GetterSetter;

public class Estudiante{
    private String _nombre = "Desconocido";
    private int _edad;
    private double _calificacion;
    
    // La diferencia en el Fluid Pattern es que los setters retornan el mismo objeto
    // para permitir encadenar llamadas.
    
    // Getters y Setters
    public string GetNombre() {
        return _nombre;
    }
    public Estudiante SetNombre(string nombre) {
        if (!IsNombreValido(nombre))
            throw new ArgumentException("El nombre debe tener 3 letras y solo letras");
        _nombre = nombre;
        return this;
    }
    
    public int GetEdad() {
        return _edad;
    }
    public Estudiante SetEdad(int edad) {
        if (!IsEdadValida(edad))
            throw new ArgumentException("La edad debe estar entre 0 y 120");
        _edad = edad;
        return this;
    }
    
    public double GetCalificacion() {
        return _calificacion;
    }
    public Estudiante SetCalificacion(double calificacion) {
        if (!IsCalificacionValida(calificacion))
            throw new ArgumentException("La calificación debe estar entre 0.0 y 10.0");
        _calificacion = calificacion;
        return this;
    }
    
    public bool IsAprobado() {
        return _calificacion >= 5.0;
    }
    
    public string GetCalificacionLiteral() {
        return _calificacion switch {
            < 5.0 => "Suspenso",
            < 7.0 => "Aprobado",
            < 9.0 => "Notable",
            _ => "Sobresaliente"
        };
    }

    public override string ToString() {
        return $"Nombre: {_nombre}, Edad: {_edad}";
    }
    
    private bool IsNombreValido(string nombre) {
        // Tres o más letras, solo letras
        var regex = new Regex("^[A-Za-z]{3,}$");
        return regex.IsMatch(nombre);
    }
    
    private bool IsEdadValida(int edad) {
        return edad >= 0 && edad <= 120;
    }
    
    private bool IsCalificacionValida(double calificacion) {
        return calificacion >= 0.0 && calificacion <= 10.0;
    }
}