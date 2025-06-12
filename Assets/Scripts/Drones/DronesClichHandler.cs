using UnityEngine;

public class DronesClichHandler : MonoBehaviour
{
    [SerializeField] private DronView[] _drones;

    private void Start()
    {
        foreach (var drone in _drones)
        {
            drone.Click += OnClick;
        }
    }

    private void OnClick(DronView view)
    {
        foreach (var drone in _drones)
        {
            if (drone != view)
            {
                drone.Deactivate();
            }
        }
    }
}
