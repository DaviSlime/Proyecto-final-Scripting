using UnityEngine;
using System.Collections.Generic; // Asegúrate de tener este using si tu StateMachine lo necesita

[RequireComponent(typeof(Animator))]
public class CharacterAnimationController : MonoBehaviour
{
    private StateMachine stateMachine;
    private Animator animator;

    // **Campo para asignar el AnimatorOverrideController desde el Inspector**
    // Este override controller debe tener asignado tu Animator Controller base
    // y los clips de animación específicos de este personaje.
    [SerializeField]
    private AnimatorOverrideController characterOverrideController;

    private const string IDLE_STATE_NAME = "Idle";
    private const string WALK_STATE_NAME = "Walk";

    void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("CharacterAnimationController requires an Animator component!");
            enabled = false; 
            return;
        }


        if (characterOverrideController != null)
        {
            animator.runtimeAnimatorController = characterOverrideController;
        }
        else
        {

            Debug.LogWarning("No Animator Override Controller assigned to " + gameObject.name + ". Using the default controller.");
        }


        stateMachine = new StateMachine();

        stateMachine.AddState(IDLE_STATE_NAME, new IdleState(animator));
        stateMachine.AddState(WALK_STATE_NAME, new WalkState(animator));

        stateMachine.SetState(IDLE_STATE_NAME);

        animator.applyRootMotion = true;
    }

    void Update()
    {
        bool isMoving = Input.GetKey(KeyCode.W); 


        if (isMoving)
        {
            stateMachine.SetState(WALK_STATE_NAME);
        }
        else
        {
            stateMachine.SetState(IDLE_STATE_NAME);
        }


        stateMachine.Update();
    }

}