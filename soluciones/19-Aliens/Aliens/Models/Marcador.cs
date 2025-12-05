namespace Aliens.Models;

/// <summary>
///     Modelo de Marcador del Jugador.
/// </summary>
public class Marcador {
    public int Tiempo { get; set; }
    public int VidasJugador { get; set; }
    public int AliensEliminados { get; set; }
    public int NumDisparos { get; set; }
    public int NumDisparosAcertados { get; set; }
    public double PrecisionDisparos => NumDisparos == 0 ? 0 : (double)NumDisparosAcertados / NumDisparos * 100;
}