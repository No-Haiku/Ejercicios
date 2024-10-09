using System;

public class Program
{
    static void Main(string[] args)
    {
        string input = string.Empty;
        int opcion;

        do
        {
            Console.WriteLine("\n----- Menú -----");
            Console.WriteLine("1. Invertir palabra");
            Console.WriteLine("2. Verificar si es capicúa");
            Console.WriteLine("3. Contar vocales");
            Console.WriteLine("4. Salir");
            Console.Write("Seleccione una opción: ");

            // Validar que la entrada sea un número
            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                Console.WriteLine("Por favor, ingrese un número válido.");
                continue;
            }

            if (opcion != 4) // Salida en opción 4
            {
                Console.WriteLine("Ingresa una palabra:");
                input = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Por favor, ingrese una palabra válida.");
                    continue;
                }
            }

            switch (opcion)
            {
                case 1:
                    // Inversión de la palabra
                    string palabraInvertida = InvertirPalabra(input);
                    Console.WriteLine($"Palabra invertida: {palabraInvertida}");
                    break;

                case 2:
                    // Verificar si es capicúa
                    if (EsCapicua(input))
                    {
                        Console.WriteLine("La palabra es capicúa.");
                    }
                    else
                    {
                        Console.WriteLine("La palabra no es capicúa.");
                    }
                    break;

                case 3:
                    // Contar vocales
                    int numeroVocales = ContarVocales(input);
                    Console.WriteLine($"Número de vocales: {numeroVocales}");
                    break;

                case 4:
                    Console.WriteLine("Saliendo...");
                    break;

                default:
                    Console.WriteLine("Opción no válida. Por favor seleccione una opción del 1 al 4.");
                    break;
            }

        } while (opcion != 4);
    }

    public static string InvertirPalabra(string palabra)
    {
        char[] arr = palabra.ToCharArray();
        Array.Reverse(arr);
        return new string(arr);
    }

    public static bool EsCapicua(string palabra)
    {
        string palabraInvertida = InvertirPalabra(palabra);
        return string.Equals(palabra, palabraInvertida, StringComparison.OrdinalIgnoreCase);
    }

    public static int ContarVocales(string palabra)
    {
        int contador = 0;
        foreach (char c in palabra.ToLower())
        {
            if ("aeiouáéíóú".Contains(c)) // Contar todas las vocales, incluidas acentuadas
            {
                contador++;
            }
        }
        return contador;
    }
}
