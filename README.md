# Proyecto-final-Scripting
David Vanegas L y Juana Vargas O

---

### **Sistema de Animación por Estados para Personajes en C#**

#### **Descripción**  
El proyecto consiste en desarrollar un sistema de animación basado en una Máquina de Estados Finita (FSM) utilizando C# y el sistema de animación Mecanim de Unity. Este sistema permitirá gestionar transiciones de animaciones según eventos y condiciones específicas, mejorando la modularidad y reusabilidad de la lógica de animación en diferentes personajes o rigs.  

#### **Diferenciación con Mecanim**  
Unity ya proporciona Mecanim para la gestión de animaciones, pero este proyecto busca ir más allá de su uso básico al:  
- Implementar una capa de abstracción en C# que permita reutilizar lógicas de animación en distintos personajes sin depender de cada Animator individual.  
- Optimizar la gestión de estados y transiciones a través de estructuras de datos eficientes.  
- Integrar un sistema de notificaciones basado en el patrón **Observer** para que otros componentes del juego reaccionen a los cambios de estado en la animación (ej., activar efectos de sonido, modificar físicas, etc.).  
- Permitir la creación dinámica de nuevas configuraciones de FSM sin necesidad de configurar manualmente cada Animator en Unity.  

#### **Alcance**  
- Implementación de un **controlador de estados de animación** en C# que interactúe con Mecanim.  
- Aplicación de patrones de diseño (**State** y **Observer**) para modularidad y reusabilidad.  
- Integración de pruebas unitarias para validar el correcto funcionamiento de las transiciones y eventos.  
- Uso de **diccionarios y grafos dirigidos** en lugar de solo listas y arreglos para optimizar la gestión de estados y eventos.  
- Diseño del sistema de manera que pueda aplicarse a múltiples personajes con distintas configuraciones de rigging sin necesidad de duplicar lógica de animación.  

#### **Eventos y Condiciones Específicas**  
El sistema de animación considerará eventos específicos según el tipo de personaje que se anime. Algunos ejemplos incluyen:  
- **Personajes humanoides:** estados como *Idle, Walk, Run, Jump, Attack* que cambiarán según la entrada del jugador.  
- **Criaturas no humanoides:** lógica adaptada a locomoción alternativa, como *Gateo, Vuelo, Salto con impulso*.  
- **NPCs:** transiciones basadas en IA, como *Patrullaje → Persecución → Ataque*.  
- **Interacción con el entorno:** cambios en animación según terreno (ej. caminar sobre lodo reduce la velocidad y afecta la animación).  

#### **Herramientas y Tecnologías**  
- **Lenguaje de programación:** C#  
- **Motor gráfico:** Unity (Mecanim)  
- **Framework de pruebas unitarias:** NUnit  
- **Estructuras de datos:** Diccionarios y grafos para mapear estados y eventos  
- **Patrones de diseño:**  
  - **State**: Modularización de estados de animación.  
  - **Observer**: Notificación de cambios a otros sistemas del juego (ej. efectos de sonido, IA, físicas).  

---
