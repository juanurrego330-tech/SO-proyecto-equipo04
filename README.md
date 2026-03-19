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
