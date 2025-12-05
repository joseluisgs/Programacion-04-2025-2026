using Serilog;

namespace Cine.Utility;

public static class Utility {
    private static readonly ILogger _log = Log.ForContext(typeof(Utility));

    public static int Indice(char letra) {
        //_log.Debug("Obteniendo indice de fila para letra: {letra}", letra);
        return letra - 'A';
    }

    public static char Letra(int indice) {
        //_log.Debug("Obteniendo letra de fila para indice: {indice}", indice);
        return (char)('A' + indice);
    }
}