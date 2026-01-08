namespace AtributosPropiedades;

public class PersonaA(string nombre, int edad, string dni) {
    private string _nombre = nombre;
    private int _edad = edad;
    private readonly string _dni = dni;

    public string GetNombre() {
        return _nombre;
    }
    public void SetNombre(string nombre) {
        _nombre = nombre;
    }
    public int GetEdad() {
        return _edad;
    }

    public void SetEdad(int edad) {
        _edad = edad;
    }
    public string GetDni() {
        return _dni;
    }

    /*public void SetDni(string dni) {
        _dni = dni;
    }*/
}