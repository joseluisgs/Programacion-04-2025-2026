namespace ClasesObjetos;

public class Gato {
    // Estado, debemos poner el modificador de acceso
    // Lo recomendable es que sea privado todo lo que no se quiera
    // que sea accesible desde fuera de la clase
    public string Nombre;
    private int _edad;
    
    // Comportamiento
    // Lo ideal es que crear la interfaz pública de la clase
    // a través de métodos públicos (public)
    
    public void Maullar() {
        Console.WriteLine($"{Nombre} Miau Miau");
    }
    
    void JugarConRaton() {
        Console.WriteLine("El gato está jugando con el ratón");
    }

    void Dormir() {
        Console.WriteLine("Zzzzzzzzzzzzzzzzz");
    }

    public int GetEdad() {
        return _edad;
    }
}