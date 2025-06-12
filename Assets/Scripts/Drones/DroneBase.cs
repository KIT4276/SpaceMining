using UnityEngine;

public class DroneBase : MonoBehaviour
{
    [SerializeField] private DronesCount _dronesCount;
    [SerializeField] private Drone[] _drones;

    private void Awake()
    {
        _dronesCount.ChangeCount += OnChangeCount;
    }

    private void OnChangeCount(float count)
    {
        for (int i = 0; i < _drones.Length; i++)
        {
            if (i < count)
            {
                _drones[i].gameObject.SetActive(true);
                if (_drones[i].DroneStateMachine.ActiveState != DroneState.StartState)
                {
                    _drones[i].FindNewDestination();
                }
            }
            else
            {
                _drones[i].gameObject.SetActive(false);
            }
        }
    }
}
