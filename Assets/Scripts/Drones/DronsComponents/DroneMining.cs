using System.Collections;
using UnityEngine;

public class DroneMining : MonoBehaviour
{
    [SerializeField] private float _miningTime = 2;
    [SerializeField] private float _unloadingTime = 2;

    private DroneStateMachine _droneStateMachine;
    private DroneMovement _droneMovement;
    private DronesBase _thisDroneBase;

    private Resource _resource;

    private Coroutine _miningCoroutine;
    private Coroutine _unloadingCoroutine;

    public void Initialize(DroneStateMachine droneStateMachine, DroneMovement droneMovement, DronesBase droneBase)
    {
        _droneStateMachine = droneStateMachine;
        _droneMovement = droneMovement;
        _thisDroneBase = droneBase;

        _droneStateMachine.StateChanged += OnStateChanged;
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (_droneStateMachine.ActiveState)
        {
            case DroneState.Following:
                if (other.TryGetComponent<Resource>(out var resource) && resource == _droneMovement.NearestResource)
                {
                    //_droneStateMachine.ChangeState(DroneState.Mining);
                    _resource = resource;
                    _miningCoroutine = StartCoroutine(MiningRoutine());
                }
                break;
            case DroneState.Return:
                if (other.TryGetComponent<DronesBase>(out var droneBase) && droneBase == _thisDroneBase)
                {
                    _droneStateMachine.ChangeState(DroneState.Unloading);
                }
                break;
        }
    }

    private IEnumerator MiningRoutine()
    {
        _droneStateMachine.ChangeState(DroneState.Mining);
        yield return new WaitForSeconds(_miningTime);
        _resource.Deactivate();
        _droneStateMachine.ChangeState(DroneState.Return);
    }

    private void OnStateChanged(DroneState state)
    {
        if (state == DroneState.Unloading)
        {
            PlayUnloadingEffects();
        }
    }

    private void PlayUnloadingEffects()
    {
        _unloadingCoroutine = StartCoroutine(UnloadingEffectsRoutine());
        //TODO Effects
    }

    private IEnumerator UnloadingEffectsRoutine()
    {
        _droneMovement.StopMove();
        yield return new WaitForSeconds(_unloadingTime);
        _droneStateMachine.ChangeState(DroneState.Following);
    }

    private void OnDisable()
    {
        if (_miningCoroutine != null)
        {
            StopCoroutine(_miningCoroutine);
        }

        if (_unloadingCoroutine != null)
        {
            StopCoroutine(_unloadingCoroutine);
        }
    }
}
