using UnityEngine;

public class FloatUpDown : MonoBehaviour
{
    public float amplitude = 0.2f;
    public float speed = 1.5f;

    private Vector3 startPos;

    void Start()
    {
        startPos = transform.localPosition;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * speed) * amplitude;
        transform.localPosition = startPos + new Vector3(0, offset, 0);
    }
}

