using System;
using System.Collections;
using UnityEngine;

//[RequireComponent(typeof(Collider))]
public class TriggerHandler : MonoBehaviour
{
    [SerializeField] private float _miningTime = 2;
    [SerializeField] private DroneStateMachine _droneStateMachine;
    [SerializeField] private Drone _drone;

    private Resource _resource;

    private void OnTriggerEnter(Collider other)
    {
        if (_droneStateMachine.ActiveState == DroneState.FollowingState
            && other.TryGetComponent<Resource>(out var resource)
            && resource == _drone.NearestResource)
        {
            _droneStateMachine.ChangeState(DroneState.MiningState);
            _resource = resource;
            StartCoroutine(MiningRoutine());

        }
        else if (_droneStateMachine.ActiveState == DroneState.ReturnState
                && other.TryGetComponent<DroneBase>(out var droneBase)
                && droneBase == _drone.DroneBase)
        {
            _droneStateMachine.ChangeState(DroneState.UnloadingState);
        }
    }

    private IEnumerator MiningRoutine()
    {
        _droneStateMachine.ChangeState(DroneState.MiningState);
        yield return new WaitForSeconds(_miningTime);
        _resource.Deactivate();
        _droneStateMachine.ChangeState(DroneState.ReturnState);
    }
}
