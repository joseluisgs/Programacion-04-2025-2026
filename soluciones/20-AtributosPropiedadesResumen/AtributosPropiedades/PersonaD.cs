namespace AtributosPropiedades;

public class PersonaD(string nombre, int edad, string dni) {
    public void SetNombre(string n) {
        nombre = n;
    }
    public override string ToString() => $"Nombre: {nombre}, Edad: {edad}, DNI: {dni}";
}