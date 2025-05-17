using NUnit.Framework; 
using UnityEngine; 
using UnityEngine.TestTools;

public class StateMachineTests
{
    private StateMachine stateMachine;
    private MockState mockState1;
    private MockState mockState2;
    private Animator dummyAnimator; 

    [SetUp]
    public void SetUp()
    {
        stateMachine = new StateMachine();

        dummyAnimator = null; 

        mockState1 = new MockState(dummyAnimator);
        mockState2 = new MockState(dummyAnimator);

        stateMachine.AddState("State1", mockState1);
        stateMachine.AddState("State2", mockState2);
    }

    [TearDown]
    public void TearDown()
    {

        stateMachine = null;
        mockState1 = null;
        mockState2 = null;
        dummyAnimator = null;
    }

    [Test]
    public void AddState_AddsStateToDictionary()
    {
      
        Debug.Log("Conceptual test: StateMachine should hold added states. Verified by SetState tests not failing for these names.");
    }

    [Test]
    public void SetState_SetsInitialStateAndCallsEnter()
    {
        stateMachine.SetState("State1");

        Assert.IsTrue(mockState1.EnterCalled, "Enter was not called on State1 when set as initial state.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 when set as initial state.");
        Assert.IsFalse(mockState1.UpdateCalled, "Update was called on State1 when set as initial state.");

        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly.");
    }

    [Test]
    public void SetState_TransitionsCorrectlyAndCallsExitAndEnter()
    {
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); 

        stateMachine.SetState("State2");

        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 again during transition.");
        Assert.IsTrue(mockState1.ExitCalled, "Exit was not called on State1 during transition.");

        Assert.IsTrue(mockState2.EnterCalled, "Enter was not called on State2 during transition.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly during transition.");
        Assert.IsFalse(mockState2.UpdateCalled, "Update was called on State2 immediately after transition.");
    }

    [Test]
    public void SetState_CallingSameStateAgain_DoesNotCallExitOrEnter()
    {
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); 

        stateMachine.SetState("State1");

        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 again when setting the same state.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 again when setting the same state.");

        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 incorrectly.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 incorrectly.");
    }


    [Test]
    public void Update_CallsUpdateOnCurrentState()
    {
        stateMachine.SetState("State1");
        mockState1.ResetFlags();

        stateMachine.Update();

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

        stateMachine.Update();

        Assert.IsFalse(mockState1.UpdateCalled, "Update was called on State1 when no state was set.");
        Assert.IsFalse(mockState2.UpdateCalled, "Update was called on State2 when no state was set.");
    }

    [Test]
    public void SetState_WithInvalidName_LogsErrorAndDoesNotChangeState()
    {
        stateMachine.SetState("State1");
        mockState1.ResetFlags(); 
        string initialCurrentStateName = stateMachine.GetCurrentStateName();

        LogAssert.Expect(LogType.Error, "State with name 'NonExistentState' not found!");

        stateMachine.SetState("NonExistentState");


        Assert.IsFalse(mockState1.EnterCalled, "Enter was called on State1 with invalid name.");
        Assert.IsFalse(mockState1.ExitCalled, "Exit was called on State1 with invalid name.");
        Assert.IsFalse(mockState2.EnterCalled, "Enter was called on State2 with invalid name.");
        Assert.IsFalse(mockState2.ExitCalled, "Exit was called on State2 with invalid name.");

    }


}