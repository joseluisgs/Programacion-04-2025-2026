using BaseDatosAlumnado.Config;
using BaseDatosAlumnado.Factories;
using BaseDatosAlumnado.Models;
using Serilog;

namespace BaseDatosAlumnado.Repositories;

/// <summary>
///     Repositorio de alumnos. Implementa el patrón Singleton.
/// </summary>
public class AlumnosRepository {
    private static AlumnosRepository? _instance;

    // Contador estático para generar IDs únicos.
    private static int _idCounter;
    private readonly ILogger _log = Log.ForContext<AlumnosRepository>();
    private Alumno?[] _listado = new Alumno?[Configuracion.TamanoInicial];


    /// <summary>
    ///     Crea una instancia única de la clase. Si ya existe, devuelve la misma instancia.
    /// </summary>
    private AlumnosRepository() {
        _log.Debug("Creando instancia de AlumnosRepository");
        // inicializamos alumnos de prueba
        InitDemoAlumnos();
    }

    public int TotalAlumnos { get; private set; }

    /// <summary>
    ///     Conseguir el siguiente ID único para un nuevo alumno.
    /// </summary>
    /// <returns>Nuevo ID único.</returns>
    private static int GetNextId() {
        return ++_idCounter;
    }

    /// <summary>
    ///     Consigue la instancia única del repositorio.
    /// </summary>
    /// <returns>Instancia única de AlumnosRepository.</returns>
    public static AlumnosRepository GetInstance() {
        return _instance ??= new AlumnosRepository();
    }

    /// <summary>
    ///     Inicializa el repositorio con datos de alumnos de prueba.
    /// </summary>
    private void InitDemoAlumnos() {
        _log.Debug("Inicializando alumnos de prueba");
        var demoAlumnos = AlumnoFactory.DemoData();
        // Añadimos los alumnos de prueba al repositorio
        foreach (var alumno in demoAlumnos)
            Save(alumno);
    }

    /// <summary>
    ///     Redimensiona el vector de alumnos si es necesario.
    /// </summary>
    private void RedimensionarSiNecesario() {
        _log.Debug("Comprobando si es necesario hacer crecer el vector");
        // Usamos la división entera.
        var porcentajeUso = TotalAlumnos * 100 / _listado.Length;
        var tamanoActual = _listado.Length;
        var nuevoTamano = tamanoActual + Configuracion.IncrementoTamano;

        // Condición de Expansión
        if (porcentajeUso >= Configuracion.IncrementoTamano) {
            _log.Warning("EXPANSION NECESARIA. Uso: {Uso}%. Expandiendo de {Actual} a {Nuevo}",
                porcentajeUso, tamanoActual, nuevoTamano);

            // Redimensionar el array existente, manteniendo los datos actuales
            Array.Resize(ref _listado, nuevoTamano);
            _log.Information("Expansión exitosa. Nuevo tamaño: {Nuevo}", nuevoTamano);
        }
    }

    private void ReducirSiNecesario() {
        _log.Debug("Comprobando si es necesario reducir el vector");
        var porcentajeUso = TotalAlumnos * 100 / _listado.Length;

        // Condición de Reducción
        if (_listado.Length > Configuracion.TamanoInicial && porcentajeUso < Configuracion.PorcentajeReduccion) {
            var tamanoActual = _listado.Length;
            var nuevoTamano = tamanoActual - Configuracion.IncrementoTamano;

            if (nuevoTamano < Configuracion.TamanoInicial) nuevoTamano = Configuracion.TamanoInicial;

            _log.Warning("REDUCCION NECESARIA. Uso: {Uso}%. Reduciendo de {Actual} a {Nuevo}",
                porcentajeUso, tamanoActual, nuevoTamano);

            // El problema es la compactación: Array.Resize NO compacta (no elimina los nulls).
            // Por lo tanto, ¡ESTE MÉTODO AÚN NECESITA COMPACTAR!
            // La mejor forma es compactar primero y luego redimensionar.

            // Paso 1: Compactar (Crear un array solo con elementos no nulos)
            var vectorCompacto = ObtenerVectorCompacto();

            // Paso 2: Crear el nuevo array final del tamaño deseado.
            _listado = new Alumno?[nuevoTamano];

            // Paso 3: Copiar los elementos compactados al nuevo array
            Array.Copy(vectorCompacto, _listado, vectorCompacto.Length);

            // Log Information para el éxito de la reducción
            _log.Information("Reducción exitosa. Nuevo tamaño: {Nuevo}", nuevoTamano);
        }
    }
    

