using UnityEngine;
using NUnit.Framework; // Necesario para las pruebas

// Esta clase simula un Estado para poder probar la StateMachine sin un Animator real
public class MockState : State
{
    public bool EnterCalled = false;
    public bool UpdateCalled = false;
    public bool ExitCalled = false;

    // Constructor que llama al constructor base (aunque no usemos el animator real)
    public MockState(Animator animator) : base(animator)
    {
        // Aquí podríamos inicializar algo si fuera necesario para el mock
    }

    public override void Enter()
    {
        EnterCalled = true;
        // Debug.Log("MockState Enter Called"); // Opcional para depuración
    }

    public override void Update()
    {
        UpdateCalled = true;
        // Debug.Log("MockState Update Called"); // Opcional para depuración
    }

    public override void Exit()
    {
        ExitCalled = true;
        // Debug.Log("MockState Exit Called"); // Opcional para depuración
    }

    // Método de ayuda para resetear los flags antes de una prueba
    public void ResetFlags()
    {
        EnterCalled = UpdateCalled = ExitCalled = false;
    }
}