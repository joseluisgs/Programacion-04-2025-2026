
# Ejercicios de Programación Orientada a Objetos (POO)

- [Ejercicios de Programación Orientada a Objetos (POO)](#ejercicios-de-programación-orientada-a-objetos-poo)
  - [I. Ejercicios Base](#i-ejercicios-base)
    - [1. Ejercicio del Reloj](#1-ejercicio-del-reloj)
    - [2. Clase Fracción (Racional)](#2-clase-fracción-racional)
    - [3. El Coche](#3-el-coche)
    - [4. Clase Ordenador y Aula](#4-clase-ordenador-y-aula)
    - [5. Manejo de Conjuntos](#5-manejo-de-conjuntos)
    - [6. Serpiente](#6-serpiente)
    - [7. La Mosca Orientada a Objetos](#7-la-mosca-orientada-a-objetos)
    - [8. Buscaminas Orientado a Objetos](#8-buscaminas-orientado-a-objetos)
    - [9. Cuenta Bancaria](#9-cuenta-bancaria)
    - [10. Conquistadores de Catán (Simplificado)](#10-conquistadores-de-catán-simplificado)
  - [II. Ejercicios Adicionales (Conceptos Avanzados de POO)](#ii-ejercicios-adicionales-conceptos-avanzados-de-poo)
    - [11. Modelado de Geometría con Tipos por Valor e Inmutabilidad](#11-modelado-de-geometría-con-tipos-por-valor-e-inmutabilidad)
    - [12. Patrón Builder para la Configuración de Mensajes](#12-patrón-builder-para-la-configuración-de-mensajes)
    - [13. Miembros Estáticos y Contadores de Instancias](#13-miembros-estáticos-y-contadores-de-instancias)
    - [14. Encapsulamiento y Validación con Propiedades Completas](#14-encapsulamiento-y-validación-con-propiedades-completas)
    - [15. Gestión de Recursos con Singleton y Carga Perezosa](#15-gestión-de-recursos-con-singleton-y-carga-perezosa)


## I. Ejercicios Base

### 1. Ejercicio del Reloj
Realiza una clase `Reloj` que sea capaz de almacenar la hora de un reloj (hora, minuto y segundo) y el modo en el que se mostrará la hora (versión 12 o 24 horas).

Se deben implementar:
*   Los métodos habituales (constructores, *getters* y *setters*).
*   Algún método especial que permita recargar pila.
*   Sobrecargar el método `toString` para mostrar la hora.

### 2. Clase Fracción (Racional)
Crea una clase `Racional` que permita trabajar con números racionales (fracciones).

Incluye los siguientes métodos:
*   Constructores (por defecto y parametrizados), `toString`, *getters* y *setters*.
*   `leer();` → que pide la fracción por teclado.
*   `c=sumar(a,b);` → que suma las fracciones `a` y `b`, devolviendo la fracción simplificada `c`.
*   `c=multiplicar(a,b);` → que multiplica las fracciones `a` y `b`, devolviendo la fracción simplificada `c`.
*   `a = simplificar(b);` → método que simplifica la fracción `b` devolviéndola en la `a`.

### 3. El Coche
Realiza una clase `Coche` con atributos que reflejen su marca, modelo, color y matrícula. También debe almacenar información acerca de sus características de movimiento: motor encendido o apagado, `marchaActual`, `velocidadActual`, `subirMarcha`, `bajarMarcha` y aquellos que creas conveniente para manipular su información dinámica.

Crea los métodos necesarios que permitan simular lo siguiente:
a) El coche parte de una situación de reposo.
b) Arranca.
c) Acelera e irá subiendo marchas hasta llegar a una velocidad que se ha pedido por teclado al usuario.
d) Se mantiene esa velocidad un tiempo que se ha pedido al usuario por teclado.
e) Se va desacelerando y bajando marchas hasta que el coche se pare.
f) Punto muerto y paramos el motor.

**Consideraciones para la simulación:**
*   Rangos de marcha: 1ª 0 – 30; 2ª 30 – 50; 3ª 50 – 70; 4ª 70 – 100; 5ª 100 →.
*   El 10% de las veces se nos cruza un gato. Para evitar pillarlo se debe simular un frenazo que pare el coche y cale el motor.

### 4. Clase Ordenador y Aula
Crea una clase que almacene la información técnica de un `Ordenador`.

Crea también una clase `Aula` que permita:
*   Almacenar la información de todos los ordenadores almacenados en un aula.
*   Guardar el nombre del aula y el curso que se imparte en la misma.
*   El programa principal debe permitir gestionar dicha aula.
*   Queda a elección del alumno definir los atributos y métodos necesarios.

### 5. Manejo de Conjuntos
Realiza una clase `Conjunto` que permita crear conjuntos matemáticos.
Estos conjuntos deben contener letras en un número indefinido de ellas y sin importar que se repitan.

Crea los métodos habituales y los necesarios para añadir y borrar elementos.

Después se deben implementar las siguientes operaciones con los conjuntos:
*   `C=Intersección(A,B)` → Devuelve el conjunto formado por los elementos comunes en A y B.
*   `C=Unión(A,B)` → Devuelve el conjunto formado por todos los elementos de A y B.
*   `N=Cardinalidad(A)` → Devuelve la cantidad de elementos del conjunto A.

### 6. Serpiente
Diseña un programa orientado a objetos que simule la vida de una `Serpiente` con las siguientes características:
*   Compuesta por anillas de un color diferente cada una, alternando entre (r, v, a).
*   Cuando nace tiene un color asignado al azar.

Se simulará su vida hasta que esté muerta (se quede sin cuerpo), de forma que cada año (segundo):

| Edad              | Comportamiento | Probabilidad | Acción                                               |
| :---------------- | :------------- | :----------- | :--------------------------------------------------- |
| Joven (< 10 años) | Crecerá        | 80%          | Añade una nueva anilla de color aleatorio.           |
| Joven (< 10 años) | Mudará la piel | 20%          | Se cambia el cuerpo respetando el número de anillas. |
| Mayor (> 10 años) | Decrecerá      | 90%          | Quita una anilla a su cuerpo (la última).            |
| Mayor (> 10 años) | Mudará la piel | 10%          | Se cambia el cuerpo respetando el número de anillas. |

*   Aleatoriamente, el 10% de las veces, puede sufrir el ataque de una mangosta (la serpiente muere y se para la simulación).

**Generalización (Nido):**
Generaliza el ejercicio para un `Nido` de serpientes que puede albergar, como máximo, hasta 20 serpientes.
*   Se simulará la vida del nido durante 5 minutos.
*   Cada cinco segundos nacen (si pueden) entre 1 y 3 serpientes, ocupando su lugar en el nido.
*   Cada segundo (año de la vida de una serpiente) pasará la vida para cada serpiente del nido según se especificó anteriormente (menos el ataque de la mangosta).
*   Cada 10 segundos una mangosta aparece por el nido, zampándose entre 0 y 4 serpientes el 20% de las veces.
*   Se debe mostrar el estado del nido cada segundo y describir lo que va ocurriendo.

### 7. La Mosca Orientada a Objetos
Realiza el juego de la `Mosca` que se hizo con matrices, pero orientado a objetos.
Una vez resuelto, amplía el juego añadiendo a la mosca un `tipo` (corriente, verde, negra, coj...) y `vidas`. Las vidas de la mosca indican la cantidad de golpes que tiene que recibir para matarla. Cuando golpeamos una mosca y le quedan vidas, le restamos una vida y revolotea.

### 8. Buscaminas Orientado a Objetos
Realiza el ejercicio del `Buscaminas` pero orientado a objetos. En el programa principal se desarrolla el juego con los métodos proporcionados por la clase correspondiente.

### 9. Cuenta Bancaria
Gestiona una `CuentaBancaria`. Dicha cuenta constará de un número de cuenta, un saldo y tres titulares (como máximo, al menos uno). Para cada `Titular` tenemos que almacenar su DNI, nombre, apellidos y teléfono.

Crea los métodos necesarios que permitan realizar las operaciones habituales:
*   Ingresar y retirar dinero.
*   Añadir/borrar titulares a la misma.
*   Cuando se crea una cuenta debe tener al menos un titular.

El programa principal debe tener menús que permitan gestionar todos los aspectos de la cuenta.

### 10. Conquistadores de Catán (Simplificado)
Implementa un juego conocido como `Conquistadores de Catán` solo para dos jugadores: un humano y el ordenador.

**Funcionamiento:**
*   El juego consiste en un mapa de 3x4 casillas.
*   Cada `Casilla` tendrá un recurso (trigo, madera y carbón), un jugador que será el dueño, y un valor numérico entre 1 y 6 (valores de un dado).
*   Inicialización: El tablero se inicializa colocando en cada casilla un recurso y un valor aleatorios.
*   Almacenes: Al comienzo, los dos jugadores tienen sus almacenes de madera, carbón y trigo a cero.
*   Reparto de casillas: Se preguntará al usuario qué casilla quiere ocupar; luego el ordenador elige una casilla vacía al azar, y así sucesivamente hasta que las 12 casillas estén ocupadas.
*   Juego: Tiran los jugadores por turnos. El valor del dado se comparará con todos los valores de las casillas del tablero. Con aquellas que coincidan, se incrementará la cantidad de recursos que esas casillas indiquen a sus propietarios.
*   Ganará el primero que consiga llegar a 20 en todos los recursos.

---

## II. Ejercicios Adicionales (Conceptos Avanzados de POO)

Estos ejercicios adicionales se centran en la implementación de conceptos avanzados de diseño y buenas prácticas como la Inmutabilidad, *Structs*, *Records*, Patrones y Miembros Estáticos.

### 11. Modelado de Geometría con Tipos por Valor e Inmutabilidad

Diseña un tipo para representar un punto en un plano bidimensional (`Punto2D`) con coordenadas `X` y `Y`.

1.  Implementa `Punto2D` como un **`struct`** para que sea un tipo por valor, adecuado para datos pequeños e inmutables.
2.  Utiliza **Propiedades Auto-implementadas `get; init;`** para asegurar que una vez creado, el punto sea inmutable.
3.  Sobrescribe el método **`ToString()`** para devolver una representación legible de la coordenada, por ejemplo: `(X: 10, Y: 5)`.

### 12. Patrón Builder para la Configuración de Mensajes

Crea una clase `Mensaje` que represente un email o notificación. La clase debe tener propiedades complejas y opcionales (ej: `Destinatario`, `Asunto`, `Cuerpo`, `Prioridad`, `Adjuntos`).

Implementa el patrón **Builder** para la clase `Mensaje`:
1.  Asegúrate de que la clase `Mensaje` sea privada para su instanciación y deba crearse siempre a través de la clase `MensajeBuilder`.
2.  El Builder debe tener métodos encadenables (`.ConAsunto()`, `.ConCuerpo()`, etc.) que sigan el **Patrón Fluent**.
3.  El método final `Construir()` del Builder debe validar que los campos obligatorios (ej: `Destinatario` y `Cuerpo`) existan, lanzando una excepción si no es así.

### 13. Miembros Estáticos y Contadores de Instancias

Crea una clase `Pedido` con un constructor que inicialice sus propiedades (ej: `Id`, `Cliente`).

Implementa una propiedad **estática** llamada `TotalPedidosCreados` dentro de la clase `Pedido`:
1.  Asegura que esta propiedad se incremente automáticamente cada vez que se crea una nueva instancia de `Pedido`.
2.  Esta propiedad debe ser de solo lectura pública (`static readonly` o solo `get`) y reflejar el número total de objetos `Pedido` vivos o creados en el programa, demostrando el uso de **datos compartidos de la clase**.

### 14. Encapsulamiento y Validación con Propiedades Completas

Diseña la clase `Producto` que tiene un campo `Precio` (decimal).

1.  Implementa el `Precio` utilizando una **Propiedad Completa** (con *backing field* explícito o implícito, como `field` en C# 14+).
2.  En el accesor **`set`**, implementa una validación que asegure que el precio nunca puede ser negativo o cero. Si el valor de entrada es inválido, lanza una **`ArgumentException`**, aplicando la buena práctica de **Validación Temprana** para proteger el estado del objeto.
3.  La propiedad debe ser *get-only* una vez inicializada, o manejar un acceso `internal set` para controlar su modificación posterior.

### 15. Gestión de Recursos con Singleton y Carga Perezosa

Crea una clase `GestorRecursos` que simule la carga de recursos de la aplicación (imaginando que es una operación costosa).

1.  Implementa esta clase utilizando el patrón **Singleton**, asegurando que solo exista una única instancia de `GestorRecursos` en todo el sistema.
2.  Utiliza el patrón **Lazy (Perezoso)** para garantizar que la carga de recursos y la creación de la instancia del `GestorRecursos` solo ocurra la primera vez que se accede a ella.
3.  Explique cómo esta combinación de patrones optimiza el **rendimiento inicial** de la aplicación.