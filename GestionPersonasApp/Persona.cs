namespace GestionPersonasApp;

public class Persona
{
    public string Nombre { get; set; } = string.Empty;
    public int Edad { get; set; }
    public string Rut { get; set; } = string.Empty;

    public void MostrarDatos()
    {
        Console.WriteLine($"Nombre: {Nombre}, Edad: {Edad}, RUT: {Rut}");
    }
}