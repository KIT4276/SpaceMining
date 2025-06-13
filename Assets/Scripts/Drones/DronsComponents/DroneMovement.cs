using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DroneMovement : MonoBehaviour
{
    private NavMeshAgent _navMeshAgent;
    private DronesBase _base;
    private DroneStateMachine _droneStateMachine;
    private ResourcesFactory _resourcesFactory;
    private Vector3 _startPosition;

    public Resource NearestResource { get; private set; }

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Initialize(NavMeshAgent navMeshAgent, DronesBase droneBase, DroneStateMachine droneStateMachine,
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
        if (_droneStateMachine.ActiveState == DroneState.Following)
        {
            if (NearestResource == null || !NearestResource.gameObject.activeInHierarchy)
            {
                NearestResource = null;
                StopMove();
                FindNewDestination();
            }
        }
    }

    public void StartPosition()
    {
        transform.position = _startPosition;
    }

    public void StopMove()
    {
        if (_navMeshAgent.isOnNavMesh)
        {
            _navMeshAgent.ResetPath();
        }
    }

    private void OnResourcesSpawned(Resource resource)
    {
        if (_droneStateMachine.ActiveState != DroneState.Start || !this.gameObject.activeInHierarchy) return;

        if (NearestResource == null)
        {
            NearestResource = resource;
        }
        else
        {
            SelectNearest(resource);
        }
        _navMeshAgent.SetDestination(NearestResource.transform.position);
        _droneStateMachine.ChangeState(DroneState.Following);
    }

    private void OnStateChanged(DroneState state)
    {
        switch (state)
        {
            case DroneState.Return:
                _navMeshAgent.SetDestination(_base.transform.position);
                break;
            case DroneState.Following:
                FindNewDestination();
                break;
            case DroneState.Mining:
                StopMove();
                break;

        }
    }

    public void FindNewDestination()
    {
        List<Resource> resources = _resourcesFactory.ActiveResourcesPool;

        for (int i = 0; i < resources.Count; i++)
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

        if (NearestResource != null)
        {
            _navMeshAgent.SetDestination(NearestResource.transform.position);
        }
        else
        {
            StartCoroutine(WaitingForAResource());
        }
        
    }

    private IEnumerator WaitingForAResource()
    {
        while(NearestResource == null)
        {
            yield return null;
        }
        FindNewDestination();
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

    private void OnDestroy()
    {
        _resourcesFactory.Spawned -= OnResourcesSpawned;
        _droneStateMachine.StateChanged -= OnStateChanged;
    }
}
