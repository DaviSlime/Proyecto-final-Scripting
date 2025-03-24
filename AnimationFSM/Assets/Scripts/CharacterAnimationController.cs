using UnityEngine;

public class CharacterAnimationController : MonoBehaviour
{
    private StateMachine stateMachine;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        stateMachine = new StateMachine();

        // Agregar estados a la máquina de estados
        stateMachine.AddState("Idle", new IdleState(animator));
        stateMachine.AddState("Walk", new WalkState(animator));

        stateMachine.SetState("Idle");
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            stateMachine.SetState("Walk");
        }
        else
        {
            stateMachine.SetState("Idle");
        }

        stateMachine.Update();
    }
}
