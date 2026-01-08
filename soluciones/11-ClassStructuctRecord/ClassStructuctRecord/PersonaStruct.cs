namespace ClassStructuctRecord;
// Un struct simple que representa una persona con nombre y edad.
// Se almacena en el Stack.
// Pasan por valor
// Es inmutable generalmente
public struct PersonaStruct {
    public string Nombre { get; init; }
    public int Edad { get; init; }
    
    public override string ToString() => $"Nombre: {Nombre}, Edad: {Edad}";
    public override int GetHashCode() => HashCode.Combine(Nombre, Edad);
    public override bool Equals(object? obj) {
        if (obj is PersonaStruct other) {
            return Nombre == other.Nombre && Edad == other.Edad;
        }
        return false;
    }
}