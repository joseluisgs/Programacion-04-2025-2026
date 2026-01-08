using BaseDatosAlumnado.Config;
using BaseDatosAlumnado.Enums;
using BaseDatosAlumnado.Models;
using BaseDatosAlumnado.Repositories;
using BaseDatosAlumnado.Validators;
using Serilog;

namespace BaseDatosAlumnado.Services;

/// <summary>
///     Servicio para administrar los alumnos.
/// </summary>
/// <param name="repository">Repositorio del alumnado</param>
/// <param name="validador">Validador de alumno/a</param>
public class AlumnosService(AlumnosRepository repository, AlumnoValidator validador) {
    private readonly ILogger _log = Log.ForContext<AlumnosService>();
    public int TotalAlumnos => repository.TotalAlumnos;

    /// <summary>
    ///     Obtiene todos los alumnos con el ordenamiento especificado.
    /// </summary>
    /// <param name="ordenamiento">Ordenamiento a usar </param>
    /// <returns>Vector de alumnos</returns>
    public Alumno[] GetAllAlumnos(TipoOrdenamiento ordenamiento) {
        _log.Information("Obteniendo todos los alumnos con ordenamiento: {Ordenamiento}", ordenamiento);
        var alumnos = repository.GetAll();
        OrdenarVectorAlumnos(alumnos, ordenamiento);
        return alumnos;
    }

    /// <summary>
    ///     Ordena el vector de alumnos según el tipo de ordenamiento especificado.
    /// </summary>
    /// <param name="alumnosArr">Vector de alumnos a ordenar</param>
    /// <param name="ordenamiento">Tipo de ordenamiento (DNI ascendente o Nota descendente)</param>
    /// <remarks>Usa el algoritmo de burbuja para ordenar.</remarks>
    private void OrdenarVectorAlumnos(Alumno[] alumnosArr, TipoOrdenamiento ordenamiento = TipoOrdenamiento.Dni) {
        var n = alumnosArr.Length;
        if (n <= 1) return;

        // ⬅️ Nuevo Log de Debug para el inicio del ordenamiento
        _log.Debug("Iniciando OrdenarVectorAlumnos ({Tipo}): {N} elementos", ordenamiento, n);

        for (var i = 0; i < n - 1; i++) {
            for (var j = 0; j < n - i - 1; j++) {
                var debeIntercambiar = false;
                var alumnoJ = alumnosArr[j];
                var alumnoJ1 = alumnosArr[j + 1];

                if (ordenamiento == TipoOrdenamiento.Dni) {
                    // Ordenar por DNI (Ascendente). CompareTo > 0 significa que alumnoJ es lexicográficamente mayor.
                    // Se usa así porque noe s un número sino una cadena: string
                    if (string.Compare(alumnoJ.Dni, alumnoJ1.Dni, StringComparison.Ordinal) > 0)
                        debeIntercambiar = true;
                }
                else if (ordenamiento == TipoOrdenamiento.Nota) {
                    // Ordenar por Nota (Descendente). Si la nota actual es MENOR que la siguiente, debe intercambiarse.
                    if (alumnoJ.Nota < alumnoJ1.Nota) debeIntercambiar = true;
                }

                if (debeIntercambiar)
                    // Con tuplas de C# 7 para intercambiar
                    (alumnosArr[j], alumnosArr[j + 1]) = (alumnosArr[j + 1], alumnosArr[j]);
            }
        }

        // Opcional: Log de Debug para el fin del ordenamiento
        _log.Debug("Ordenamiento finalizado.");
    }

    /// <summary>
    ///     Genera un informe estadístico del alumnado.
    /// </summary>
    /// <returns>Informe con estadísticas del alumnado</returns>
    public Informe GenerarInforme() {
        _log.Information("Generando informe");
        var totalNotas = 0.0;
        var aprobados = 0;
        var suspensos = 0;

        var alumnos = repository.GetAll();

        // Se recorre el vector principal directamente
        foreach (var al in alumnos)
            if (al is { } alumno) {
                totalNotas += alumno.Nota;
                if (alumno.Nota >= Configuracion.NotaAprobado) // Criterio de aprobado (constante 5.00m)
                    aprobados = aprobados + 1;
                else
                    suspensos = suspensos + 1;
            }

        // División con decimal para mantener la precisión
        var notaMedia = totalNotas / TotalAlumnos;

        return new Informe(TotalAlumnos, aprobados, aprobados * 100.0 / TotalAlumnos,
            suspensos, suspensos * 100.0 / TotalAlumnos, notaMedia);
    }

