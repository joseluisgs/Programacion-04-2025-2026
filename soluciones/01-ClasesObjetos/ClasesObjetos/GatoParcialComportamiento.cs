namespace ClasesObjetos;

public partial class GatoParcial {
    public void Maullar() {
        Console.WriteLine($"{Nombre} Miau Miau");
    }

    public void JugarConRaton() {
        Console.WriteLine("El gato está jugando con el ratón");
    }

    public void Dormir() {
        Console.WriteLine("Zzzzzzzzzzzzzzzzz");
    }

    public int IsVacunado() {
        return EstadoVacunado ? 1 : 0;
    }

    public bool IsMayor() {
        return Edad >= 10;
    }
}