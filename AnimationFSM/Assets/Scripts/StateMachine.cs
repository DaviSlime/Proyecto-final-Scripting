using System.Collections.Generic;
using UnityEngine; 

public class StateMachine
{
    private Dictionary<string, State> states = new Dictionary<string, State>();
    private State currentState;
    private string currentStateName;

    public void AddState(string name, State state)
    {
        if (state == null)
        {
            Debug.LogError($"Cannot add null state with name '{name}'");
            return;
        }
        if (states.ContainsKey(name))
        {
            Debug.LogWarning($"State with name '{name}' already exists. Overwriting.");
        }
        states[name] = state;
    }

    public void SetState(string name)
    {
        if (currentStateName == name && currentState != null)
        {
            //Debug.Log($"Already in state: {name}"); // Opcional: para depuración
            return;
        }

        if (!states.ContainsKey(name))
        {
            Debug.LogError($"State with name '{name}' not found!");
            return;
        }

        if (currentState != null)
        {
            currentState.Exit();
        }

        currentStateName = name; 
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

    public string GetCurrentStateName()
    {
        return currentStateName;
    }
}