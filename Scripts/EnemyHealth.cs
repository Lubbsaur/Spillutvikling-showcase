using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int health = 100;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AttemptToHitEnemy();
        }
    }

    void AttemptToHitEnemy()
    {

        Debug.Log("Enemy health: " + health);

        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}