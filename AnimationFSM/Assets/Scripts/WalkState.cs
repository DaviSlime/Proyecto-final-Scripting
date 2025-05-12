using UnityEngine;

public class WalkState : State
{
    // Nombre del parámetro en el Animator Controller
    private const string SPEED_PARAMETER = "Speed";

    public WalkState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        Debug.Log("Entering Walk State"); 
        animator.SetFloat(SPEED_PARAMETER, 1.0f);
    }

    public override void Update()
    {

    }

    public override void Exit()
    {
        Debug.Log("Exiting Walk State"); 
    }
}