namespace Cine.Structs;

public record Informe {
    public required int EntradasVendidas { get; init; }
    public required decimal AsientosLibres { get; init; }
    public required decimal AsientosFueraDeServicio { get; init; }
    public required double PorcentajeDeOcupacion { get; init; }
    public required decimal RecaudacionTotal { get; init; }
}