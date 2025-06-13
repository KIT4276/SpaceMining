using UnityEngine;

public class DronesConductor : MonoBehaviour
{
    [SerializeField] private DronesBase _dronesBase;
    [SerializeField] private DronesCount _dronesCount;

    private int _count = 0;

    private void Start()
    {
        foreach (var drone in _dronesBase.Drones)
        {
            drone.Initialized += OnDronInitialized;
        }
    }

    private void OnDronInitialized()
    {
        _count++;
        if (_count == _dronesBase.Drones.Length)
        {
            _dronesCount.Initialize();
        }
    }

    private void OnDestroy()
    {
        foreach (var drone in _dronesBase.Drones)
        {
            drone.Initialized -= OnDronInitialized;
        }
    }
}
