using System.Collections.Generic;

public class StateMachine
{
    private Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;

    public void AddState(string name, State state)
    {
        states[name] = state;
    }

    public void SetState(string name)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = states[name];
        currentState.Enter();
    }

    public void Update()
    {
        if (currentState != null)
        {
            currentState.Update();
        }
    }
}
