namespace Singleton;

public class SingletonB {
    private static int _contador = 0;
    private static readonly SingletonB _instancia = new SingletonB();
    public static SingletonB Instancia => _instancia;
    
    private SingletonB()
    {
        _contador++;
    }

    public override string ToString()
    {
        return $"SingletonB: contador = {_contador}";
    }
}