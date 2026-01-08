- [6. Constructores e inicialización](#6-constructores-e-inicialización)
  - [6.1 Propósito de un constructor](#61-propósito-de-un-constructor)
  - [6.2 Constructor por defecto](#62-constructor-por-defecto)
  - [6.3 Constructores parametrizados](#63-constructores-parametrizados)
  - [6.4 Sobrecarga de constructores](#64-sobrecarga-de-constructores)
  - [6.5 Encadenamiento de constructores (this(...))](#65-encadenamiento-de-constructores-this)
  - [6.6 Constructor estático](#66-constructor-estático)
  - [6.7 Constructores privados](#67-constructores-privados)
  - [6.8 Constructor de copia](#68-constructor-de-copia)
  - [6.9 Parámetros opcionales, named arguments y params](#69-parámetros-opcionales-named-arguments-y-params)
  - [6.10 Inicialización de campos readonly](#610-inicialización-de-campos-readonly)
  - [6.11 Constructores en structs y records](#611-constructores-en-structs-y-records)
  - [6.12 Manejo de errores en constructores](#612-manejo-de-errores-en-constructores)
  - [6.13 Constructores primarios y secundarios (C# 12+)](#613-constructores-primarios-y-secundarios-c-12)
  - [6.14 Buenas prácticas en constructores](#614-buenas-prácticas-en-constructores)


# 6. Constructores e inicialización

Un **constructor** es el método especial que se llama al crear un objeto, y sirve para establecer su estado inicial. Es como el “ritual de bienvenida” de cada objeto. En el fondo es una función con el mismo nombre que la clase, sin tipo de retorno (ni siquiera `void`), se encarga de reservar memoria y preparar el objeto para su uso.

## 6.1 Propósito de un constructor

Los constructores te obligan a definir el estado inicial de cada objeto, asegurando que todos empiecen bien configurados.

## 6.2 Constructor por defecto

Si no defines ningún constructor, C# crea uno sin parámetros que inicializa los atributos a sus valores por defecto.
**Ejemplo:**
```csharp
public class Animal
{
    public string Nombre; // null por defecto
    public int Edad;      // 0 por defecto
}
var perro = new Animal(); // Usa el constructor por defecto
```

## 6.3 Constructores parametrizados

Permiten pasar información para personalizar el estado al crear el objeto.

**Ejemplo:**
```csharp
public class Animal
{
    public string Nombre;
    public Animal(string nombre) {
        Nombre = nombre;
    }
}
```

## 6.4 Sobrecarga de constructores

Puedes definir varios constructores para permitir distintos modos de inicialización. Con la sobrecarga, cada constructor tiene una firma diferente (número/tipo de parámetros). De esta forma, puedes crear objetos con diferentes niveles de detalle.
Deberemos usar this, para evitar repetir código entre constructores y utilizar el constructor más completo como base.

**Ejemplo:**
```csharp
public class Animal
{
    public string Nombre;
    public int Edad;

    // Sobrecarga de constructores
    // Si nos pasan solo el nombre, edad será 0 por defecto
    public Animal(string nombre) : this(nombre, 0) { }

    // Constructor principal
    public Animal(string nombre, int edad) {
        Nombre = nombre;
        Edad = edad;
    }
}
```

## 6.5 Encadenamiento de constructores (this(...))

Utiliza `this(...)` para reutilizar lógica y evitar errores, como hemos visto en el ejemplo anterior.
```csharp
public class Libro
{
    public string Titulo;
    public string Autor;
    public int Paginas;

    // Constructor secundario
    public Libro(string titulo) : this(titulo, "Desconocido", 0) { }

    // Constructor principal
    public Libro(string titulo, string autor, int paginas) {
        Titulo = titulo;
        Autor = autor;
        Paginas = paginas;
    }
}

var libro1 = new Libro("1984"); // Autor="Desconocido", Paginas=0
var libro2 = new Libro("1984", "Orwell", 328);
```

## 6.6 Constructor estático

Un constructor `static` se ejecuta una sola vez antes de que se use la clase (para inicializar datos compartidos). Lo veremos más adelante en detalle.

**Ejemplo:**
```csharp
public class Configuracion
{
    public static string RutaConfig;
    static Configuracion() {
        RutaConfig = "/etc/config";
    }
}
```

## 6.7 Constructores privados

Sirven para controlar cómo se instancian los objetos, por ejemplo, en patrones singleton o factories. Lo veremos más adelante.
```csharp
public class Singleton
{
    private static Singleton instancia;
    private Singleton() { }
    public static Singleton ObtenerInstancia() {
        if (instancia == null) {
            instancia = new Singleton();
        }
        return instancia;
    }
}
```

## 6.8 Constructor de copia

Crea una nueva instancia copiando el estado de otro objeto (útil para duplicar objetos sin compartir referencia).
```csharp
public class Punto
{
    public int X;
    public int Y;

    // Constructor de copia
    public Punto(Punto otro) {
        X = otro.X;
        Y = otro.Y;
    }
}

var p1 = new Punto { X = 5, Y = 10 };
var p2 = new Punto(p1); // p2 es una copia de p1
```

## 6.9 Parámetros opcionales, named arguments y params

Puedes facilitar distintos modos de uso con parámetros por defecto y argumentos con nombre, como vimos en funciones.
```csharp
public class Rectangulo
{
    public Rectangulo(int alto = 10, int ancho = 5) { ... }
}
var r1 = new Rectangulo(); // usa los valores por defecto
var r2 = new Rectangulo(ancho: 15); // alto=10, ancho=15
```

## 6.10 Inicialización de campos readonly

Los campos `readonly` pueden fijarse solo una vez (en la declaración o en el constructor), ayudando a crear objetos inmutables, de esta manera que no se puedan modificar después de ser creados.
```csharp
public class Circulo
{
    public readonly double Radio;
    public Circulo(double radio) {
        Radio = radio;
    }
}

var c = new Circulo(5.0);
// c.Radio = 10.0; // Error: no se puede modificar después del constructor
```

## 6.11 Constructores en structs y records

- **Structs:** Debes inicializar todos los campos en el constructor; hay uno por defecto si no defines ninguno.
- **Records:** Suelen tener constructores posicionales (`record Persona(string Nombre, int Edad)`), y soportan inicialización con `with` para crear copias modificadas.

## 6.12 Manejo de errores en constructores
Lanza excepciones si los argumentos son inválidos para asegurar objetos consistentes desde el principio.
**Ejemplo:**
```csharp
public class CuentaBancaria
{
    public decimal Saldo;

    public CuentaBancaria(decimal saldoInicial) {
        if (saldoInicial < 0)
            throw new ArgumentException("El saldo inicial no puede ser negativo.");
        Saldo = saldoInicial;
    }
}

var cuenta = new CuentaBancaria(-100); // Lanza excepción
```

## 6.13 Constructores primarios y secundarios (C# 12+)
Los **constructores primarios** permiten definir parámetros directamente en la declaración de la clase, simplificando la sintaxis para clases simples. Esta característica está disponible a partir de C# 12.
```csharp
public class Persona(string nombre, int edad)
{
    public string Nombre { get; } = nombre;
    public int Edad { get; } = edad;
}

var p = new Persona("Ana", 30);
```

Esta forma es ideal para simplificar la inyección de dependencias con campos `readonly`.
```csharp
public class ServicioCorreo(string servidorSmtp, int puerto)
{
    private readonly string _servidorSmtp = servidorSmtp;
    private readonly int _puerto = puerto;

    public void EnviarCorreo(string destinatario, string asunto, string cuerpo) {
        // Lógica para enviar correo usando _servidorSmtp y _puerto
    }
}
```

Los constructores secundarios pueden seguir definiéndose como antes, y pueden llamar al constructor primario usando `: this(...)`.

```csharp
public class Producto(string nombre, decimal precio)
{
    public string Nombre { get; } = nombre;
    public decimal Precio { get; } = precio;

    // Constructor secundario que asigna un precio por defecto
    public Producto(string nombre) : this(nombre, 0.0m) { }
}
```

## 6.14 Buenas prácticas en constructores

- Hazlos simples; delega procesos largos a métodos o factories.
- Mantén siempre los invariantes del objeto.
- Usa excepciones para validar argumentos.
- Prefiere constructores parametrizados sobre el uso excesivo de setters después de crear el objeto.
- No tengas lógica pesada dentro de los constructores.
- Usa parámetros con valores por defecto para evitar múltiples sobrecargas innecesarias.
- Siempre que puedas, para una sintaxis más limpia, usa initializers o constructores primarios (C# 12+) en lugar de múltiples constructores secundarios.