    /// <summary>
    ///     Obtiene un alumno por su ID.
    /// </summary>
    /// <param name="id">ID del alumno</param>
    /// <returns>Alumno encontrado</returns>
    /// <exception cref="KeyNotFoundException">Si no se encuentra el alumno</exception>
    public Alumno GetAlumnoById(int id) {
        _log.Information("Obteniendo alumno por ID: {Id}", id);
        var alumno = repository.GetById(id);
        if (alumno == null) {
            _log.Warning("Alumno con ID {Id} no encontrado.", id);
            throw new KeyNotFoundException($"Alumno con ID {id} no encontrado.");
        }

        return alumno;
    }

    /// <summary>
    ///     Obtiene un alumno por su DNI.
    /// </summary>
    /// <param name="dni">DNI del alumno</param>
    /// <returns>Alumno encontrado</returns>
    /// <exception cref="KeyNotFoundException">Si no se encuentra el alumno</exception>
    public Alumno GetAlumnoByDni(string dni) {
        _log.Information("Obteniendo alumno por DNI: {Dni}", dni);
        var alumnos = repository.GetAll();
        foreach (var al in alumnos)
            if (al.Dni.Equals(dni, StringComparison.OrdinalIgnoreCase))
                return al;
        throw new KeyNotFoundException($"Alumno con DNI {dni} no encontrado.");
    }

    /// <summary>
    ///     Elimina un alumno por su ID.
    /// </summary>
    /// <param name="id">ID del alumno</param>
    /// <returns>Alumno eliminado</returns>
    /// <exception cref="KeyNotFoundException">Si no se encuentra el alumno</exception>
    public Alumno? DeleteAlumno(int id) {
        _log.Information("Eliminando alumno con ID: {Id}", id);
        return repository.Delete(id) ??
               throw new KeyNotFoundException($"Alumno con ID {id} no encontrado para eliminación.");
    }

    /// <summary>
    ///     Salva un nuevo alumno.
    /// </summary>
    /// <param name="alumno">Alumno a guardar</param>
    /// <returns>El alumno guardado con la id generada</returns>
    /// <exception cref="InvalidOperationException">Si el DNI ya existe en la base de datos</exception>
    public Alumno SaveAlumno(Alumno alumno) {
        _log.Information("Guardando nuevo alumno: {Alumno}", alumno);
        // Validar el alumno antes de guardarlo
        var alumnoValidado = validador.Validate(alumno);

        // Y si existe ese DNI ya en la base de datos, no dejar guardarlo, es campo único
        try {
            GetAlumnoByDni(alumnoValidado.Dni);
            _log.Warning("No se puede guardar. Ya existe un alumno con DNI: {Dni}", alumnoValidado.Dni);
            throw new InvalidOperationException($"Ya existe un alumno con DNI {alumnoValidado.Dni}.");
        }
        catch (KeyNotFoundException) {
            // No existe, seguimos adelante :) La vida es feliz, cual perdiz
        }

        // Guardar en el repositorio
        return repository.Save(alumnoValidado);
    }

    /// <summary>
    ///     Actualiza un alumno existente.
    /// </summary>
    /// <param name="alumno">Alumno con los datos actualizados</param>
    /// <returns>Alumno actualizado</returns>
    /// <exception cref="KeyNotFoundException">Si no se encuentra el alumno para actualizar</exception>
    public Alumno UpdateAlumno(Alumno alumno) {
        _log.Information("Actualizando alumno: {Alumno}", alumno);
        // Validar el alumno antes de actualizarlo
        var alumnoValidado = validador.Validate(alumno);

        // Buscar el alumno original en la base de datos
        return repository.Update(alumnoValidado) ?? throw new KeyNotFoundException(
            $"Alumno con ID {alumnoValidado.Id} no encontrado para actualización.");
    }
}