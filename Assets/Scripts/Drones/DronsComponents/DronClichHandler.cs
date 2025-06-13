using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DronClichHandler : MonoBehaviour, IPointerClickHandler
{
    private DronView _dronView;
         
    public DroneInstaller DroneInstaller { get; private set; }  

    public event Action<DroneInstaller> Click;

    public bool IsSelected { get; private set; }

    private void Awake()
    {
        IsSelected = false;
    }

    public void Initialize(DronView dronView, DroneInstaller droneInstaller)
    {
        _dronView = dronView;
        DroneInstaller = droneInstaller;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            IsSelected = true;
            _dronView.Activate();
            Click?.Invoke(DroneInstaller);
        }
        else
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        IsSelected = false;
        _dronView.Deactivate();
    }
}
