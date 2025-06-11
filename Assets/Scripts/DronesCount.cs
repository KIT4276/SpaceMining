using System;
using UnityEngine;
using UnityEngine.UI;

public class DronesCount : MonoBehaviour
{
    [SerializeField] private float _startCount = 3;
    [SerializeField] private Slider _slider;

    public event Action <float> ChangeCount;

    private void Start()
    {
        ChangeCount?.Invoke(_startCount);
        _slider.SetValueWithoutNotify(_startCount);
    }

    public void OnChangeCount()
    {
        ChangeCount?.Invoke(_slider.value);
    }
}
