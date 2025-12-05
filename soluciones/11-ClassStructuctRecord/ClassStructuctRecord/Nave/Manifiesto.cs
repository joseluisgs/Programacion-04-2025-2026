using System.Globalization;

namespace ClassStructuctRecord.Nave;

// Locale en España

public struct Manifiesto {
    public int PesoCarga { get; init; }
    public decimal ValorTotal { get; init; }

    public override string ToString() {
        var culture = new CultureInfo("es-ES");
        return $"Peso de la carga: {PesoCarga.ToString("F2", culture)} toneladas, Valor total: {ValorTotal.ToString("C", culture)}";
    }

    /*public Manifiesto(int pesoCarga, decimal valorTotal) {
        PesoCarga = pesoCarga;
        ValorTotal = valorTotal;
    }*/
}