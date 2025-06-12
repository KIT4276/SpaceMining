using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    //[SerializeField] private ResourcesFactory _resourcesFactory;
    ////[SerializeField] private DroneBase _base;
    ////[SerializeField] private NavMeshAgent _agent;
    //[SerializeField] private DroneStateMachine _droneStateMachine;

    //public DroneBase DroneBase { get => _base; }

    ////public Resource NearestResource{ get; private set;}

    //public DroneStateMachine DroneStateMachine { get => _droneStateMachine; }

    //private void Start()
    //{
    //    //_resourcesFactory.Spawned += OnResourcesSpawned;
    //    _droneStateMachine.StateChanged += OnStateChanged;
    //}

    //private void Update()
    //{
    //    if(_droneStateMachine.ActiveState == DroneState.FollowingState)
    //    {
    //        if(NearestResource == null || !NearestResource.gameObject.activeInHierarchy)
    //        {
    //            NearestResource = null;
    //            _agent.ResetPath();
    //            FindNewDestination();
    //        }
    //    }
    //}
    //public void FindNewDestination()
    //{
    //    foreach (var res in _resourcesFactory.ActiveResourcesPool) // change to for
    //    {
    //        if (res != NearestResource)
    //        {
    //            if (NearestResource == null)
    //            {
    //                NearestResource = res;
    //            }
    //            else
    //            {
    //                CompareDistances(res);
    //            }
    //        }
    //        _agent.SetDestination(NearestResource.transform.position);
    //    }
    //}

    //private void OnStateChanged(DroneState state)
    //{
    //    //switch (state)
    //    //{
    //    //    //case DroneState.ReturnState:
    //    //    //    _agent.SetDestination(_base.transform.position);
    //    //    //    break;
    //    //    //case DroneState.UnloadingState:
    //    //    //    PlayUnloadingEffects();
    //    //        break;
    //    //    //case DroneState.FollowingState:
    //    //    //    FindNewDestination();
    //    //    //    break;

    //    //}
    //}
    
    //private void PlayUnloadingEffects()
    //{
    //    //TODO

    //    StartCoroutine(UnloadingEffectsRoutine());
    //}

    //private IEnumerator UnloadingEffectsRoutine()
    //{
    //    _agent.ResetPath();
    //    yield return new WaitForSeconds(2); /// FIX temporary magic number !
    //    _droneStateMachine.ChangeState(DroneState.FollowingState);
    //}

    //private void OnResourcesSpawned(Resource resource)
    //{
    //    if (_droneStateMachine.ActiveState != DroneState.StartState) return;

    //    if (NearestResource == null)
    //    {
    //        NearestResource = resource;
    //    }
    //    else
    //    {
    //        CompareDistances(resource);
    //    }
    //    _agent.SetDestination(NearestResource.transform.position);
    //    _droneStateMachine.ChangeState(DroneState.FollowingState);
    //}

    //private void CompareDistances(Resource resource)
    //{
    //    var distCurrent = Vector3.Distance(transform.position, NearestResource.transform.position);
    //    var distNew = Vector3.Distance(transform.position, resource.transform.position);

    //    if (distNew < distCurrent)
    //    {
    //        NearestResource = resource;
    //    }
    //}
}
