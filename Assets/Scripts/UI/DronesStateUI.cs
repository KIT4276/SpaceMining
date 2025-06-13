using TMPro;
using UnityEngine;

public class DronesStateUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _state;
    [SerializeField] private AllDronesClichHandler _dronesClichHandler;

    private DroneInstaller _selectedDron;

    private void Start()
    {
        _dronesClichHandler.Click += OnClick;
    }

    private void Update()
    {
        if (_selectedDron == null)
        {
            CleanText();
        }
        else if (!_selectedDron.gameObject.activeInHierarchy)
        {
            CleanText();
            
            _selectedDron = null;
        }
    }

    private void OnClick(DroneInstaller dron)
    {
        if (dron.DronClichHandler.IsSelected)
        {
            _selectedDron = dron;
            DroneStateMachine stateMachinne = dron.GetComponent<DroneInstaller>().DroneStateMachine;
            UpdateText(dron.gameObject.name, stateMachinne.ActiveState.ToString());

            stateMachinne.StateChanged += UpdateState;
        }
        else
        {
            _selectedDron = null;
            _name.text = string.Empty;
            _state.text = string.Empty;
        }
    }

    private void UpdateState(DroneState state)
    {
        if (_selectedDron != null)
        {
            UpdateText(_selectedDron.gameObject.name, state.ToString());
        }
    }

    private void UpdateText(string name, string state)
    {
        _name.text = name;
        _state.text = state;
    }

    private void CleanText()
    {
        _name.text = string.Empty;
        _state.text = string.Empty;
    }

    private void OnDestroy()
    {
        _dronesClichHandler.Click -= OnClick;
    }
}
