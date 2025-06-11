using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourcesFactory : MonoBehaviour
{
    [SerializeField] private float _spawnTime = 3;
    [SerializeField] private Vector2 _x_Limits = new Vector2(-5, 5);
    [SerializeField] private Vector2 _z_Limits = new Vector2(-5, 5);

    public List<Resource> InactiveResourcesPool { get; private set; }
    public List<Resource> ActiveResourcesPool { get; private set; }

    public event Action<Resource> Spawned;

    private void Start()
    {
        InactiveResourcesPool = new();
        ActiveResourcesPool = new();
        StartCoroutine(SpawnResourcesRoutine());
    }

    private IEnumerator SpawnResourcesRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnTime);
            SpawnResource();
        }
    }

    private void SpawnResource()
    {
        Resource resource;

        if (InactiveResourcesPool.Count == 0)
        {
            GameObject obj = Instantiate(UnityEngine.Resources.Load("Prefabs/Resource")) as GameObject;
            resource = obj.GetComponent<Resource>();
        }
        else
        {
            resource = InactiveResourcesPool[0];
            InactiveResourcesPool.Remove(resource);
        }
        resource.Initialize(GetRandomPosition(), this);
        resource.gameObject.SetActive(true);
        ActiveResourcesPool.Add(resource);

        Spawned?.Invoke(resource);
    }

    private Vector2 GetRandomPosition()
    {
        float x = UnityEngine.Random.Range(_x_Limits.x, _x_Limits.y);
        float y = UnityEngine.Random.Range(_z_Limits.x, _z_Limits.y);

        return new Vector2(x, y);
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void Despawn(Resource resource)
    {
        ActiveResourcesPool.Remove(resource);
        InactiveResourcesPool.Add(resource);

        resource.gameObject.SetActive(false);
    }
}
