namespace _12_MoscaMatriz.Model;

public class Mosca {
    public enum Estado {
        Viva,
        Muerta
    }

    public required int Vida { get; set; }

    public Estado EstadoActual => Vida > 0 ? Estado.Viva : Estado.Muerta;

    public void Golpear() {
        if (Vida > 0) Vida--;
    }
}