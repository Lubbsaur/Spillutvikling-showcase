using UnityEngine;

public class FootstepPlayer : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footstepClips;
    public float stepInterval = 0.5f;
    private float stepTimer = 0f;

    void Update()
    {
        var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        if (agent != null && agent.velocity.magnitude > 0.1f)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                PlayFootstep();
                stepTimer = stepInterval;
            }
        }
        else
        {
            stepTimer = stepInterval;
        }
    }

    void PlayFootstep()
    {
        if (footstepClips.Length > 0 && audioSource != null)
        {
            AudioClip clip = footstepClips[Random.Range(0, footstepClips.Length)];
            audioSource.PlayOneShot(clip);
        }
    }
}

