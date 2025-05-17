using UnityEngine;
using NUnit.Framework; 
public class MockState : State
{
    public bool EnterCalled = false;
    public bool UpdateCalled = false;
    public bool ExitCalled = false;

    public MockState(Animator animator) : base(animator)
    {

    }

    public override void Enter()
    {
        EnterCalled = true;
    }

    public override void Update()
    {
        UpdateCalled = true;
    }

    public override void Exit()
    {
        ExitCalled = true;
    }

    public void ResetFlags()
    {
        EnterCalled = UpdateCalled = ExitCalled = false;
    }
}