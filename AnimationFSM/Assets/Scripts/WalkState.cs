using UnityEngine;

public class WalkState : State
{
    public WalkState(Animator animator) : base(animator) { }

    public override void Enter()
    {
        animator.Play("Walk");
    }

    public override void Update() { }

    public override void Exit() { }
}
