using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float chaseDistance = 5f;
    public float forgetDistance = 8f;
    public float patrolRange = 10f;
    public float attackDistance = 1.5f;
    public int maxHealth = 100;

    public float attackCooldown = 2f;
    private float lastAttackTime = -999f;

    public GameObject attackHitbox;

    public AudioClip[] detectedSounds;
    public AudioClip[] hitSounds;
    public AudioClip[] attackSounds;

    public SkinnedMeshRenderer bodyRenderer;

    private AudioSource audioSource;
    private int currentHealth;
    private NavMeshAgent agent;
    private Animator animator;
    private Vector3 startPosition;
    private bool chasing = false;
    private bool isDead = false;
    private bool isAttacking = false;
    private bool hasPlayedSound = false;

    private Color originalColor;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        startPosition = transform.position;
        currentHealth = maxHealth;

        if (attackHitbox != null)
            attackHitbox.SetActive(false);

        if (bodyRenderer != null)
            originalColor = bodyRenderer.material.color;

        SetNewPatrolPoint();
        StartCoroutine(AttackMonitor());
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        float distanceFromStart = Vector3.Distance(transform.position, startPosition);

        if (distanceFromStart > patrolRange * 1f)
        {
            chasing = false;
            isAttacking = false;
            animator.SetBool("isAttacking", false);
            if (attackHitbox != null)
                attackHitbox.SetActive(false);
            agent.isStopped = false;
            agent.SetDestination(startPosition);
            return;
        }

        if (chasing)
        {
            if (distanceToPlayer > forgetDistance)
            {
                chasing = false;
                hasPlayedSound = false;
                isAttacking = false;
                animator.SetBool("isAttacking", false);
                if (attackHitbox != null)
                    attackHitbox.SetActive(false);
                agent.isStopped = false;
                SetNewPatrolPoint();
            }
            else if (distanceToPlayer <= attackDistance)
            {
                if (!isAttacking && Time.time > lastAttackTime + attackCooldown)
                {
                    isAttacking = true;
                    animator.SetBool("isAttacking", true);
                    lastAttackTime = Time.time;
                    agent.isStopped = true;

                    if (attackHitbox != null)
                        attackHitbox.SetActive(true);

                    if (attackSounds.Length > 0 && audioSource != null)
                    {
                        AudioClip attackClip = attackSounds[Random.Range(0, attackSounds.Length)];
                        StartCoroutine(PlayAttackSoundWithDelay(attackClip, 0.3f));
                    }
                }

                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0f;
                if (direction != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(direction);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }
            }
            else
            {
                if (isAttacking)
                {
                    isAttacking = false;
                    animator.SetBool("isAttacking", false);
                    if (attackHitbox != null)
                        attackHitbox.SetActive(false);
                }

                agent.isStopped = false;
                agent.SetDestination(player.position);

                Vector3 chaseDir = (player.position - transform.position).normalized;
                chaseDir.y = 0f;
                if (chaseDir != Vector3.zero)
                {
                    Quaternion lookRotation = Quaternion.LookRotation(chaseDir);
                    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
                }
            }
        }
        else
        {
            if (distanceToPlayer < chaseDistance)
            {
                chasing = true;
                agent.SetDestination(player.position);

                if (!hasPlayedSound && detectedSounds.Length > 0)
                {
                    AudioClip chosen = detectedSounds[Random.Range(0, detectedSounds.Length)];
                    audioSource.PlayOneShot(chosen);
                    hasPlayedSound = true;
                }
            }

            if (!agent.pathPending && agent.remainingDistance < 0.5f)
            {
                SetNewPatrolPoint();
            }
        }

        animator.SetTrigger("isChasing");
    }

    void SetNewPatrolPoint()
    {
        for (int i = 0; i < 10; i++) 
        {
            Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
            randomDirection += startPosition;
            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomDirection, out hit, patrolRange, NavMesh.AllAreas))
            {
                agent.SetDestination(hit.position);
                return;
            }
        }


        agent.SetDestination(startPosition);
    }

    public void HitByPlayer(int damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log("Enemy HP: " + currentHealth);

        if (hitSounds.Length > 0 && audioSource != null)
        {
            AudioClip chosen = hitSounds[Random.Range(0, hitSounds.Length)];
            StartCoroutine(PlayHitSoundWithDelay(chosen, 0.5f));
        }

        if (bodyRenderer != null)
            StartCoroutine(BlinkRed());

        if (currentHealth <= 0)
        {
            isDead = true;
            agent.enabled = false;
            chasing = false;
            isAttacking = false;

            animator.SetBool("isAttacking", false);
            animator.SetTrigger("isDead");

            if (attackHitbox != null)
                attackHitbox.SetActive(false);

            Collider col = GetComponent<Collider>();
            if (col != null)
                col.enabled = false;

            gameObject.tag = "Untagged";

            StartCoroutine(DespawnAfterDelay(2f));
        }
    }

    private IEnumerator PlayHitSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    private IEnumerator PlayAttackSoundWithDelay(AudioClip clip, float delay)
    {
        yield return new WaitForSeconds(delay);
        audioSource.PlayOneShot(clip);
    }

    private IEnumerator BlinkRed()
    {
        bodyRenderer.material.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        bodyRenderer.material.color = originalColor;
    }

    private IEnumerator AttackMonitor()
    {
        while (true)
        {
            if (isAttacking && Time.time > lastAttackTime + 0.5f)
            {
                isAttacking = false;
                animator.SetBool("isAttacking", false);

                if (attackHitbox != null)
                    attackHitbox.SetActive(false);

                agent.isStopped = false;
            }

            yield return null;
        }
    }

    private IEnumerator DespawnAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isDead) return;

        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(20);
            }
        }
    }
}













