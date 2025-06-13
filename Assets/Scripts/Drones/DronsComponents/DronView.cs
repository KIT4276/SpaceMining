using UnityEngine;

public class DronView : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _meshRenderers;
    [SerializeField] private Material _normMaterial;
    [SerializeField] private Material _selectedMaterial;
    [SerializeField] private float _selectedScale = 1.2f;

    private Vector3 _startScale;

    private void Awake()
    {
        _startScale = transform.localScale;
    }

    private void OnEnable()
    {
        Deactivate();
    }

    public void Activate()
    {
        this.transform.localScale *= _selectedScale;
        ChangeMaterial(_selectedMaterial);
    }

    public void Deactivate()
    {
        this.transform.localScale = _startScale;
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
