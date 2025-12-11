using StarWarsBasico.Models;

namespace StarWarsBasico.Factories;

/// <summary>
///     Factory para crear droides aleatorios.
/// </summary>
public static class DroidFactory {
    /// <summary>
    ///     Crea un droide aleatorio basado en probabilidades predefinidas.
    /// </summary>
    public static Droid RandomDroid() {
        var random = new Random().Next(1, 101);
        return random switch {
            <= 30 => new Droid(50, Droid.DroidType.Sw348, new Random().Next(9, 13), 0, 0),
            <= 80 => new Droid(100, Droid.DroidType.Sw447, 0, new Random().Next(5, 11), 0),
            _ => new Droid(new Random().Next(100, 151), Droid.DroidType.Sw421, 0, 0, new Random().Next(10, 31))
        };
    }
}