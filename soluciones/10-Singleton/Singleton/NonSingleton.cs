namespace Singleton;

public class NonSingleton {
    
    private static int _contador = 0;
    
    public NonSingleton() {
        _contador++;
    }

    public override string ToString() {
        return $"NonSingleton: contador = {_contador}";
    }
}