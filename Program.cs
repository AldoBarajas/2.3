using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace Unidad2Recursividad;

internal static class Program
{
    private static int Main(string[] args)
    {
        try
        {
            if (args.Length > 0)
            {
                switch (args[0].ToLowerInvariant())
                {
                    case "--self-test": Pruebas(); return 0;
                    case "--benchmark": Benchmark(); return 0;
                    case "--demo": Demostracion(); return 0;
                    default:
                        Console.WriteLine("Opciones: --demo, --self-test, --benchmark");
                        return 1;
                }
            }
            MostrarMenu();
            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error: {ex.Message}");
            return 1;
        }
    }

    private static void MostrarMenu()
    {
        while (true)
        {
            Console.WriteLine("\n=== UNIDAD 2: RECURSIVIDAD ===");
            Console.WriteLine("1. Catálogo: comparación de 4 ejercicios");
            Console.WriteLine("2. Torres de Hanoi y lista de movimientos");
            Console.WriteLine("3. Pruebas automáticas");
            Console.WriteLine("4. Benchmark y exportación CSV");
            Console.WriteLine("0. Salir");
            Console.Write("Elige una opción: ");
            string? opcion = Console.ReadLine();
            switch (opcion)
            {
                case "1": Demostracion(); break;
                case "2":
                    Console.Write("Cantidad de discos (1 a 10 para mostrar): ");
                    if (!int.TryParse(Console.ReadLine(), out int n) || n < 1 || n > 10)
                    {
                        Console.WriteLine("Indica un entero entre 1 y 10.");
                        break;
                    }
                    MetricasHanoi m = Hanoi.ResolverRecursivo(n,
                        (disco, origen, destino) =>
                            Console.WriteLine($"Disco {disco}: {origen} -> {destino}"));
                    Console.WriteLine($"Movimientos: {m.Movimientos}; " +
                        $"llamadas: {m.Llamadas}; profundidad máxima: {m.ProfundidadMaxima}");
                    break;
                case "3": Pruebas(); break;
                case "4": Benchmark(); break;
                case "0": return;
                default: Console.WriteLine("Opción no válida."); break;
            }
        }
    }

    private static void Demostracion()
    {
        int[] datos = { 3, 5, 7, 9 };
        Console.WriteLine("\nComparación iterativa / recursiva:");
        Console.WriteLine($"Factorial(5): {Ejercicios.FactorialIterativo(5)} / " +
            $"{Ejercicios.FactorialRecursivo(5)}");
        Console.WriteLine($"Fibonacci(7): {Ejercicios.FibonacciIterativo(7)} / " +
            $"{Ejercicios.FibonacciRecursivo(7)}");
        Console.WriteLine($"Suma [3,5,7,9]: {Ejercicios.SumarIterativo(datos)} / " +
            $"{Ejercicios.SumarRecursivo(datos)}");
        Console.WriteLine($"Buscar 7: índice {Ejercicios.BuscarLinealIterativo(datos, 7)} / " +
            $"{Ejercicios.BuscarLinealRecursivo(datos, 7)}");
        MetricasHanoi m = Hanoi.ResolverRecursivo(3);
        Console.WriteLine($"Hanoi(3): {Hanoi.ResolverIterativo(3)} / " +
            $"{m.Movimientos} movimientos, profundidad {m.ProfundidadMaxima}");
    }

