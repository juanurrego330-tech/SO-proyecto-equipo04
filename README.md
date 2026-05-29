# PlanificadorProcesosRR

Equipo: SO-EQUIPO-04

Integrantes:
- Juan José Urrego Tejada

---

Nombre del proyecto: Simulación de Planificador Round Robin  
Modalidad: Simulación en consola (programación secuencial)

---


Este proyecto implementa una simulación básica del algoritmo de planificación Round Robin.  
Permite crear procesos con un tiempo de ejecución definido por el usuario y simula su ejecución por turnos.  
En cada iteración se actualiza el estado del proceso y su tiempo restante hasta que todos terminan.  
La simulación se muestra en consola en tiempo real.

---

## Lenguaje y dependencias

Lenguaje: C#  
Framework: .NET  
Dependencias:  
- System  
- System.Collections.Generic  
- System.Threading
- System.Linq
- Microsoft.Data.SqlClient

No requiere librerías externas.

---

## Instrucciones de ejecución

1. Abrir el proyecto en Visual Studio o compilar desde consola.
2. Ejecutar el programa.
3. Ingresar la cantidad de procesos.
4. Ingresar el tiempo de ejecución para cada proceso.
5. Ingrese el valor de Quantum
6. Observar la simulación en consola hasta que finalice.

---

## Qué funciona y qué no (Primera Entrega)

### Funciona:
- Validación de entrada con TryParse.
- Simulación básica del algoritmo Round Robin.

### No funciona / No implementado aún:
- Creacion de clases.
- Ejecución concurrente con hilos reales.
- Manejo de prioridades.
- Simulacion de procesos.
- Procesos en estado BLOCKED.
- Interfaz gráfica.
- Persistencia de datos.


### Qué funciona y qué no — Segunda Entrega

Funciona:

- Validación de entrada con TryParse.
- Creación de clases (Proceso, SimulationClock, RoundRobinScheduler).
- Simulación completa del algoritmo Round Robin con quantum configurable.
- Ejecución concurrente con hilos reales (Thread, lock, Monitor).
- Simulación de procesos con ciclo de vida completo (NEW → READY → RUNNING → TERMINATED).
- Manejo de ArrivalTime dinámico (los procesos llegan en distintos momentos).
- CPU idle cuando no hay procesos listos (el reloj sigue avanzando).
- Cálculo de métricas por proceso: WaitingTime, TurnaroundTime, CompletionTime.
- Cálculo de promedios globales de WT y TAT.
- Visualización en tiempo real del estado de cada proceso por consola.

No funciona / No implementado aún:

- Procesos en estado BLOCKED (no hay simulación de I/O ni esperas intermedias).
- Interfaz gráfica.
- Persistencia de datos (los resultados no se guardan en archivo ni base de datos).

- ### Qué funciona y qué no — Tercera Entrega (Entrega Final)

Funciona:
- **Interfaz Gráfica Avanzada (GUI):** Implementación completa en Windows Forms con un diseño oscuro (Dark Mode) estructurado mediante pestañas (`TabControl`) y tablas dinámicas (`DataGridView`).
- **Mapeo de Datos en Tiempo Real:** Visualización asíncrona del ciclo de vida de los hilos, mostrando estados, ráfagas, tiempos de llegada y barra de porcentaje de CPU sin congelar la pantalla.
- **Persistencia Relacional:** Conexión directa a base de datos local (`SQL Server`) mediante ADO.NET (`SqlConnection`, `SqlCommand`, `SqlDataReader`) para almacenar el histórico masivo de ejecuciones de forma permanente.
- **Módulo de Historial Personalizado:** Pantalla de auditoría interna que lee la base de datos fila por fila usando un ciclo optimizado, inyectando los datos con formato legible para el usuario (`KB` para memoria, `s` para tiempos).
- **Simulación Completa de Recursos:** Inclusión de la métrica de consumo de Memoria (KB) asignada dinámicamente por proceso, integrada en el cálculo de promedios globales.
- **Control de Flujo:** Botones funcionales para limpiar tablas de simulación activa, reiniciar parámetros, navegar entre formularios y cerrar la aplicación de manera segura.

No funciona / No implementado aún:
- Procesos en estado BLOCKED (Se decidio no implementarlo).
