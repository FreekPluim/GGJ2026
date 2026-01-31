using UnityEngine;

public class ItemBobber : MonoBehaviour
{
    [SerializeField] private float maxOffset;
    [SerializeField] private float bobSpeed;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        float yOffset = Mathf.Sin(bobSpeed * Time.time) * maxOffset;
        Vector3 newPosition = startPosition + Vector3.up * yOffset;

        transform.position = newPosition;
    }
}