    /// <summary>
    ///     Obtiene todos los alumnos en un array compacto (sin nulls).
    /// </summary>
    /// <returns>Array con todos los alumnos en un array compacto.</returns>
    public Alumno[] GetAll() {
        _log.Information("Obteniendo todos los alumnos. Total: {Total}", TotalAlumnos);
        return ObtenerVectorCompacto();
    }

    /// <summary>
    ///     Crea un vector compacto con solo los alumnos válidos (no nulos).
    /// </summary>
    /// <returns>Vector compacto de alumnos.</returns>
    private Alumno[] ObtenerVectorCompacto() {
        _log.Debug("Iniciando ObtenerVectorCompacto: Preparando {Total} elementos válidos de un array de {Length}",
            TotalAlumnos,
            _listado.Length);

        if (TotalAlumnos == 0) return [];

        // 1. Crear vector auxiliar con el tamaño exacto
        var vectorCompacto = new Alumno[TotalAlumnos];

        // 2. Copiar solo los elementos no nulos (sin LINQ, el bucle es la mejor opción)
        var auxIndex = 0;
        foreach (var alumno in _listado)
            if (alumno is not null) { // Usamos 'is not null' por legibilidad, ambien valdría '!= null' o is {} al
                vectorCompacto[auxIndex] = alumno;
                auxIndex++;
            }

        _log.Debug("Vector compacto creado con {Copiados} elementos.", auxIndex);

        return vectorCompacto;
    }

    /// <summary>
    ///     Obtiene un alumno por su ID.
    /// </summary>
    /// <param name="id">Identificador del alumno/a</param>
    /// <returns>Alumno si se encuentra, null en caso contrario.</returns>
    public Alumno? GetById(int id) {
        _log.Information("Buscando alumno por ID: {Id}", id);
        foreach (var alumno in _listado)
            if (alumno?.Id == id)
                return alumno;

        return null;
    }

    /// <summary>
    ///     Elimina un alumno por su ID.
    /// </summary>
    /// <param name="id">Identificador del alumno/a</param>
    /// <returns>Alumno eliminado si se encuentra, null en caso contrario.</returns>
    public Alumno? Delete(int id) {
        _log.Information("Eliminando alumno por ID: {Id}", id);
        for (var i = 0; i < _listado.Length; i++)
            if (_listado[i]?.Id == id) {
                _listado[i] = null; // Marcamos como eliminado
                TotalAlumnos--;
                ReducirSiNecesario();
                _log.Information("Alumno con ID {Id} eliminado exitosamente.", id);
                return _listado[i];
            }

        _log.Warning("Alumno con ID {Id} no encontrado para eliminación.", id);
        return null;
    }

    /// <summary>
    ///     Salva un nuevo alumno en el repositorio.
    /// </summary>
    /// <param name="alumno">Alumno a guardar</param>
    /// <returns>El alumno con su ID actualizado.</returns>
    public Alumno Save(Alumno alumno) {
        var nuevoConId = alumno with { Id = GetNextId() };
        _log.Information("Guardando alumno: {Alumno}", nuevoConId);
        RedimensionarSiNecesario();
        // Busco la primera posición libre (null)
        for (var i = 0; i < _listado.Length; i++)
            if (_listado[i] == null) {
                _listado[i] = nuevoConId;
                _log.Debug("Alumno guardado en la posición {Index}.", i);
                break;
            }

        TotalAlumnos++;
        _log.Debug("Alumno guardado exitosamente. Total alumnos ahora: {Total}", TotalAlumnos);
        return nuevoConId;
    }

    /// <summary>
    ///     Actualiza un alumno existente en el repositorio.
    /// </summary>
    /// <param name="alumno">Alumno a actualizar</param>
    /// <returns>El alumno con la actualización aplicada, o null si no se encontró.</returns>
    public Alumno? Update(Alumno alumno) {
        _log.Information("Actualizando alumno: {Alumno}", alumno);
        for (var i = 0; i < _listado.Length; i++)
            if (_listado[i]?.Id == alumno.Id) {
                _listado[i] = alumno;
                _log.Information("Alumno con ID {Id} actualizado exitosamente.", alumno.Id);
                return alumno;
            }

        _log.Warning("Alumno con ID {Id} no encontrado para actualización.", alumno.Id);
        return null;
    }
}