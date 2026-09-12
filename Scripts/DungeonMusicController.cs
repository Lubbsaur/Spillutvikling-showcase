using UnityEngine;
using System.Collections;

public class DungeonMusicController : MonoBehaviour
{
    public AudioSource backgroundMusic;
    public AudioSource ambience;
    public AudioSource dungeonMusic;

    public float fadeTime = 2f;
    public float dungeonVolume = 0.4f;

    public void EnterDungeon()
    {
        StopAllCoroutines();

        
        if (backgroundMusic != null && backgroundMusic.isPlaying)
            StartCoroutine(FadeOut(backgroundMusic));

        if (ambience != null && ambience.isPlaying)
            StartCoroutine(FadeOut(ambience));

        
        if (dungeonMusic != null)
            StartCoroutine(FadeIn(dungeonMusic, dungeonVolume));
    }

    public void ExitDungeon()
    {
        StopAllCoroutines();

        if (dungeonMusic != null)
        {
            if (dungeonMusic.isPlaying)
                StartCoroutine(FadeOut(dungeonMusic));
            else
                dungeonMusic.Stop();
        }

        if (backgroundMusic != null)
            StartCoroutine(FadeIn(backgroundMusic, 0.5f));

        if (ambience != null)
            StartCoroutine(FadeIn(ambience, 0.3f));
    }

    private IEnumerator FadeOut(AudioSource source)
    {
        float startVolume = source.volume;
        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(startVolume, 0f, time / fadeTime);
            yield return null;
        }

        source.Stop();
        source.volume = startVolume; 
    }

    private IEnumerator FadeIn(AudioSource source, float targetVolume)
    {
        source.volume = 0f;
        source.Play();

        float time = 0f;

        while (time < fadeTime)
        {
            time += Time.deltaTime;
            source.volume = Mathf.Lerp(0f, targetVolume, time / fadeTime);
            yield return null;
        }

        source.volume = targetVolume;
    }
}




