using System;
using UnityEngine;

public class DroneStateMachine : MonoBehaviour
{
    public DroneState ActiveState { get; private set; }

    public event Action<DroneState> StateChanged;

    public void ChangeState(DroneState followingState)
    {
        ActiveState = followingState;
        StateChanged?.Invoke(ActiveState);
    }

    private void Start()
    {
        ActiveState =  DroneState.StartState;
    }
}

    public enum DroneState
    {
        StartState,
        FollowingState,
        MiningState,
        ReturnState,
        UnloadingState,
    }