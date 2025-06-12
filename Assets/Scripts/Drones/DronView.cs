using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DronView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Material _normMaterial;
    [SerializeField] private Material _selectedMaterial;
    
    private bool _isSelected = false;
    private Vector3 _startScale;

    public event Action<DronView> Click;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!_isSelected)
        {
            Click?.Invoke(this);
            this.transform.localScale *= 1.2f;
            _isSelected = true;
            ChangeMaterial(_selectedMaterial);
        }
        else
        {
            Deactivate();
        }
    }

    public void Deactivate()
    {
        this.transform.localScale = _startScale;
        _isSelected = false;
        ChangeMaterial(_normMaterial);
    }

    private void ChangeMaterial(Material material)
    {
        foreach (var renderer in _meshRenderers)
        {
            renderer.material = material;
        }
    }
}
