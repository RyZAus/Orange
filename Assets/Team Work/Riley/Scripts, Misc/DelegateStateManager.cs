using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateStateManager
{
    public DelegateState currentState;

    public void ChangeState(DelegateState thisState)
    {
        if (currentState != null)
        {
            currentState.isRunning = false;
            currentState.Exit.Invoke();
        }

        if (thisState != null)
        {
            thisState.isRunning = true;
            thisState.Enter.Invoke();
            currentState = thisState;
        }
    }

    public void UpdateState()
    {
        if (currentState != null)
        {
            currentState.Update.Invoke();
        }
    }
}
