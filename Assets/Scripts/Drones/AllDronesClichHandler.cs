using System;
using UnityEngine;

public class AllDronesClichHandler : MonoBehaviour
{
    [SerializeField] private DronClichHandler[] _drones;

    public event Action<DroneInstaller> Click;

    public DroneInstaller SelectedDron {  get; private set; } 

    private void Start()
    {
        foreach (var drone in _drones)
        {
            drone.Click += OnClick;
        }
    }

    private void OnClick(DroneInstaller droneInstaller)
    {
        SelectedDron = droneInstaller;

        Click?.Invoke(droneInstaller);
        foreach (var drone in _drones)
        {
            if (drone != droneInstaller.DronClichHandler)
            {
                drone.Deactivate();
                if (SelectedDron = drone.DroneInstaller)
                {
                    SelectedDron = null;
                }
            }
        }
    }

    private void OnDestroy()
    {
        foreach (var drone in _drones)
        {
            drone.Click -= OnClick;
        }
    }
}
