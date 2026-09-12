using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] musicClips;

    private void Start()
    {
        PlayRandomMusic();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            PlayRandomMusic();
        }
    }

    void PlayRandomMusic()
    {
        if (musicClips.Length == 0) return;

        AudioClip chosen = musicClips[Random.Range(0, musicClips.Length)];
        audioSource.clip = chosen;
        audioSource.Play();
    }
}

