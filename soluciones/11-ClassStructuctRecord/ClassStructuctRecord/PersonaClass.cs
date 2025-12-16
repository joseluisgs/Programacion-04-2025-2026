namespace ClassStructuctRecord;

// Un clase simple que representa una persona con nombre y edad.
// Se almacena en el Heap.
// Pasan por referencia
// Es mutable generalmente
public class PersonaClass {
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    
    public override string ToString() => $"Nombre: {Nombre}, Edad: {Edad}";
    public override int GetHashCode()  => HashCode.Combine(Nombre, Edad);
    public override bool Equals(object? obj) {
        if (obj is PersonaClass other) {
            return Nombre == other.Nombre && Edad == other.Edad;
        }
        return false;
    }
}