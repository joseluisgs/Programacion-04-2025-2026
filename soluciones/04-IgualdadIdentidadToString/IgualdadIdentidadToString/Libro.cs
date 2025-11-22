namespace IgualdadIdentidadToString;

public class Libro {
    public string Titulo;

    // Poder imprimirlo, hay que hacer el ToString
    public override string ToString() {
        return $"[Libro: {Titulo}]";
    }

    // HasCode: Se usa para comparar objetos en colecciones
    // Si dos objetos son iguales, deben tener el mismo HasCode
    // Si dos objetos tienen el mismo HasCode, no necesariamente son iguales
    public override int GetHashCode() {
        return Titulo.GetHashCode();
    }

    // Equals: Compara si dos objetos son iguales en contenido
    public override bool Equals(object? obj) {
        if (obj is Libro otroLibro)
            return Titulo == otroLibro.Titulo;
        return false;
    }
}