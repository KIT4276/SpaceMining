using System;

public class DroneStateMachine 
{
    public DroneState ActiveState { get; private set; }

    public event Action<DroneState> StateChanged;

    public DroneStateMachine()
    {
        ActiveState =  DroneState.StartState;
    }

    public void ChangeState(DroneState followingState)
    {
        ActiveState = followingState;
        StateChanged?.Invoke(ActiveState);
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