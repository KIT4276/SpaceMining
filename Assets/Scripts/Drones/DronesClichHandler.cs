using System;
using UnityEngine;

public class DronesClichHandler : MonoBehaviour
{
    [SerializeField] private DronView[] _drones;

    public event Action<DronView> Click;

    public DronView SelectedDron {  get; private set; } 

    private void Start()
    {
        foreach (var drone in _drones)
        {
            drone.Click += OnClick;
        }
    }

    private void OnClick(DronView view)
    {
        SelectedDron = view;

        Click?.Invoke(view);
        foreach (var drone in _drones)
        {
            if (drone != view)
            {
                drone.Deactivate();
                if (SelectedDron = drone)
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