    private static void Pruebas()
    {
        int correctas = 0;
        void Verificar(bool condicion, string nombre)
        {
            if (!condicion) throw new InvalidOperationException($"Falló la prueba: {nombre}");
            correctas++;
            Console.WriteLine($"[OK] {nombre}");
        }

        Verificar(Ejercicios.FactorialRecursivo(0) == 1 &&
            Ejercicios.FactorialIterativo(0) == 1, "Factorial: caso base");
        Verificar(Ejercicios.FactorialRecursivo(5) == 120 &&
            Ejercicios.FactorialIterativo(5) == 120, "Factorial(5) = 120");
        Verificar(Ejercicios.FibonacciRecursivo(0) == 0 &&
            Ejercicios.FibonacciIterativo(1) == 1, "Fibonacci: bases");
        Verificar(Ejercicios.FibonacciRecursivo(7) == 13 &&
            Ejercicios.FibonacciIterativo(7) == 13, "Fibonacci(7) = 13");
        Verificar(Ejercicios.SumarRecursivo(Array.Empty<int>()) == 0 &&
            Ejercicios.SumarIterativo(Array.Empty<int>()) == 0, "Arreglo vacío");
        Verificar(Ejercicios.SumarRecursivo(new[] { 3, 5, 7, 9 }) == 24 &&
            Ejercicios.SumarIterativo(new[] { 3, 5, 7, 9 }) == 24, "Suma = 24");
        Verificar(Ejercicios.BuscarLinealRecursivo(new[] { 3, 5, 7, 9 }, 7) == 2 &&
            Ejercicios.BuscarLinealIterativo(new[] { 3, 5, 7, 9 }, 7) == 2,
            "Búsqueda: elemento presente");
        Verificar(Ejercicios.BuscarLinealRecursivo(new[] { 3, 5, 7, 9 }, 8) == -1 &&
            Ejercicios.BuscarLinealIterativo(new[] { 3, 5, 7, 9 }, 8) == -1,
            "Búsqueda: elemento ausente");
        foreach (int n in new[] { 0, 1, 3, 5, 8 })
        {
            MetricasHanoi m = Hanoi.ResolverRecursivo(n);
            long esperado = (1L << n) - 1;
            Verificar(m.Movimientos == esperado &&
                Hanoi.ResolverIterativo(n) == esperado &&
                m.Llamadas == 2 * esperado + 1 &&
                m.ProfundidadMaxima == n + 1 &&
                Hanoi.ValidarSolucion(n, true) && Hanoi.ValidarSolucion(n, false),
                $"Hanoi({n}): movimientos válidos y métricas");
        }
        Console.WriteLine($"\nPruebas superadas: {correctas} de {correctas}");
    }

    private static void Benchmark()
    {
        const int repeticiones = 7;
        int[] tamanos = { 3, 5, 8, 10, 12, 15 };
        var csv = new StringBuilder("discos,metodo,movimientos,llamadas,profundidad_maxima,mediana_ms\n");
        Console.WriteLine("\nResultados: mediana de 7 repeticiones (sin imprimir movimientos)");
        Console.WriteLine("n | método     | movimientos | llamadas | profundidad | mediana (ms)");
        foreach (int n in tamanos)
        {
            // Calentamiento: reduce el efecto inicial del JIT en la medida.
            Hanoi.ResolverRecursivo(n);
            Hanoi.ResolverIterativo(n);
            foreach (bool recursivo in new[] { true, false })
            {
                double[] tiempos = new double[repeticiones];
                long movimientos = 0;
                long llamadas = 0;
                int profundidad = 0;
                for (int i = 0; i < repeticiones; i++)
                {
                    Stopwatch reloj = Stopwatch.StartNew();
                    if (recursivo)
                    {
                        MetricasHanoi m = Hanoi.ResolverRecursivo(n);
                        movimientos = m.Movimientos;
                        llamadas = m.Llamadas;
                        profundidad = m.ProfundidadMaxima;
                    }
                    else movimientos = Hanoi.ResolverIterativo(n);
                    reloj.Stop();
                    tiempos[i] = reloj.Elapsed.TotalMilliseconds;
                }
                Array.Sort(tiempos);
                double mediana = tiempos[repeticiones / 2];
                string metodo = recursivo ? "recursivo" : "iterativo";
                string ms = mediana.ToString("F6", CultureInfo.InvariantCulture);
                csv.AppendLine($"{n},{metodo},{movimientos},{llamadas},{profundidad},{ms}");
                Console.WriteLine($"{n,2} | {metodo,-10} | {movimientos,11} | " +
                    $"{llamadas,8} | {profundidad,11} | {ms}");
            }
        }
        string ruta = Path.Combine(Directory.GetCurrentDirectory(), "resultados_hanoi.csv");
        File.WriteAllText(ruta, csv.ToString(), new UTF8Encoding(true));
        Console.WriteLine($"\nArchivo CSV creado: {ruta}");
        Console.WriteLine("Las columnas 'llamadas' y 'profundidad_maxima' " +
            "solo aplican a la versión recursiva.");
    }
}
