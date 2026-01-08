namespace Singleton;

public class SingletonA {
    private static SingletonA? _instancia; // Variable estática para almacenar la única instancia
    private static int _contador = 0;

    // Constructor privado para evitar la creación de nuevas instancias
    private SingletonA() {
        _contador++;
    }

    // Método estático para obtener la única instancia
    public static SingletonA Instancia() {
        /*if (_instancia == null) {
            _instancia = new SingletonA();
        }*/
        _instancia ??= new SingletonA();
        return _instancia;
    }

    public override string ToString() {
        return $"SingletonA: contador = {_contador}";
    }
}