using System;
using UnityEngine;

public class Resource : MonoBehaviour
{
    private ResourcesFactory _resourcesFactory;

    public bool IsOccupied {  get; private set; }

    public void Initialize(Vector2 position, ResourcesFactory resourcesFactory)
    {
        _resourcesFactory = resourcesFactory;

         var x = position.x;
        var z = position.y;
        
        transform.position = new Vector3(x, 0, z);

        IsOccupied = false;
    }

    public void Deactivate()
    {
        _resourcesFactory.Despawn(this);
    }

    public void TakeAResource()
    {
        IsOccupied = true;
    }
}
