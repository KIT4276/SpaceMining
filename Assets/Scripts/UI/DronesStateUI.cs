using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class DronesStateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _state;
    [SerializeField] private DronesClichHandler _dronesClichHandler;

    private DronView _view;

    private void Start()
    {
        _dronesClichHandler.Click += OnClick;
    }

    private void OnClick(DronView view)
    {
        if (view.IsSelected)
        {
            _view = view;
            DroneStateMachine stateMachinne = view.GetComponent<DroneInstaller>().DroneStateMachine;
            UpdateText(view.gameObject.name, stateMachinne.ActiveState.ToString());

            stateMachinne.StateChanged += UpdateState;
        }
        else
        {
            _view = null;
            _name.text = string.Empty;
            _state.text = string.Empty;
        }
    }

    private void UpdateState(DroneState state)
    {
        UpdateText(_view.gameObject.name, state.ToString());// как теперь отписаться?..
    }

    private void UpdateText(string name, string state)
    {
        _name.text = name;
        _state.text = state;
    }

    private void OnDestroy()
    {
        _dronesClichHandler.Click -= OnClick;
    }
}
