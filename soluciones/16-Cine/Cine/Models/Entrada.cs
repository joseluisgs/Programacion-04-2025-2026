namespace Cine.Structs;

public record class Entrada {
    public required Butaca Butaca { get; init; }
    public required DateTime FechaCompra { get; init; }
    public required Butaca.Tipo Categoria { get; init; }
    public required decimal Precio { get; init; }
}