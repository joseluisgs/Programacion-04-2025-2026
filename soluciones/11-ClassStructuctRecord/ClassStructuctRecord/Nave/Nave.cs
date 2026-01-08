using System.Text.RegularExpressions;

namespace ClassStructuctRecord.Nave;

public class Nave {
    public string Id { get; init; }
    public Tipo TipoNave { get; init; }

    public Manifiesto? Manifiesto {
        get;
        set => field = ValidarManifiesto(value);
    }

    

    // Constructor
    public Nave(string id, Tipo tipoNave = Tipo.Batalla, Manifiesto? manifiesto = null) {
        if (!ValidarId(id)) {
            throw new ArgumentException("El Id de la nave no cumple con el formato LLLNNNL");
        }
        Id = id;
        TipoNave = tipoNave;
        if (tipoNave == Tipo.Carga && manifiesto == null) {
            throw new ArgumentException("Las naves de carga deben tener un manifiesto");
        }
        Manifiesto = ValidarManifiesto(manifiesto);
    }
    
    public override string ToString() => $"Nave Id: {Id}, Tipo: {TipoNave}, Manifiesto: {Manifiesto}";
    
    // Nasted Enum
    public enum Tipo {
        Carga,
        Batalla
    }
    
    private bool ValidarId(string id) {
        // LLLNNNL 
        var regex = new Regex("^[A-Z]{3}[0-9]{3}[A-Z]{1}$");
        return regex.IsMatch(id);
    }
    
    // Como manifiesto es una entidad debil, que no existe 
    // Sin una nave de carga, validamos sus valores aquí
    private Manifiesto? ValidarManifiesto(Manifiesto? value) {
        if (TipoNave == Tipo.Carga && value == null) {
            throw new ArgumentException("Las naves de carga deben tener un manifiesto");
        }
        if (TipoNave == Tipo.Batalla && value != null) {
            throw new ArgumentException("Las naves de batalla no pueden tener un manifiesto");
        }
        // Comprobamos que los valores del manifiesto sean válidos
        var manifiesto = value!.Value;
        // Peso de la carga entre 100 y 10_000 toneladas
        if (manifiesto.PesoCarga < 100 || manifiesto.PesoCarga > 10_000) {
            throw new ArgumentException("El peso de la carga debe estar entre 100 y 10_000 toneladas");
        }
        // valor de la carga entre 99,99 y 999_999,99
        if (manifiesto.ValorTotal < 99.99m || manifiesto.ValorTotal > 999_999.99m) {
            throw new ArgumentException("El valor total de la carga debe estar entre 99,99 y 999_999,99");
        }
        return manifiesto;
    }
    
}