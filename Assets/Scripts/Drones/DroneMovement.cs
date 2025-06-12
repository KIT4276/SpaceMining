using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
   private NavMeshAgent _navMeshAgent;
   private DroneBase _base;
   private DroneStateMachine _droneStateMachine;
  private ResourcesFactory _resourcesFactory;

    public Resource NearestResource { get; private set; }

    public void Initialize(NavMeshAgent navMeshAgent, DroneBase droneBase, DroneStateMachine droneStateMachine, 
        ResourcesFactory resourcesFactory)
    {
        _navMeshAgent = navMeshAgent;
        _base = droneBase;
        _droneStateMachine = droneStateMachine;
        _resourcesFactory = resourcesFactory;

        _resourcesFactory.Spawned += OnResourcesSpawned;
        _droneStateMachine.StateChanged += OnStateChanged;
    }

    private void Update()
    {
        if (_droneStateMachine.ActiveState == DroneState.FollowingState)
        {
            if (NearestResource == null || !NearestResource.gameObject.activeInHierarchy)
            {
                NearestResource = null;
                StopMove();
                FindNewDestination();
            }
        }
    }

    public void StopMove()
    {
        _navMeshAgent.ResetPath();
    }

    private void OnResourcesSpawned(Resource resource)
    {
        if (_droneStateMachine.ActiveState != DroneState.StartState || !this.gameObject.activeInHierarchy) return;

        if (NearestResource == null)
        {
            NearestResource = resource;
        }
        else
        {
            SelectNearest(resource);
        }
        _navMeshAgent.SetDestination(NearestResource.transform.position);
        _droneStateMachine.ChangeState(DroneState.FollowingState);
    }

    private void OnStateChanged(DroneState state)
    {
        switch (state)
        {
            case DroneState.ReturnState:
                _navMeshAgent.SetDestination(_base.transform.position);
                break;
            case DroneState.FollowingState:
                FindNewDestination();
                break;

        }
    }

    public void FindNewDestination()
    {
        List< Resource> resources = _resourcesFactory.ActiveResourcesPool;

        for (int i = 0; i< resources.Count; i++)
        {
            if (resources[i] != NearestResource)
            {
                if (NearestResource == null)
                {
                    NearestResource = resources[i];
                }
                else
                {
                    SelectNearest(resources[i]);
                }
            }
        }
        _navMeshAgent.SetDestination(NearestResource.transform.position);
    }

    private void SelectNearest(Resource resource)
    {
        var distCurrent = Vector3.Distance(transform.position, NearestResource.transform.position);
        var distNew = Vector3.Distance(transform.position, resource.transform.position);

        if (distNew < distCurrent)
        {
            NearestResource = resource;
        }
    }
}
