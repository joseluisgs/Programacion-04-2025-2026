namespace PropMetodosClase;

public class Persona {
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public int Id { get; private set; } = ContadorPersonas + 1;
    
    public static int ContadorPersonas { get; private set; } = 0;
    public static String MensajeCompartido { get; set; } = "Este es un mensaje compartido entre todas las personas.";

    public Persona() {
        ContadorPersonas++;
    }
    
    public void Saludar() {
        Console.WriteLine($"Hola, mi nombre es {Nombre} y tengo {Edad} años . Mi ID es {Id}.");
    }

    public override string ToString() {
        return $"Persona[ID={Id}, Nombre={Nombre}, Edad={Edad}]";
    }

    public static void MostrarContadorPersonas() {
        Console.WriteLine($"Hay {ContadorPersonas} personas en total.");
    }
}