using UnityEngine;
using TMPro;

public class CloseQuestDialogue : MonoBehaviour
{
    public GameObject dialogueBox;
    public AudioSource voiceSource;
    public NPCDialogueImage npcDialogueRef;

    public AudioSource questCompleteAudio;
    public GameObject questCompleteText;

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

       
        if (questCompleteAudio != null)
            questCompleteAudio.Play();

        
        if (questCompleteText != null)
            questCompleteText.SetActive(true);
    }
}
