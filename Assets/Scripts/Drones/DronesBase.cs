using UnityEngine;

public class DronesBase : MonoBehaviour
{
    [SerializeField] private DronesCount _dronesCount;
    [SerializeField] private DroneInstaller[] _drones;

    public DroneInstaller[] Drones { get => _drones; }

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
                if (!_drones[i].gameObject.activeInHierarchy)
                {
                    _drones[i].gameObject.SetActive(true);
                    _drones[i].DroneMovement.StartPosition();
                    _drones[i].DroneStateMachine.ChangeState(DroneState.Following);
                }
            }
            else
            {
                _drones[i].DroneMovement.StopMove();
                _drones[i].gameObject.SetActive(false);
            }
        }
    }

    private void OnDestroy()
    {
        _dronesCount.ChangeCount -= OnChangeCount;
    }
}
