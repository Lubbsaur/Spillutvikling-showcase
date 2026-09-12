using UnityEngine;

public class LookAtPlayer : MonoBehaviour
{
    public Transform player;
    public float lookRadius = 5f;
    public float rotationSpeed = 5f;

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= lookRadius)
        {
            Vector3 direction = player.position - transform.position;
            direction.y = 0; // keep rotation horizontal
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}

