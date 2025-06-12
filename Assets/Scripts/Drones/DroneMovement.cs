using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
    [SerializeField] private NavMeshAgent _agent;
    [SerializeField] private DroneBase _base;
    [SerializeField] private DroneStateMachine _droneStateMachine;
    [SerializeField] private ResourcesFactory _resourcesFactory;

    public Resource NearestResource { get; private set; }

    private void Start()
    {
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
        _agent.ResetPath();
    }

    private void OnResourcesSpawned(Resource resource)
    {
        if (_droneStateMachine.ActiveState != DroneState.StartState) return;

        if (NearestResource == null)
        {
            NearestResource = resource;
        }
        else
        {
            SelectNearest(resource);
        }
        _agent.SetDestination(NearestResource.transform.position);
        _droneStateMachine.ChangeState(DroneState.FollowingState);
    }

    private void OnStateChanged(DroneState state)
    {
        switch (state)
        {
            case DroneState.ReturnState:
                _agent.SetDestination(_base.transform.position);
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
        _agent.SetDestination(NearestResource.transform.position);
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
