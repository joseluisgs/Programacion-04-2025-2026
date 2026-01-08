namespace PropMetodosClase;

public static class Utilidades {
    public static bool EsPalindromo(string texto) {
        if (string.IsNullOrEmpty(texto)) return false;
        // Console.WriteLine($"Texto original: {texto}");
        var textoReverso = new string(texto.Reverse().ToArray());
        // Console.WriteLine($"Texto reverso: {textoReverso}");
        return texto == textoReverso;
    }
    
    public static bool EsPar(int number) {
        return number % 2 == 0;
    }

    public static bool EsPrimo(int number) {
        if (number <= 1) return false;
        for (var i = 2; i <= Math.Sqrt(number); i++) {
            if (number % i == 0) return false;
        }
        return true;
    }
}