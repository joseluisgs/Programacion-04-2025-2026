## Cuestionario: Programación Orientada a Objetos (POO)

- [Cuestionario: Programación Orientada a Objetos (POO)](#cuestionario-programación-orientada-a-objetos-poo)
  - [I. Conceptos y Principios Fundamentales](#i-conceptos-y-principios-fundamentales)
  - [II. Objetos y Clases](#ii-objetos-y-clases)
  - [III. Clases, Visibilidad y Modificadores](#iii-clases-visibilidad-y-modificadores)
  - [IV. Constructores y Referencia `this`](#iv-constructores-y-referencia-this)
  - [V. Igualdad, Representación y Propiedades](#v-igualdad-representación-y-propiedades)
  - [VI. Miembros Estáticos, Structs, Records y Patrones](#vi-miembros-estáticos-structs-records-y-patrones)


### I. Conceptos y Principios Fundamentales

1.  La **Programación Orientada a Objetos (POO)** es una forma de escribir programas que se inspira en:
    a) El uso exclusivo de funciones matemáticas.
    b) La observación y comprensión del mundo real, donde todo está formado por "objetos".
    c) La estructura secuencial de los lenguajes de bajo nivel.
    d) La arquitectura del hardware de la computadora.

2.  ¿Cuál de las siguientes es una característica que define a un objeto, según los principios de la POO?
    a) Ensamblado.
    b) Solución.
    c) Comportamiento.
    d) Tipo simple.

3.  El concepto de **Abstracción** se refiere a la acción de:
    a) Juntar el estado y el comportamiento en un solo contenedor.
    b) Fijarse solo en las características importantes de un objeto, dejando de lado detalles irrelevantes.
    c) Permitir que diferentes objetos respondan a la misma orden.
    d) Crear copias de objetos modificadas (uso de `with`).

4.  El principio de la POO que implica que un objeto guarda sus datos internos y solo permite acceder a lo que deja su "interfaz" pública es la:
    a) Herencia.
    b) Modularidad.
    c) Ocultación.
    d) Polimorfismo.

5.  ¿Qué concepto asegura que si un objeto cambia algo dentro de sí mismo, el resto del programa no se estropea ni se ve afectado, al juntar estado y comportamiento?
    a) Abstracción.
    b) Recolector de basura.
    c) Encapsulamiento.
    d) Identidad.

6.  La **Herencia** permite:
    a) Dividir un programa en partes independientes.
    b) Controlar cómo se accede a los atributos privados.
    c) Crear objetos que "heredan" características y comportamientos de otros más generales.
    d) Asegurar que solo haya una instancia de una clase.

7.  El **Polimorfismo** se caracteriza porque diferentes objetos pueden:
    a) Ocupar siempre la misma referencia en memoria.
    b) Responder a la misma orden de maneras distintas.
    c) Ser inicializados únicamente con el constructor por defecto.
    d) Tener únicamente propiedades `init-only`.

8.  La **Modularidad** es la práctica de:
    a) Asegurar que el objeto sea inmutable.
    b) Dividir un programa en partes independientes y reutilizables llamadas módulos.
    c) Implementar el patrón Singleton.
    d) Usar la referencia `this` en constructores.

9.  En lenguajes como C#, el **Recolector de Basura (Garbage Collector)** tiene la función de:
    a) Evitar la sobrecarga de constructores.
    b) Gestionar la memoria liberando la ocupada por objetos que ya no se usan (no referenciados).
    c) Compilar el código fuente en ensamblados.
    d) Aplicar el patrón Fachada.

### II. Objetos y Clases

10. La **Identidad** de un objeto en programación se corresponde con:
    a) El conjunto de métodos que puede ejecutar.
    b) La referencia que el ordenador guarda en memoria.
    c) El valor devuelto por el método `ToString()`.
    d) Los datos que almacena (su estado).

11. El **Estado** de un objeto puede cambiar a lo largo del tiempo si:
    a) Se utiliza un `namespace` diferente.
    b) El objeto tiene comportamientos (métodos) que lo permiten.
    c) Se sobrescribe el `GetHashCode()`.
    d) Se declara como `sealed`.

12. ¿Cuál es la herramienta que te permite definir cómo deben ser tus objetos (información que guardan y acciones que realizan)?
    a) La Interfaz pública.
    b) El Recolector de basura.
    c) La Clase.
    d) El Alias.

13. Cuando se usa la palabra clave `new` en C# para crear un objeto, se devuelve:
    a) Una copia por valor del estado.
    b) Una referencia (una "dirección") en la memoria del programa por donde se podrá localizar el objeto.
    c) El valor predeterminado del objeto.
    d) Un constructor estático.

14. Un **namespace** (espacio de nombres) es como una carpeta virtual que:
    a) Solo agrupa variables locales.
    b) Agrupa tipos relacionados (clases, structs, interfaces, etc.) y evita conflictos de nombres.
    c) Se utiliza para dividir una clase en varios archivos (`partial`).
    d) Define la estructura general de una solución completa.

