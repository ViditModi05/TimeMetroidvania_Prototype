using UnityEngine;

public class Test : MonoBehaviour
{
    [Header("Movement Settings")]
    public Transform pointA;
    public Transform pointB;
    public float speed = 2f;

    private Vector3 target;

    private void Start()
    {
        target = pointB.position;
    }

    private void Update()
    {
        float dt = TimeManager.Instance.WorldDeltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target, speed * dt);

        // Switch direction
        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
}
