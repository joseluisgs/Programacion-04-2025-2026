namespace ClassStructuctRecord;

public record PersonaRecord(string Nombre, int Edad);

// si necesitamos validar campos o agregar logica adicional
// podemos usar el siguiente formato:
// public record PersonaRecord {
//     public string Nombre {
//         get;
//         init = field = ValidarNombre(value);
//     }
//     public int Edad { get; init; }
//