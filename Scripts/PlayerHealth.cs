using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    public Slider healthBar; // slider brukes til healthbar
    public AudioClip damageSound;  // lydfil for skade

    public AudioSource audioSource;
    public AudioClip[] hurtClips;
    public AudioClip deathClip;

    public GameObject deathScreen;
    public Transform spawnPoint;

    public DungeonMusicController musicController;
    public HitFlash hitFlash; 

    private bool isDead = false;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();

        currentHealth = maxHealth;
        healthBar.maxValue = maxHealth;
        healthBar.value = currentHealth;

        if (deathScreen != null)
            deathScreen.SetActive(false);

        Time.timeScale = 1f;
    }

    public void TakeDamage(int damageAmount)
    {
        if (isDead) return;

        currentHealth -= damageAmount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;

        if (hurtClips.Length > 0 && audioSource != null)
        {
            AudioClip chosen = hurtClips[Random.Range(0, hurtClips.Length)];
            audioSource.PlayOneShot(chosen);
        }

        if (hitFlash != null)
            hitFlash.Flash(); 

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        healthBar.value = currentHealth;
    }

    void Die()
    {
        isDead = true;

        if (deathClip != null && audioSource != null)
            audioSource.PlayOneShot(deathClip);

        if (deathScreen != null)
            deathScreen.SetActive(true);

        if (agent != null)
        {
            agent.ResetPath();
            agent.isStopped = true;
        }

        if (musicController != null)
        {
            musicController.ExitDungeon();
        }

        Time.timeScale = 0f;
    }

    public void RestartAfterDeath()
    {
        isDead = false;
        currentHealth = maxHealth;
        healthBar.value = currentHealth;

        if (deathScreen != null)
            deathScreen.SetActive(false);

        if (agent != null)
        {
            agent.enabled = false;
            transform.position = spawnPoint.position;
            transform.rotation = spawnPoint.rotation;
            agent.enabled = true;
            agent.ResetPath();
            agent.isStopped = false;
        }

        Time.timeScale = 1f;
    }
}








