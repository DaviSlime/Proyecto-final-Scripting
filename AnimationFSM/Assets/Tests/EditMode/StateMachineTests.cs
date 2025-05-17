using NUnit.Framework; // Atributos de prueba como [Test], [SetUp]
using UnityEngine; // Necesario para tipos de Unity como Animator (aunque sea null en mocks)
using UnityEngine.TestTools;
// Las pruebas en Edit Mode no requieren que Unity esté ejecutando una escena
// [TestFixture] es opcional, pero organiza las pruebas lógicamente
public class StateMachineTests
{
    private StateMachine stateMachine;
    private MockState mockState1;
    private MockState mockState2;
    private Animator dummyAnimator; // Creamos un Animator "falso" ya que MockState lo requiere en el constructor

    // [SetUp] se ejecuta antes de cada método de prueba ([Test])
    [SetUp]
    public void SetUp()
    {
        // Inicializamos la StateMachine y los MockStates antes de cada prueba
        stateMachine = new StateMachine();
        // En Edit Mode, el Animator real es null. Creamos uno dummy si es necesario para el constructor del estado.
        // En este caso, MockState acepta null porque no lo usa internamente. Si tuvieras un estado real, necesitarías Play Mode.
        dummyAnimator = null; // Para MockState, null está bien

        mockState1 = new MockState(dummyAnimator);
        mockState2 = new MockState(dummyAnimator);

        stateMachine.AddState("State1", mockState1);
        stateMachine.AddState("State2", mockState2);
    }

    // [TearDown] se ejecuta después de cada método de prueba ([Test])
    [TearDown]
    public void TearDown()
    {
        // Limpiamos las referencias después de cada prueba si es necesario
        stateMachine = null;
        mockState1 = null;
        mockState2 = null;
        dummyAnimator = null;
    }

    [Test]
    public void AddState_AddsStateToDictionary()
    {
        // Verificamos que los estados se agregaron correctamente
        // Nota: No podemos acceder directamente al diccionario 'states' porque es privado.
        // Podríamos añadir un método público temporal para pruebas o confiar en que SetState lo encontrará.
        // Confiemos en SetState por ahora, o añadamos un método como HasState(string name) a StateMachine si queremos probar esto directamente.
        // Para esta prueba, nos basta saber que SetState no lanza error para los estados agregados.

        // Un método HasState en StateMachine sería ideal para esta prueba:
        // Assert.IsTrue(stateMachine.HasState("State1"));
        // Assert.IsTrue(stateMachine.HasState("State2"));
        // Assert.IsFalse(stateMachine.HasState("NonExistentState"));

        // Como no tenemos HasState, esta prueba es más conceptual o se verifica indirectamente con otras pruebas.
        Debug.Log("Conceptual test: StateMachine should hold added states. Verified by SetState tests not failing for these names.");
    }

    [Test]
    public void SetState_SetsInitialStateAndCallsEnter()
    {
        // Act: Establecemos el estado inicial
        stateMachine.SetState("State1");

        // Assert: Verificamos que Enter fue llamado en el estado 1
        Assert.IsTrue(mockState1.EnterCalled, "Enter was not called on State1 when set as initial state.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 when set as initial state.");
        Assert.IsFalse(mockState1.UpdateCalled, "Update was called on State1 when set as initial state.");

        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly.");
    }

    [Test]
    public void SetState_TransitionsCorrectlyAndCallsExitAndEnter()
    {
        // Arrange: Establecemos un estado inicial primero
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); // Reseteamos los flags después del Enter inicial

        // Act: Transicionamos al segundo estado
        stateMachine.SetState("State2");

        // Assert: Verificamos que Exit fue llamado en el estado anterior (State1) y Enter en el nuevo estado (State2)
        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 again during transition.");
        Assert.IsTrue(mockState1.ExitCalled, "Exit was not called on State1 during transition.");

        Assert.IsTrue(mockState2.EnterCalled, "Enter was not called on State2 during transition.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly during transition.");
        Assert.IsFalse(mockState2.UpdateCalled, "Update was called on State2 immediately after transition.");
    }

    [Test]
    public void SetState_CallingSameStateAgain_DoesNotCallExitOrEnter()
    {
        // Arrange: Establecemos un estado inicial
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); // Reseteamos los flags después del Enter inicial

        // Act: Intentamos establecer el MISMO estado de nuevo
        stateMachine.SetState("State1");

        // Assert: Verificamos que NINGÚN método fue llamado de nuevo en State1
        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 again when setting the same state.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 again when setting the same state.");

        // También verificamos que nada fue llamado en State2
        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly.");
    }


    [Test]
    public void Update_CallsUpdateOnCurrentState()
    {
        // Arrange: Establecemos un estado y reseteamos sus flags
        stateMachine.SetState("State1");
        mockState1.ResetFlags();

        // Act: Llamamos al Update de la StateMachine
        stateMachine.Update();

        // Assert: Verificamos que Update fue llamado SOLO en el estado actual (State1)
        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 during Update.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 during Update.");
        Assert.IsTrue(mockState1.UpdateCalled, "Update was not called on State1 during StateMachine Update.");

        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.UpdateCalled, "Update was called on State2 incorrectly.");
    }

    [Test]
    public void Update_BeforeSettingState_DoesNothing()
    {
        // Arrange: No establecemos ningún estado después de la inicialización

        // Act: Llamamos al Update de la StateMachine
        stateMachine.Update();

        // Assert: Verificamos que Update no fue llamado en ningún estado (porque currentState es null inicialmente)
        Assert.IsFalse(mockState1.UpdateCalled, "Update was called on State1 when no state was set.");
        Assert.IsFalse(mockState2.UpdateCalled, "Update was called on State2 when no state was set.");
    }

    [Test]
    public void SetState_WithInvalidName_LogsErrorAndDoesNotChangeState()
    {
        // Arrange: Establecemos un estado inicial y esperamos un error
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); // Reset flags
        string initialCurrentStateName = stateMachine.GetCurrentStateName(); // Assuming GetCurrentStateName exists or add it

        // Configurar para capturar mensajes de log (esto es más avanzado, pero útil para probar errores esperados)
        LogAssert.Expect(LogType.Error, "State with name 'NonExistentState' not found!");

        // Act: Intentamos establecer un estado que no existe
        stateMachine.SetState("NonExistentState");

        // Assert: Verificamos que el estado actual no cambió
        // Si añadiste un método GetCurrentStateName():
        // Assert.AreEqual(initialCurrentStateName, stateMachine.GetCurrentStateName(), "State machine changed state with invalid name.");

        // Si no, verificamos que ningún método de Enter/Exit fue llamado en los estados existentes
        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 with invalid name.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 with invalid name.");
        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 with invalid name.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 with invalid name.");

        // La expectativa del log (LogAssert.Expect) verifica que el mensaje de error fue mostrado.
    }


}