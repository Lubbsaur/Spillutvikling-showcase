using UnityEngine;

public class AmbienceLooper : MonoBehaviour
{
    public AudioSource audioSource;
    public float volume = 0.3f;
    public float fadeTime = 3f;

    private void Start()
    {
        StartCoroutine(FadeIn());
        audioSource.Play();
    }

    private void Update()
    {
        if (!audioSource.isPlaying)
        {
            // Restart with fade-in
            StartCoroutine(RestartWithFade());
        }
    }

    private System.Collections.IEnumerator RestartWithFade()
    {
        yield return null; // wait one frame just to be safe
        audioSource.volume = 0f;
        audioSource.Play();
        yield return FadeIn();
    }

    private System.Collections.IEnumerator FadeIn()
    {
        float timer = 0f;
        while (timer < fadeTime)
        {
            timer += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(0f, volume, timer / fadeTime);
            yield return null;
        }
        audioSource.volume = volume;
    }
}

