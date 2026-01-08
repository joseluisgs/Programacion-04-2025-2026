namespace AtributosPropiedades;

public class PersonaC{
    public string Nombre { set; get; }
    public int Edad { set; get; }
    public string Dni { get; init; }
    
    // constrcutor primario va arriba con la clase tambien
    public PersonaC() { }
    
    // Constructor secundario, usa el primario (this())
    public PersonaC(string nombre, int edad, string dni): this() {
        Nombre = nombre;
        Edad = edad;
        Dni = dni;
    }
    
}