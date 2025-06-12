using UnityEngine;

public class Resource : MonoBehaviour
{
    private ResourcesFactory _resourcesFactory;

    public void Initialize(Vector2 position, ResourcesFactory resourcesFactory)
    {
        _resourcesFactory = resourcesFactory;

        var x = position.x;
        var z = position.y;

        transform.position = new Vector3(x, 0, z);
    }

    public void Deactivate()
    {
        _resourcesFactory.Despawn(this);
    }
}
