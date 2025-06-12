using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class DronView : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Material _normMaterial;
    [SerializeField] private Material _selectedMaterial;
    
    private Vector3 _startScale;
    public  bool IsSelected { get; private set; }

    public event Action<DronView> Click;

    private void Awake()
    {
        IsSelected = false;
        _startScale = transform.localScale;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!IsSelected)
        {
            this.transform.localScale *= 1.2f;
            IsSelected = true;
            Click?.Invoke(this);
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
        IsSelected = false;
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
