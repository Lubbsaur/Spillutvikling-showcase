using UnityEngine;

public class NPCVoiceLine : MonoBehaviour
{
    public AudioSource voiceSource;
    public AudioClip[] voiceLines; 

    private void OnMouseDown()
    {
        if (voiceSource == null || voiceLines.Length == 0) return;

        if (voiceSource.isPlaying)
        {
            voiceSource.Stop();
        }

        // Pick a random clip
        int randomIndex = Random.Range(0, voiceLines.Length);
        voiceSource.clip = voiceLines[randomIndex];
        voiceSource.Play();
    }
}


