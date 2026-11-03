using System;
using System.Collections.Generic;

namespace Unidad2Recursividad;

public sealed class MetricasHanoi
{
    public long Llamadas { get; internal set; }
    public long Movimientos { get; internal set; }
    public int ProfundidadMaxima { get; internal set; }
}

public static class Hanoi
{
    // Callback: (número de disco, torre de origen, torre de destino).
    public static MetricasHanoi ResolverRecursivo(
        int discos, Action<int, char, char>? alMover = null)
    {
        Validar(discos);
        var metricas = new MetricasHanoi();
        Resolver(discos, 'A', 'B', 'C', 1, alMover, metricas);
        return metricas;
    }

    private static void Resolver(int n, char origen, char auxiliar, char destino,
        int profundidad, Action<int, char, char>? alMover, MetricasHanoi m)
    {
        m.Llamadas++;
        m.ProfundidadMaxima = Math.Max(m.ProfundidadMaxima, profundidad);
        if (n == 0) return;                              // Caso base.

        Resolver(n - 1, origen, destino, auxiliar, profundidad + 1, alMover, m);
        alMover?.Invoke(n, origen, destino);
        m.Movimientos++;
        Resolver(n - 1, auxiliar, origen, destino, profundidad + 1, alMover, m);
    }

    // Equivalencia iterativa: emula el árbol de ejecución con una pila explícita.
    private readonly record struct Paso(int N, char Origen, char Auxiliar,
        char Destino, bool Mover);

    public static long ResolverIterativo(
        int discos, Action<int, char, char>? alMover = null)
    {
        Validar(discos);
        var pendientes = new Stack<Paso>();
        pendientes.Push(new Paso(discos, 'A', 'B', 'C', false));
        long movimientos = 0;

        while (pendientes.Count > 0)
        {
            Paso paso = pendientes.Pop();
            if (paso.Mover)
            {
                alMover?.Invoke(paso.N, paso.Origen, paso.Destino);
                movimientos++;
                continue;
            }
            if (paso.N == 0) continue;

            // LIFO: se apilan en el orden inverso al que se ejecutarán.
            pendientes.Push(new Paso(paso.N - 1, paso.Auxiliar,
                paso.Origen, paso.Destino, false));
            pendientes.Push(new Paso(paso.N, paso.Origen,
                paso.Auxiliar, paso.Destino, true));
            pendientes.Push(new Paso(paso.N - 1, paso.Origen,
                paso.Destino, paso.Auxiliar, false));
        }
        return movimientos;
    }

    public static bool ValidarSolucion(int discos, bool recursivo)
    {
        Validar(discos);
        var torres = new Dictionary<char, Stack<int>>
        {
            ['A'] = new Stack<int>(),
            ['B'] = new Stack<int>(),
            ['C'] = new Stack<int>()
        };
        for (int n = discos; n >= 1; n--) torres['A'].Push(n);
        bool movimientosValidos = true;
        void Verificar(int disco, char origen, char destino)
        {
            Stack<int> desde = torres[origen];
            Stack<int> hacia = torres[destino];
            if (desde.Count == 0 || desde.Peek() != disco ||
                (hacia.Count > 0 && hacia.Peek() < disco))
            {
                movimientosValidos = false;
                return;
            }
            desde.Pop();
            hacia.Push(disco);
        }
        if (recursivo) ResolverRecursivo(discos, Verificar);
        else ResolverIterativo(discos, Verificar);
        return movimientosValidos && torres['A'].Count == 0 &&
               torres['B'].Count == 0 && torres['C'].Count == discos;
    }

    private static void Validar(int discos)
    {
        if (discos < 0 || discos > 16)
            throw new ArgumentOutOfRangeException(nameof(discos),
                "Utiliza entre 0 y 16 discos para la práctica.");
    }
}