15. La instrucción **using** sirve para:
    a) Definir un nuevo constructor estático.
    b) Importar otros espacios de nombres al archivo para usar sus clases sin escribir el nombre completo.
    c) Declarar una propiedad `get-only`.
    d) Asignar valores a campos `readonly` fuera del constructor.

### III. Clases, Visibilidad y Modificadores

16. La estructura de una clase en C# se define con la palabra clave `class` seguida de su nombre, usando típicamente la nomenclatura:
    a) snake_case.
    b) camelCase.
    c) PascalCase.
    d) kebab-case.

17. En una clase, los **Campos Privados** (`private`) se usan para almacenar datos internos que:
    a) Deben ser accesibles directamente desde fuera de la clase.
    b) Solo son visibles dentro de la clase.
    c) Definen la interfaz pública del objeto.
    d) Se nombran con mayúscula inicial (convención).

18. Un miembro con modificador **public** es:
    a) Accesible solo desde el mismo proyecto o ensamblado.
    b) Accesible solo dentro de la clase.
    c) Accesible desde cualquier sitio.
    d) Útil para operaciones internas.

19. El modificador **internal** hace que una clase o miembro sea accesible:
    a) Solamente dentro de la clase.
    b) Solamente para el Recolector de basura.
    c) Solo desde el mismo proyecto o ensamblado.
    d) Desde cualquier solución.

20. ¿Cuál de los siguientes modificadores se aplica a una clase para evitar que pueda ser heredada (evitar subclases)?
    a) `const`.
    b) `static`.
    c) `partial`.
    d) `sealed`.

21. Los campos declarados con **readonly** solo pueden asignarse:
    a) En cualquier momento de la vida del objeto.
    b) Una vez en la declaración o el constructor.
    c) Por métodos estáticos.
    d) Por propiedades calculadas.

22. Si un objeto de clase se asigna a otra variable en C#, ambas variables apuntan al mismo objeto en memoria. Esto se debe a que las clases son tipos por:
    a) Valor.
    b) Tupla.
    c) Estático.
    d) Referencia.

23. El operador `is` en C# se utiliza para:
    a) Intentar convertir un objeto a un tipo determinado.
    b) Verificar si un objeto es de un tipo específico.
    c) Definir una propiedad calculada.
    d) Inicializar campos `readonly`.

### IV. Constructores y Referencia `this`

24. Los **Object initializers** se usan para:
    a) Garantizar que se cumpla la herencia.
    b) Asignar propiedades al momento de crear el objeto.
    c) Sobrescribir `ReferenceEquals`.
    d) Definir la identidad del objeto.

25. Se debe usar un **Constructor** en lugar de un `object initializer` cuando:
    a) Se necesita la sintaxis corta con target-typed new.
    b) Se quiere flexibilidad en la asignación de propiedades.
    c) Se necesita garantizar algún valor sí o sí al crear el objeto.
    d) La clase es declarada `sealed`.

26. Un **Constructor por defecto** es el que:
    a) Permite pasar información para personalizar el estado.
    b) No tiene parámetros y C# lo crea si no defines ninguno.
    c) Debe ser siempre privado.
    d) Es declarado `static`.

27. ¿Qué se requiere para la **Sobrecarga de Constructores**?
    a) Que todos los constructores tengan el mismo número y tipo de parámetros.
    b) Que cada constructor tenga una firma diferente (número/tipo de parámetros).
    c) Que el constructor utilice `params`.
    d) Que se use un `static readonly` para inicializarlos.

28. La palabra clave **this** en C# se utiliza para:
    a) Referenciar la clase base para la herencia.
    b) Acceder a los métodos estáticos.
    c) Referirse al objeto actual, es decir, la instancia sobre la que se está ejecutando el código.
    d) Definir nuevos espacios de nombres.

29. El uso de **`this(...)`** se emplea en constructores para:
    a) Inicializar campos `const`.
    b) Llamar a otro constructor de la misma clase, reutilizando lógica.
    c) Comparar dos objetos por referencia.
    d) Definir una tupla.

30. Los campos **readonly** en C# son un mecanismo que ayuda a crear objetos:
    a) Estáticos.
    b) Anidados.
    c) Inmutables.
    d) De tipo valor (`structs`).

31. Una buena práctica al diseñar constructores es:
    a) Incluir lógica pesada dentro de ellos.
    b) Lanzar excepciones si los argumentos son inválidos.
    c) Delegar todo el proceso de inicialización a un `object initializer`.
    d) Usar siempre el patrón Lazy.

### V. Igualdad, Representación y Propiedades

32. Si se compara la **Identidad** de dos objetos, se verifica si:
    a) Tienen los mismos datos internos (estado).
    b) Apuntan al mismo objeto en memoria.
    c) Son compatibles con el método `ToString()`.
    d) Su clase base es la misma.

