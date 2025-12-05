namespace Propiedades.Models;

public class ProductoB {
    private string _nombre = string.Empty;
    private decimal _precio = 0.0m;
    private int _cantidad = 0;
    //private required string _categoria; // No hay forma de congelarlo y manejar el nulo que tenemos en C#
    private string _marca = string.Empty;
    
    
    // Getters y Setters
    public string GetNombre() => _nombre;
    public void SetNombre(string value) => _nombre = value;
    public decimal GetPrecio() => _precio;

    public void SetPrecio(decimal value) {
        if (value < 0)
            throw new ArgumentException("El precio no puede ser negativo");
        _precio = value;
    }
    // Si no queremos cambiar la cantidad desde fuera, no creamos el setter
    public int GetCantidad() => _cantidad;
    // public string GetCategoria() => _categoria;
    // public void SetCategoria(string value) => _categoria = value;
    public string GetMarca() => _marca;
    private void SetMarca(string value) => _marca = value;
}