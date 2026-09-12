using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damage = 20;  // Startverdi, men vil endres i OnTriggerEnter

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                // Generer et tilfeldig skadetall mellom 1 og 20
                damage = Random.Range(10, 21);
                
                playerHealth.TakeDamage(damage);
                
                Debug.Log("Fienden påførte " + damage + " skade");
            }
        }
    }
}