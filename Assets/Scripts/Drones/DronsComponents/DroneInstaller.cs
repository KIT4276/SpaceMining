using System;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(DroneMovement), typeof(DroneMining), typeof(NavMeshAgent))]
[RequireComponent( typeof(Rigidbody), typeof(Collider), typeof (DronClichHandler))]
[RequireComponent(typeof(DronView))]
public class DroneInstaller : MonoBehaviour
{
    [SerializeField] private ResourcesFactory _resourcesFactory;
    [SerializeField] private DronesBase _droneBase;

    public DroneMovement DroneMovement { get; private set; }
    public DroneMining DroneMining { get; private set; }
    public DroneStateMachine DroneStateMachine { get; private set; }
    public NavMeshAgent NavMeshAgent { get; private set; }
    public DronClichHandler DronClichHandler { get; private set; }
    public DronView DronView { get; private set; }

    public ResourcesFactory ResourcesFactory { get => _resourcesFactory; }
    public DronesBase DroneBase { get => _droneBase; }

    public event Action Initialized;

    private void Awake()
    {
        DroneMovement = GetComponent<DroneMovement>();
        DroneMining = GetComponent<DroneMining>();
        NavMeshAgent = GetComponent<NavMeshAgent>();
        DronClichHandler = GetComponent<DronClichHandler>();
        DronView = GetComponent<DronView>();

        DroneStateMachine = new DroneStateMachine(this.name);
    }

    private void Start()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        DroneMovement.Initialize(NavMeshAgent, DroneBase, DroneStateMachine, ResourcesFactory);
        DroneMining.Initialize(DroneStateMachine, DroneMovement, DroneBase);
        DronClichHandler.Initialize(DronView,this);
        Initialized?.Invoke();
    }
}
