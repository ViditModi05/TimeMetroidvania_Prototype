using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float rotateSpeed;
    private void Update()
    {
        transform.Rotate(0, 0, 1 * rotateSpeed * Time.deltaTime);
    }
}
