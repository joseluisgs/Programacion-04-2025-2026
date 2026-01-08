namespace Lazy;

// Un objeto que toma mucho tiempo en construirse
public class ReporteMensual { 
    public ReporteMensual() { 
        Thread.Sleep(5000); // Simula una carga de 5 segundos
        Console.WriteLine("Reporte mensual creado."); 
    }
    public void MostrarResumen()
    {
        Console.WriteLine("Mostrando resumen mensual detallado...");
    }
}

public class ReporteSemanal { 
    public ReporteSemanal() { 
        Console.WriteLine("Reporte semanal creado."); 
    }
    public void MostrarResumen()
    {
        Console.WriteLine("Mostrando resumen semanal detallado...");
    }
}

public class DashboardLazy
{
    // El objeto 'ReporteMensual' NO se crea cuando se crea el 'Dashboard'
    private readonly Lazy<ReporteMensual> _reporteMensualLazy = new(() => new ReporteMensual());
    // El objeto 'ReporteSemanal' NO se crea cuando se crea el 'Dashboard'
    private readonly ReporteSemanal _reporteSemanal = new ReporteSemanal();

    // La propiedad expone el valor y activa la creación al primer acceso
    public ReporteMensual DatosReporteMensual => _reporteMensualLazy.Value; 
    public ReporteSemanal DatosReporteSemanal => _reporteSemanal;
    
    public void MostrarResumen()
    {
        _reporteSemanal.MostrarResumen(); // Este método se puede usar sin necesidad de instanciar el Dashboard
        _reporteMensualLazy.Value.MostrarResumen(); // Este método también se puede usar sin necesidad de instanciar el Dashboard
    }
}

public class Dashboard
{
    // El objeto 'ReporteMensual' NO se crea cuando se crea el 'Dashboard'
    private readonly ReporteMensual _reporteMensual = new ReporteMensual();
    // El objeto 'ReporteSemanal' NO se crea cuando se crea el 'Dashboard'
    private readonly ReporteSemanal _reporteSemanal = new ReporteSemanal();

    // La propiedad expone el valor y activa la creación al primer acceso
    public ReporteMensual DatosReporteMensual => _reporteMensual;
    public ReporteSemanal DatosReporteSemanal => _reporteSemanal;
    
    public void MostrarResumen()
    {
        _reporteSemanal.MostrarResumen(); // Este método se puede usar sin necesidad de instanciar el Dashboard
        _reporteMensual.MostrarResumen(); // Este método también se puede usar sin necesidad de instanciar el Dashboard
    }
}