33. ¿Qué método debe sobrescribirse junto con `Equals(object)` para comparar el **estado** (contenido) en lugar de la referencia?
    a) `Override()`.
    b) `ReferenceEquals()`.
    c) `GetHashCode()`.
    d) `SetX()`.

34. El método que se sobrescribe para que un objeto devuelva una representación legible como cadena es:
    a) `ToValue()`.
    b) `ToType()`.
    c) `ToString()`.
    d) `ToInternal()`.

35. Los métodos `GetX()` y `SetX()` se conocen como **getters** y **setters**, y su propósito es:
    a) Acceder y modificar los atributos privados de forma controlada.
    b) Sustituir completamente el uso de propiedades.
    c) Definir campos estáticos inmutables.
    d) Implementar el patrón Fluent.

36. En C#, las **Propiedades** (`get`/`set`) son preferibles a los campos públicos porque:
    a) Permiten almacenar únicamente datos inmutables.
    b) Combinan la sencillez de los campos con la seguridad y control de los métodos.
    c) Solo funcionan con tipos por referencia.
    d) Son gestionadas directamente por el Recolector de basura.

37. Las **Propiedades Auto-implementadas** son aquellas que:
    a) Deben tener un `backing field` explícito.
    b) El compilador gestiona automáticamente el campo de respaldo.
    c) Solo se utilizan en `structs`.
    d) Son necesariamente `get-only`.

38. Las propiedades `get-only` (solo lectura) son aquellas que:
    a) Se pueden modificar después de crear el objeto.
    b) No tienen el accesor `set`, o lo tienen privado/interno.
    c) Requieren un constructor estático.
    d) Deben ser inicializadas usando el patrón Builder.

39. Si una propiedad se declara como **`get; init;`** (init-only), significa que solo puede asignarse:
    a) En la declaración, nunca más tarde.
    b) Durante la inicialización (constructor o `new { ... }`).
    c) Mediante un método público `SetX()`.
    d) Al acceder a ella por primera vez (Lazy).

40. Las **Propiedades Calculadas (computed properties)** son aquellas que:
    a) Almacenan directamente el valor en un campo.
    b) No almacenan un dato propio, sino que devuelven un valor basado en otros atributos o lógica.
    c) Solo se pueden usar con el operador `is`.
    d) Requieren el modificador `sealed`.

41. ¿Cuál es un uso común de los **Backing Fields** (campos de respaldo) en una propiedad completa?
    a) Evitar el encadenamiento de constructores.
    b) Implementar validación en el `set`.
    c) Definir una tupla.
    d) Declarar un `namespace`.

### VI. Miembros Estáticos, Structs, Records y Patrones

42. Los **Miembros Estáticos** (métodos o propiedades `static`) pertenecen a:
    a) La instancia concreta del objeto.
    b) La clase, y son compartidos por todas las instancias.
    c) El proyecto completo (`.csproj`).
    d) Un miembro privado de la clase.

43. Una **Clase Estática** (`static class`) tiene la restricción de que:
    a) Puede tener miembros de instancia pero no constructor estático.
    b) Solo puede tener miembros estáticos y no puede instanciarse.
    c) Puede ser heredada pero no componer otros objetos.
    d) Debe implementar al menos una interfaz.

44. Los **Enums** son tipos por **valor** que se utilizan para representar opciones limitadas y conocidas y en el fondo son:
    a) Cadenas de texto.
    b) Objetos con identidad única.
    c) Números enteros bajo el capó con nombres legibles.
    d) Propiedades `get-only`.

45. La característica principal de un **Struct** es que es un tipo por:
    a) Referencia (heap).
    b) Referencia, con igualdad por valor.
    c) Valor (stack), donde copiar el struct crea una copia independiente.
    d) Record (con constructor posicional).

46. Un **Record Class** es un tipo por referencia, pero se diferencia de una `class` estándar en que automáticamente soporta:
    a) Inicialización perezosa (Lazy).
    b) Herencia de múltiples clases.
    c) Igualdad por valor.
    d) Sobrecarga de operadores estáticos.

47. La **Composición de Objetos** (un objeto contiene otros como campos) se utiliza para representar una relación de:
    a) "Es parte de" o "tiene un".
    b) Herencia ("es un").
    c) Implementación de interfaz.
    d) Referencia a la pila (stack).

48. ¿Qué patrón de diseño garantiza que solo haya una instancia de una clase en todo el programa?
    a) Builder.
    b) Fachada.
    c) Lazy.
    d) Singleton.

49. El patrón **Builder** se utiliza para:
    a) Retrasar la creación de un objeto hasta que se necesite.
    b) Agrupar múltiples operaciones complejas en una interfaz simple.
    c) Construir objetos complejos paso a paso.
    d) Lanzar excepciones de manera controlada.

50. Una **Buena Práctica** en la programación orientada a objetos (POO) es mantener las clases:
    a) Grandes, para minimizar el número total de archivos.
    b) Pequeñas y con responsabilidad única.
    c) Con campos públicos para facilitar el acceso.
    d) Mutables por defecto.