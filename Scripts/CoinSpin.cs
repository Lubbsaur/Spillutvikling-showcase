using UnityEngine;

public class CoinSpin : MonoBehaviour
{
    public float rotationSpeed = 90f;
    public float floatSpeed = 1f;
    public float floatHeight = 0.25f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }
}

