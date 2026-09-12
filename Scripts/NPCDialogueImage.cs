using UnityEngine;

public class NPCDialogueImage : MonoBehaviour
{
    public AudioSource voiceSource;
    public AudioClip voiceClip;
    public GameObject dialogueImage;

    private bool isTalking = false;
    private bool hasSpoken = false;

    private void OnMouseDown()
    {
        if (isTalking) return;
        if (voiceSource == null || dialogueImage == null) return;


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.LockMovement();
        }


        if (!hasSpoken && voiceClip != null)
        {
            voiceSource.Stop();
            voiceSource.clip = voiceClip;
            voiceSource.Play();
            hasSpoken = true;
        }


        dialogueImage.SetActive(true);
        isTalking = true;
    }

    public void EndDialogue()
    {
        isTalking = false;
    }
}






