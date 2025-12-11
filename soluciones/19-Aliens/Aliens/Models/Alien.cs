using Aliens.Config;

namespace Aliens.Models;

public class Alien {
    private static int _nextId = 1;

    public Alien() {
        Id = _nextId++;
    }

    public int Id { get; }
    public int Vidas { get; set; } = Configuracion.AliensVida;
    public bool IsMuerto => Vidas <= 0;

    public DateTime MomentoDeMuerte { get; set; }

    public void RecibirDisparo() {
        Vidas--;
        if (IsMuerto) MomentoDeMuerte = DateTime.Now;
    }

    public override string ToString() {
        return
            $"Alien {Id} - Vidas: {Vidas} - Muerto: {IsMuerto} - MomentoDeMuerte: {MomentoDeMuerte.ToString("T", Configuracion.Locale)}";
    }
}