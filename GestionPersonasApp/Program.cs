using GestionPersonasApp;

List<Persona> listaPersonas = new List<Persona>();
bool salir = false;

while (!salir)
{
    Console.WriteLine("\n--- SISTEMA DE GESTIÓN DE PERSONAS ---");
    Console.WriteLine("1. Agregar Persona");
    Console.WriteLine("2. Listar Personas");
    Console.WriteLine("3. Salir");
    Console.Write("Seleccione una opción: ");

    string? opcion = Console.ReadLine();

    switch (opcion)
    {
        case "1":
            Persona p = new Persona();
            Console.Write("Nombre: ");
            p.Nombre = Console.ReadLine() ?? "";
            Console.Write("Edad: ");
            int.TryParse(Console.ReadLine(), out int edad);
            p.Edad = edad;
            Console.Write("RUT: ");
            p.Rut = Console.ReadLine() ?? "";
            listaPersonas.Add(p);
            Console.WriteLine("¡Persona agregada!");
            break;
        case "2":
            Console.WriteLine("\nLista de Personas:");
            foreach (var per in listaPersonas)
            {
                per.MostrarDatos();
            }
            break;
        case "3":
            salir = true;
            break;
        default:
            Console.WriteLine("Opción no válida.");
            break;
    }
}