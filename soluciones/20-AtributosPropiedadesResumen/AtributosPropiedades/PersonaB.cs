namespace AtributosPropiedades;

public class PersonaB(string nombre, int edad, string dni) {
    private string _nombre = nombre;
    private int _edad = edad;
    private string _dni = dni;

    public string Nombre {
        get { return _nombre; }
        set { _nombre = value; }
    }
    
    public int Edad {
        get { return _edad; }
        set { _edad = value; }
    }
    
    public string Dni {
        get { return _dni; }
        init { _dni = value; }
    }
}