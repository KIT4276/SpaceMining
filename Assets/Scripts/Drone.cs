using System;
using UnityEngine;
using UnityEngine.AI;

public class Drone : MonoBehaviour
{
    [SerializeField] private ResourcesFactory _resourcesFactory;
    [SerializeField] private NavMeshAgent _agent;

    private Resource _nearestResource;

    private void Start()
    {
        _resourcesFactory.Spawned += OnResourcesSpawned;
    }

    private void OnResourcesSpawned(Resource resource)
    {
        if (_nearestResource == null)
        {
            _agent.SetDestination(resource.transform.position);
        }
    }
}
