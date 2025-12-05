namespace Propiedades.Models;

public class ProductoC {
    public string Nombre { get; set; } = string.Empty;
    public decimal Precio {
        get;
        set => field = value < 0 
            ? throw new ArgumentOutOfRangeException(nameof(value), "El precio no puede ser negativo") 
            : value;
    } = 0.0m;
    public int Cantidad { get; } = 0; // Solo lectura
    public required string Categoria { get; init; } // Solo se puede asignar en la inicialización
    public string Marca { get; private set; } = string.Empty; // Público para lectura y privado para escritura
    
    // esto equivale a que todo el estado sea privado y se acceda a él mediante
    // getters y setters y todo generado por el compilador
    // private string nombre = string.Empty;
    // private decimal precio = 0.0m;
    // public string GetNombre() => nombre;
    // public void SetNombre(string value) => nombre = value;
    // public decimal GetPrecio() => precio;
    // public void SetPrecio(decimal value) => precio = value;
}