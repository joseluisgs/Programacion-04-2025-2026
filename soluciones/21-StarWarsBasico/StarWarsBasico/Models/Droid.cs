namespace StarWarsBasico.Models;

/// <summary>
///     Representa un droide en el universo de Star Wars.
/// </summary>
public class Droid(int maxEnergy, Droid.DroidType type, int defense, int shield, int velocity) {
    /// <summary>
    ///     Tipos de droides disponibles.
    /// </summary>
    public enum DroidType {
        Sw348,
        Sw447,
        Sw421
    }

    public int Defense { get; set; } = defense;
    public int MaxEnergy { get; set; } = maxEnergy;
    public int Shield { get; set; } = shield;
    public DroidType Type { get; set; } = type;
    public int Velocity { get; set; } = velocity;

    public string Color =>
        Type switch {
            DroidType.Sw348 => "🔴",
            DroidType.Sw447 => "🔵",
            DroidType.Sw421 => "⚪",
            _ => "❓"
        };

    public bool IsAlive => MaxEnergy > 0;

    public override string ToString() {
        return $"Droid(maxEnergy={MaxEnergy}, type={Type}, defense={Defense}, shield={Shield}, velocity={Velocity})";
    }

    /// <summary>
    ///     Acción de defenderse solo para droides tipo SW348.
    /// </summary>
    /// <param name="damage">Daño que recibirá</param>
    /// <returns>Daño final</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public int Defend(int damage) {
        if (Type != DroidType.Sw348)
            throw new InvalidOperationException("Este tipo de droide no puede defenderse");
        Console.WriteLine($"Enemigo trata de defenderse con defensa: {Defense}");
        return Math.Min(damage, Defense);
    }

    /// <summary>
    ///     Acción de moverse solo para droides tipo SW421.
    /// </summary>
    /// <returns>Si escapa o no</returns>
    /// <exception cref="InvalidOperationException"></exception>
    public bool Move() {
        if (Type != DroidType.Sw421)
            throw new InvalidOperationException("Este tipo de droide no puede moverse");
        Console.WriteLine($"Este droide se mueve con velocidad {Velocity}");
        return new Random().Next(1, 101) <= Velocity;
    }
}