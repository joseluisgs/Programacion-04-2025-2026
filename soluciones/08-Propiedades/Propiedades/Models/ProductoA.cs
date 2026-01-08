namespace Propiedades.Models;

public class ProductoA {
    // En este caso, el estado es problemático, ya que los campos son públicos
    // Y con ello cualquiera puede modificarlos directamente
    public string Nombre = string.Empty;
    public decimal Precio = 0.0m; // No puedo evitar que el precio sea negativo
    public int Cantidad = 0; // No podemos hacer que no se modifique directamente
    public string Categoria = string.Empty;// como lo congelo una vez asignado?
    public string Marca = string.Empty; // publico para lectura y privado para escritura?
}