namespace Constructores;

public class Persona {
    public int Edad = 0;
    public string Nombre = "Desconocido";

    public override string ToString() {
        return $"Nombre: {Nombre}, Edad: {Edad}";
    }
}