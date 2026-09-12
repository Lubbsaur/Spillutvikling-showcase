using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    // Referanse til Player, så vi får tak i boolen vi setter der
    [SerializeField] private PlayerMovement player;

    // Referanse til Animator komponenten vår
    private Animator animator;

    // Pek på riktig Animator
    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Hvis isRunning fra Player er true
        if (player.IsWalking())
        {
            // Sett isRunning parameteret til true
            animator.SetBool("isWalking", true);
        }
        // Hvis ikke
        else
        {
            // Sett isRunning parameteret til false
            animator.SetBool("isWalking", false);
        }
        
        
        
    }
}
