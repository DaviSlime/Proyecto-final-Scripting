using UnityEngine;

public class IdleState : State
{
    private const string SPEED_PARAMETER = "Speed";

    public IdleState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        Debug.Log("Entering Idle State"); 
        animator.SetFloat(SPEED_PARAMETER, 0f);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        Debug.Log("Exiting Idle State"); 
    }
}