# Unidad 2: Recursividad - tareas 2.2 y 2.3

**Alumno:** Aldo Alberto Barajas Ancira  
**Docente:** Rubén Riojas  
**Institución:** Instituto Tecnológico Superior de Monclova

Proyecto de consola en **C# / .NET 8**, sin paquetes externos.

## Abrir y ejecutar (Windows / Visual Studio)

1. Instala Visual Studio 2022 con la carga de trabajo **Desarrollo de escritorio de .NET** o el SDK de .NET 8.
2. Abre `Recursividad.csproj` en Visual Studio y presiona `Ctrl + F5`, o abre una terminal en esta carpeta.
3. Ejecuta los comandos:

```powershell
dotnet run -- --demo
dotnet run -- --self-test
dotnet run -- --benchmark
```

Sin argumentos (`dotnet run`) aparece un menú interactivo. El benchmark crea **resultados_hanoi.csv** en la carpeta desde la que ejecutes el comando.

## Archivos

- `Ejercicios.cs`: versiones iterativas y recursivas de factorial, Fibonacci, suma de arreglos y búsqueda lineal.
- `Hanoi.cs`: solución recursiva y simulación iterativa con pila explícita, métricas y validación de movimientos.
- `Program.cs`: menú, demostración, 13 pruebas de escritorio automatizadas y benchmark de Hanoi.
- `Recursividad.csproj`: proyecto .NET 8.

## Límites intencionales

- Factorial: `0..20` para no desbordar `long`.
- Fibonacci: `0..35` para que la recursión ingenua no tarde excesivamente.
- Hanoi: `0..16` discos para limitar recursos; opción visual: `1..10`.
- La profundidad se mide en **niveles de llamadas**, incluyendo el caso base `n=0`. No equivale a bytes reales consumidos de la pila del sistema.
- La mediana de tiempo depende del equipo, JIT y condiciones de la ejecución. Ninguna cifra de tiempo real se debe afirmar antes de ejecutar el programa.

## Subida a GitHub

1. Crea un repositorio público en tu cuenta (ejemplo de nombre: `unidad2-recursividad-csharp`).
2. Sube `Recursividad.csproj`, `Program.cs`, `Ejercicios.cs`, `Hanoi.cs` y `README.md`.
3. Sustituye `PENDIENTE DE PUBLICAR` en los reportes por la URL pública auténtica.
4. Adjunta las capturas de las pruebas y el `resultados_hanoi.csv` generado en tu equipo.

## Evidencia de video

Publica los tres videos como **Público** en el **mismo canal de YouTube**. En OBS utiliza **Captura de pantalla** + **Dispositivo de captura de video (webcam)** y deja la webcam en la esquina inferior derecha. Las guías por tarea están en `Guias_Video`.

**Estado de validación:** código preparado y comprobaciones analíticas diseñadas. No se ejecutó el proyecto en .NET en el entorno de generación porque no había SDK de .NET; ejecútalo en Visual Studio para aportar pruebas reales.
