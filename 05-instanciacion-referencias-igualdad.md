- [5. Instanciación, Referencias, Igualdad y ToString](#5-instanciación-referencias-igualdad-y-tostring)
  - [5.1 Creación e instanciación de objetos](#51-creación-e-instanciación-de-objetos)
    - [5.1.1 new y sintaxis básica](#511-new-y-sintaxis-básica)
    - [5.1.2 Object initializers y uso con init-only](#512-object-initializers-y-uso-con-init-only)
    - [5.1.3 Target-typed new](#513-target-typed-new)
    - [5.1.4 Comparativa: constructor vs object initializer](#514-comparativa-constructor-vs-object-initializer)
    - [5.1.5 Constructores secundarios](#515-constructores-secundarios)
  - [5.2 Referencia this](#52-referencia-this)
    - [5.2.1 Uso para acceder a miembros de la instancia](#521-uso-para-acceder-a-miembros-de-la-instancia)
    - [5.2.2 Uso en encadenamiento de constructores](#522-uso-en-encadenamiento-de-constructores)
    - [5.2.3 Ejemplos prácticos](#523-ejemplos-prácticos)
  - [5.3 Representación como cadena](#53-representación-como-cadena)
    - [5.3.1 Override ToString()](#531-override-tostring)
    - [5.3.2 Interpolación de cadenas ($"...")](#532-interpolación-de-cadenas-)
  - [5.4 Igualdad e identidad](#54-igualdad-e-identidad)
    - [5.4.1 ReferenceEquals y comportamiento por defecto](#541-referenceequals-y-comportamiento-por-defecto)
    - [5.4.2 Igualdad por valor vs por referencia](#542-igualdad-por-valor-vs-por-referencia)
    - [5.4.3 Sobrescribir Equals(object) y GetHashCode()](#543-sobrescribir-equalsobject-y-gethashcode)


# 5. Instanciación, Referencias, Igualdad y ToString

En este bloque aprenderemos cómo se crean los objetos en memoria y cómo el lenguaje gestiona su identidad y representación.

## 5.1 Creación e instanciación de objetos

Crear (“instanciar”) un objeto es el proceso de obtener una unidad concreta de una clase.

### 5.1.1 new y sintaxis básica

Cuando usas `new`, le pides al sistema que cree un objeto nuevo de acuerdo a la definición de su clase.

**Ejemplo:**
```csharp
var miGato = new Gato();
miGato.Nombre = "Felix";
miGato.Edad = 3;
```

Cada vez que usas `new`, se crea una referencia nueva en la memoria para ese objeto. Se almacena en el heap, y la variable (`miGato`) contiene la referencia a esa ubicación.

### 5.1.2 Object initializers y uso con init-only

Los **initializers** permiten asignar propiedades al momento de crear el objeto.
```csharp
var miLibro = new Libro { Titulo = "1984", Autor = "Orwell" };
```

Si la propiedad es `init`, sólo puede establecerse en el initializer o el constructor: no se podrá modificar después.
```csharp
public class Usuario
{
    public string Nombre;
}

var usuario = new Usuario { Nombre = "Marta" }; // Ok
// usuario.Nombre = "Ana"; // Error: sólo puede fijarse al principio
```

### 5.1.3 Target-typed new

Puedes usar la nueva sintaxis con tipos conocidos para acortar el código desde C# 9:
```csharp
Gato miGato = new("Miau", 2); // Si existe un constructor Gato(string, int)
```

### 5.1.4 Comparativa: constructor vs object initializer

Usa **constructores** cuando necesitas garantizar algún valor sí o sí al crear el objeto, y **initializers** cuando quieres flexibilidad y claridad.

Ejemplo:
```csharp
var gato = new Gato("Felix", 2); // usando constructor, no puedes olvidar la edad
var libro = new Libro { Titulo = "1984", Autor = "Orwell" }; // usando initializer, puedes dejar Autor vacío (no recomendado)
```

### 5.1.5 Constructores secundarios
Puedes definir varios constructores en una clase para diferentes formas de crear objetos. Usa `this(...)` para llamar a otro constructor y evitar repetir código. Lo veremos en detalle en el apartado de constructores.
```csharp
public class Coche
{
    public string Marca;
    public string Modelo;
    public int Año;

    // Constructor secundario que usa el principal
    public Coche(string marca, string modelo) : this(marca, modelo, 0) { }

    // Constructor principal
    public Coche(string marca, string modelo, int año) {
        Marca = marca;
        Modelo = modelo;
        Año = año;
    }
}
```

---

## 5.2 Referencia this

La palabra **this** en C# siempre se refiere al objeto actual, es decir, el mismo sobre el que se está ejecutando el código.
Ayuda a distinguir entre los atributos del objeto y los parámetros con el mismo nombre. Es importante tenerlo claro para evitar confusiones que al usar this nos referimos a la instancia actual en la que se está ejecutando el método o constructor.

### 5.2.1 Uso para acceder a miembros de la instancia

**Ejemplo:**
```csharp
public class Punto
{
    private int x;
    public void SetX(int x) {
        this.x = x; // “this.x” es el atributo, “x” es el parámetro
    }
}
```
Aquí, usas `this.x` para asegurarte de referirte al atributo de la instancia.

### 5.2.2 Uso en encadenamiento de constructores

Puedes usar `this` para llamar a otro constructor de la misma clase, reutilizando lógica.

**Ejemplo:**
```csharp
public class Persona
{
    public string Nombre;
    public int Edad;

    public Persona(string nombre) : this(nombre, 0) { }

    public Persona(string nombre, int edad) {
        Nombre = nombre;
        Edad = edad;
    }
}
```

### 5.2.3 Ejemplos prácticos

En métodos, `this` te permite referenciar la instancia actual para modificar datos o llamar a otros métodos de esa instancia.
Puedes incluso pasar `this` como argumento a otros métodos.

---

## 5.3 Representación como cadena

Es útil que tus objetos puedan convertirse a texto para mostrar información, depurar o guardar.

### 5.3.1 Override ToString()
Siempre puedes sobrescribir el método `ToString()` en tu clase para devolver una representación legible.

**Ejemplo:**
```csharp
public class Persona
{
    public string Nombre;
    public int Edad;
    public override string ToString() => $"Persona: {Nombre}, {Edad} años";
}
```
Ahora, si imprimes el objeto, verás ese texto en consola.

### 5.3.2 Interpolación de cadenas ($"...")

La interpolación (`$`) permite mezclar variables y texto fácilmente:
```csharp
var nombre = "Jose";
var mensaje = $"Hola, {nombre}!";
Console.WriteLine(mensaje); // Imprime: Hola, Jose!
```
Úsalo en ToString() y otros métodos para hacer el código más legible.

## 5.4 Igualdad e identidad

Cuando trabajas con objetos, es muy importante distinguir entre comparar si son **el mismo objeto** (identidad) o si solo tienen **el mismo estado** (igualdad).

### 5.4.1 ReferenceEquals y comportamiento por defecto
Una comparación por identidad verifica si dos referencias apuntan al mismo objeto en memoria.

**Ejemplo:**
```csharp
var a = new Libro { Titulo = "1984" };
var b = new Libro { Titulo = "1984" };
Console.WriteLine(object.ReferenceEquals(a, b)); // False: diferente objeto
var c = a;
Console.WriteLine(object.ReferenceEquals(a, c)); // True: misma referencia
```

### 5.4.2 Igualdad por valor vs por referencia

Por defecto, en C#, la comparación con `==` entre objetos compara la referencia (identidad) salvo que la clase sobrescriba el método `Equals`.

Para comparar el **estado** (contenido):
- Sobreescribe el método `Equals`
- Sobreescribe también `GetHashCode`

### 5.4.3 Sobrescribir Equals(object) y GetHashCode()

**Ejemplo:**
```csharp
public class Libro
{
    public string Titulo;

    public override bool Equals(object? obj)
    {
        if (obj is Libro otro)
            return Titulo == otro.Titulo;
        return false;
    }
    public override int GetHashCode() => Titulo.GetHashCode();
}
```
Así, dos libros con el mismo título serán iguales por estado, aunque no tengan la misma identidad.

```csharp
var libro1 = new Libro { Titulo = "1984" };
var libro2 = new Libro { Titulo = "1984" };
Console.WriteLine(libro1.Equals(libro2)); // True: mismo estado

if (libro1.Equals(libro2))
    Console.WriteLine("Mismo estado");

if (libro1.GetHashCode() == libro2.GetHashCode())
    Console.WriteLine("Mismo hash code");

if (libro1 == libro2)
    Console.WriteLine("Mismo estado, porque tiene equals");

if (object.ReferenceEquals(libro1, libro2))
    Console.WriteLine("Misma referencia"); // No se cumple
```

Si tenemos varios campos
```csharp
public class Gato {
    public int Edad;
    public bool EsDomestico;
    public string Nombre;
    public string Raza;


    public override string ToString() {
        return $"[Gato: {Nombre}, Edad: {Edad}, Raza: {Raza}, Doméstico: {EsDomestico}]";
    }

    public override int GetHashCode() {
        return HashCode.Combine(Nombre, Edad, Raza, EsDomestico);
    }

    public override bool Equals(object obj) {
        if (obj is Gato gato)
            return gato.Nombre == Nombre && gato.Edad == Edad && gato.Raza == Raza && gato.EsDomestico == EsDomestico;
        return false;
    }
}
```

---


