using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DroneMovement), typeof(DroneMining), typeof(NavMeshAgent))]
[RequireComponent( typeof(Rigidbody), typeof(Collider))]
public class DroneInstaller : MonoBehaviour
{
    [SerializeField] private ResourcesFactory _resourcesFactory;
    [SerializeField] private DronesBase _droneBase;

    public DroneMovement DroneMovement { get; private set; }
    public DroneMining DroneMining { get; private set; }
    public DroneStateMachine DroneStateMachine { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }

    public ResourcesFactory ResourcesFactory { get => _resourcesFactory; }
    public DronesBase DroneBase { get => _droneBase; }

    public event Action Initialized;

    private void Awake()
    {
        DroneMovement = GetComponent<DroneMovement>();
        DroneMining = GetComponent<DroneMining>();
        NavMeshAgent = GetComponent<NavMeshAgent>();

        DroneStateMachine = new DroneStateMachine();
    }

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        DroneMovement.Initialize(NavMeshAgent, DroneBase, DroneStateMachine, ResourcesFactory);
        DroneMining.Initialize(DroneStateMachine, DroneMovement, DroneBase);
        Initialized?.Invoke();
    }
}
