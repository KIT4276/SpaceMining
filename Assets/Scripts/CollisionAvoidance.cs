using UnityEngine;

public class CollisionAvoidance : MonoBehaviour
{
    [SerializeField] private Drone _drone;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
        if (collision.gameObject.TryGetComponent<CollisionAvoidance>(out var dron))
        {
            Debug.Log("dron");
            _drone.FindNewDestination();
        }
    }
}
