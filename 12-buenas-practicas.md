- [12. Buenas prácticas en el Desarrollo de Software](#12-buenas-prácticas-en-el-desarrollo-de-software)
  - [12.1 Nomenclatura y estilo](#121-nomenclatura-y-estilo)
  - [12.2 Mantener clases pequeñas y con responsabilidad única (SRP)](#122-mantener-clases-pequeñas-y-con-responsabilidad-única-srp)
  - [12.3 Métodos cortos y enfocados](#123-métodos-cortos-y-enfocados)
    - [12.3.1 Principio DRY (Don't Repeat Yourself)](#1231-principio-dry-dont-repeat-yourself)
  - [12.4 Preferir propiedades sobre campos públicos](#124-preferir-propiedades-sobre-campos-públicos)
  - [12.5 Inmutabilidad cuando proceda](#125-inmutabilidad-cuando-proceda)
  - [12.6 Validación temprana (Fail-Fast)](#126-validación-temprana-fail-fast)
  - [12.7 Documentación con XML comments](#127-documentación-con-xml-comments)
  - [12.8 Consideraciones de rendimiento](#128-consideraciones-de-rendimiento)
  - [12.9 Gestión de dependencias y separación de capas](#129-gestión-de-dependencias-y-separación-de-capas)


# 12. Buenas prácticas en el Desarrollo de Software

Programar bien es mucho más que hacer que el código funcione; es hacerlo claro, seguro y fácil de mantener. Aplicar buenas prácticas desde el principio separa a los aficionados de los profesionales.

## 12.1 Nomenclatura y estilo

El código se lee muchas más veces de las que se escribe. Usa nombres que expliquen la intención, no el tipo.

- **PascalCase**: Para nombres de clases, métodos y propiedades (`class CuentaBancaria`, `public void RetirarEfectivo`).
- **camelCase**: Para variables locales y parámetros de métodos (`int saldoActual`, `string nombreUsuario`).
- **Guion bajo inicial (_)**: Recomendado para campos privados (`private decimal _saldo`).

## 12.2 Mantener clases pequeñas y con responsabilidad única (SRP)

El principio de **Responsabilidad Única** (Single Responsibility Principle) dicta que una clase debe tener una sola razón para cambiar.
- Si tu clase `Usuario` también envía correos electrónicos y se conecta a la base de datos, tiene demasiadas responsabilidades. 
- Divide y vencerás: crea una clase `ServicioCorreo` y una clase `RepositorioUsuario`.

## 12.3 Métodos cortos y enfocados

Cada método debe hacer una sola tarea. Si un método es muy largo o tiene muchos niveles de anidamiento, es una señal de que debe dividirse.

### 12.3.1 Principio DRY (Don't Repeat Yourself)
Evita repetir código. Si ves que copias y pegas la misma lógica en tres sitios, extrae esa lógica a un método común o a una clase de utilidad. La duplicación es la raíz de muchos bugs (si cambias la lógica en un sitio, debes acordarte de cambiarla en todos).

## 12.4 Preferir propiedades sobre campos públicos

El estado de un objeto debe estar protegido. Nunca expongas variables directamente (`public int Edad`). Usa siempre propiedades, incluso si son automáticas, para poder añadir validaciones en el futuro sin romper el código que ya usa tu clase.

## 12.5 Inmutabilidad cuando proceda

Si un objeto no debe cambiar después de crearse (ej. una transacción terminada o un punto en el espacio), usa propiedades `readonly`, `init-only` y el tipo `record`. Los objetos inmutables son mucho más fáciles de depurar y seguros en entornos multihilo.

## 12.6 Validación temprana (Fail-Fast)

Comprueba los argumentos de los métodos y constructores lo antes posible. No esperes a que el error ocurra en la línea 50; lanza una excepción en la línea 1 si los datos son malos.

```csharp
public void ProcesarPedido(Pedido p) {
    if (p == null) throw new ArgumentNullException(nameof(p));
    // ... resto de la lógica ...
}
```

## 12.7 Documentación con XML comments

Escribe siempre resúmenes que expliquen el "qué" y el "por qué", no solo el "cómo". C# permite generar documentación automática a partir de estos comentarios.

```csharp
/// <summary>
/// Calcula el interés anual basado en el tipo de riesgo del cliente.
/// </summary>
/// <param name="capital">Monto inicial invertido.</param>
/// <param name="riesgo">Nivel de riesgo del 1 al 5.</param>
/// <returns>El monto de interés generado.</returns>
public decimal CalcularInteres(decimal capital, int riesgo) {
    // ...
}
```

## 12.8 Consideraciones de rendimiento

Evita operaciones costosas (como consultas a base de datos o cálculos complejos) dentro de los bloques `get` de las propiedades o dentro de los constructores. Para estos casos, usa métodos explícitos (`CargarDatos()`) o el patrón `Lazy<T>`.

## 12.9 Gestión de dependencias y separación de capas

Organiza tu código en capas lógicas:
1.  **Presentación**: Interacción con el usuario.
2.  **Lógica/Negocio**: Reglas del programa y modelos.
3.  **Datos**: Acceso a BBDD o archivos.

Usa namespaces claros para reflejar esta estructura y mantén las capas separadas para que un cambio en la interfaz no obligue a cambiar la base de datos.
