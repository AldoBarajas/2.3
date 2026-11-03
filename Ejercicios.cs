using System;

namespace Unidad2Recursividad;

/// <summary>Versiones iterativas y recursivas del catálogo de la Tarea 2.2.</summary>
public static class Ejercicios
{
    // Factorial: n! = 1 si n=0; n! = n*(n-1)! si n>0.
    public static long FactorialRecursivo(int n)
    {
        ValidarFactorial(n);
        return FactorialRecursivoInterno(n);
    }

    private static long FactorialRecursivoInterno(int n)
    {
        if (n <= 1) return 1;                         // Caso base.
        return checked(n * FactorialRecursivoInterno(n - 1)); // Caso recursivo.
    }

    public static long FactorialIterativo(int n)
    {
        ValidarFactorial(n);
        long resultado = 1;
        for (int i = 2; i <= n; i++) resultado = checked(resultado * i);
        return resultado;
    }

    private static void ValidarFactorial(int n)
    {
        if (n < 0 || n > 20)
            throw new ArgumentOutOfRangeException(nameof(n), "Utiliza enteros entre 0 y 20.");
    }

    // Fibonacci: F(0)=0; F(1)=1; F(n)=F(n-1)+F(n-2).
    // Se limita el caso clásico recursivo a 35 para evitar esperas excesivas.
    public static long FibonacciRecursivo(int n)
    {
        ValidarFibonacci(n);
        return FibonacciRecursivoInterno(n);
    }

    private static long FibonacciRecursivoInterno(int n)
    {
        if (n <= 1) return n;                         // Caso base.
        return FibonacciRecursivoInterno(n - 1)
             + FibonacciRecursivoInterno(n - 2);       // Caso recursivo.
    }

    public static long FibonacciIterativo(int n)
    {
        ValidarFibonacci(n);
        if (n <= 1) return n;
        long anterior = 0, actual = 1;
        for (int i = 2; i <= n; i++)
        {
            long siguiente = anterior + actual;
            anterior = actual;
            actual = siguiente;
        }
        return actual;
    }

    private static void ValidarFibonacci(int n)
    {
        if (n < 0 || n > 35)
            throw new ArgumentOutOfRangeException(nameof(n), "Utiliza enteros entre 0 y 35.");
    }

    // Suma de arreglo: suma desde índice i; si i==Length devuelve 0.
    public static long SumarRecursivo(int[] datos)
    {
        ArgumentNullException.ThrowIfNull(datos);
        return SumarDesde(datos, 0);
    }

    private static long SumarDesde(int[] datos, int indice)
    {
        if (indice == datos.Length) return 0;         // Caso base.
        return datos[indice] + SumarDesde(datos, indice + 1); // Caso recursivo.
    }

    public static long SumarIterativo(int[] datos)
    {
        ArgumentNullException.ThrowIfNull(datos);
        long suma = 0;
        foreach (int valor in datos) suma += valor;
        return suma;
    }

    // Búsqueda lineal: -1 si se recorrió todo; índice si se encontró.
    public static int BuscarLinealRecursivo(int[] datos, int buscado)
    {
        ArgumentNullException.ThrowIfNull(datos);
        return BuscarDesde(datos, buscado, 0);
    }

    private static int BuscarDesde(int[] datos, int buscado, int indice)
    {
        if (indice == datos.Length) return -1;        // Caso base: no encontrado.
        if (datos[indice] == buscado) return indice;   // Caso base: encontrado.
        return BuscarDesde(datos, buscado, indice + 1); // Caso recursivo.
    }

    public static int BuscarLinealIterativo(int[] datos, int buscado)
    {
        ArgumentNullException.ThrowIfNull(datos);
        for (int indice = 0; indice < datos.Length; indice++)
            if (datos[indice] == buscado) return indice;
        return -1;
    }
}
