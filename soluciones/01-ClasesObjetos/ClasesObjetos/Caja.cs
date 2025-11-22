namespace ClasesObjetos;

// Opciones de acceso: public, private, protected, internal
public class Caja {
    private string _contenido; // sólo accesible por métodos de Caja
    public string Tipo; // accesible desde fuera

    private int miPeso() {
        return _contenido.Length * 2; // ejemplo simple
    }

    public void Guardar(string item) {
        _contenido = item;
    }
}