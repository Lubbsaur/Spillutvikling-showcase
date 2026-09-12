using UnityEngine;
using TMPro;

public class CoinCollector : MonoBehaviour
{
    public int coinCount = 0;
    public TextMeshProUGUI coinText;
    public AudioSource audioSource;
    public AudioClip coin1;
    public AudioClip coin2;

    public PlayerHealth playerHealth;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Coin"))
        {
            coinCount++;
            UpdateCoinUI();

            if (playerHealth != null)
            {
                playerHealth.Heal(10);
            }

            AudioClip chosenSound = Random.value < 0.5f ? coin1 : coin2;
            if (chosenSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(chosenSound);
            }

            Destroy(other.gameObject);
        }
    }

    public void UpdateCoinUI()
    {
        if (coinText != null)
            coinText.text = "Coins " + coinCount;
    }
}






