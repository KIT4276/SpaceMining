using System;
using UnityEngine;

public class DroneStateMachine 
{
    private string _name;

    public DroneState ActiveState { get; private set; }

    public event Action<DroneState> StateChanged;

    public DroneStateMachine(string name)
    {
         _name = name;
        ActiveState =  DroneState.Start;
    }

    public void ChangeState(DroneState followingState)
    {
        ActiveState = followingState;
        
        StateChanged?.Invoke(ActiveState);
    }
}

    public enum DroneState
    {
        Start,
        Following,
        Mining,
        Return,
        Unloading,
    }