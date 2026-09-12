using UnityEngine;

public class DialogueCloseButton : MonoBehaviour
{
    public GameObject dialogueBox;           
    public AudioSource voiceSource;           
    public NPCDialogueImage npcDialogueRef;   

    public void CloseDialogue()
    {
        if (dialogueBox != null)
            dialogueBox.SetActive(false);

        if (voiceSource != null && voiceSource.isPlaying)
            voiceSource.Stop();


        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerMovement movement = player.GetComponent<PlayerMovement>();
            if (movement != null)
                movement.UnlockMovement();
        }


        if (npcDialogueRef != null)
            npcDialogueRef.EndDialogue();
    }
}



