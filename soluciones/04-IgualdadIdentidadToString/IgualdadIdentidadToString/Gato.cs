namespace IgualdadIdentidadToString;

public class Gato {
    public int Edad;
    public bool EsDomestico;
    public string Nombre;
    public string Raza;

    
    public override string ToString() {
        return $"[Gato: {Nombre}, Edad: {Edad}, Raza: {Raza}, Doméstico: {EsDomestico}]";
    }

    public override int GetHashCode() {
        return HashCode.Combine(Nombre, Edad, Raza, EsDomestico);
    }

    public override bool Equals(object obj) {
        if (obj is Gato gato)
            return gato.Nombre == Nombre && gato.Edad == Edad && gato.Raza == Raza && gato.EsDomestico == EsDomestico;
        return false;
    }
}