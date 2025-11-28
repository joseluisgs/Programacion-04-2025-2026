## Cuestionario: Investigación y Desarrollo (I+D) en POO

- [Cuestionario: Investigación y Desarrollo (I+D) en POO](#cuestionario-investigación-y-desarrollo-id-en-poo)
  - [I. Diseño de Software y Principios](#i-diseño-de-software-y-principios)
  - [II. Modelado de Datos y Seguridad](#ii-modelado-de-datos-y-seguridad)
  - [III. Patrones de Diseño y Composición](#iii-patrones-de-diseño-y-composición)
  - [IV. Buenas Prácticas y Arquitectura](#iv-buenas-prácticas-y-arquitectura)


### I. Diseño de Software y Principios

1.  La **Programación Orientada a Objetos (POO)** se define como una forma de escribir programas inspirada en la observación del mundo real. Resuma la **importancia de los conceptos** fundamentales (Abstracción, Ocultación, Encapsulamiento, Herencia y Polimorfismo) en el **diseño de software** complejo.

2.  Explique la práctica de la **Modularidad** en el desarrollo de software. ¿Cómo contribuye la filosofía de mantener las clases **pequeñas y con responsabilidad única** a la hora de crear programas que son más fáciles de testear, mantener y ampliar?.

### II. Modelado de Datos y Seguridad

3.  El desarrollo moderno de software promueve la **Inmutabilidad** para aumentar la seguridad y la robustez del código. Describa tres mecanismos específicos en C# (además de los campos `const`) que se utilizan para garantizar que un objeto (o sus propiedades) no se puedan modificar después de su creación o inicialización.

4.  Compare los tipos **`class`**, **`struct`**, y **`record class`** en C#. ¿En qué escenarios de diseño (I+D) se recomienda utilizar un **`record class`** en lugar de una `class` tradicional, y qué característica clave (relacionada con la igualdad) proporciona el `record` automáticamente?.


### III. Patrones de Diseño y Composición

5.  El patrón **Builder** se utiliza para la construcción de objetos complejos. Argumente por qué, en el contexto de I+D, se prefiere el patrón Builder frente a un constructor simple que maneja muchos parámetros opcionales o constructores sobrecargados, detallando el problema que el Builder resuelve.

6.  La **Composición de Objetos** es un principio de diseño fundamental. Describa qué tipo de relación modela la composición (a diferencia de la herencia), y mencione dos ventajas de la composición sobre la herencia que la hacen una técnica de diseño más flexible, con menor acoplamiento y más fácil de testear.


7.  Explique el funcionamiento del patrón **Lazy (Perezoso)**. ¿Cuál es el beneficio de rendimiento o de recursos que se obtiene al retrasar la creación de una instancia hasta que se accede a ella por primera vez, especialmente cuando la instanciación es costosa o el objeto podría no usarse nunca?.


### IV. Buenas Prácticas y Arquitectura

8.  En el diseño de sistemas robustos, la **Validación temprana (fail fast)** es una buena práctica crítica. Si un constructor o método recibe un argumento obligatorio que es nulo, ¿qué tipo de excepción debe lanzarse para asegurar que el objeto no se crea en un estado inconsistente, y cómo se relaciona esto con la integridad del objeto?.


9.  Las **Propiedades con Backing Field** (campos de respaldo) permiten un control estricto sobre el estado de un objeto. Describa tres usos comunes (o escenarios de I+D) para implementar lógica dentro del accesor `set` de una propiedad que requiera un *backing field* explícito.

10. Diferencie la función y el alcance entre una **Solución (.sln)** y un **Proyecto (.csproj)** en un entorno de desarrollo C#. Además, explique cómo el modificador de visibilidad **`internal`** se utiliza para definir límites de accesibilidad dentro de esta estructura de proyectos.
