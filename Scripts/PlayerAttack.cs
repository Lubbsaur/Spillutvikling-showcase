using System.Collections;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Animator attackAnimator;
    [SerializeField] private GameObject hitbox;
    [SerializeField] private AudioClip attackSound;
    private AudioSource audioSource;
    private GameObject player;
    private PlayerMovement input;

    private float lastAttackTime = 0f;
    [SerializeField] private float attackSoundCooldown = 0.5f;

    private bool canAttack = true;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        input = player.GetComponent<PlayerMovement>();
        input.OnAttackAction += InputOnAttackAction;
        audioSource = GetComponent<AudioSource>();
    }

    private void InputOnAttackAction()
    {
        if (!canAttack) return;

        StartCoroutine(EnableDisableHitbox());
    }

    IEnumerator EnableDisableHitbox()
    {
        if (attackSound != null && audioSource != null && Time.time - lastAttackTime >= attackSoundCooldown)
        {
            audioSource.PlayOneShot(attackSound, 4f);
            lastAttackTime = Time.time;
        }

        hitbox.SetActive(true);
        attackAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.3f);
        hitbox.SetActive(false);
        attackAnimator.SetBool("isAttacking", false);
    }

    private IEnumerator PlayAttackSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            if (enemy != null)
            {
                enemy.HitByPlayer(33);
            }
        }
    }
}