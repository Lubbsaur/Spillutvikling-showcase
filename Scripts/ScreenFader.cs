using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;             
    public float fadeDuration = 0.3f;    
    public float blackScreenTime = 1.0f;    

    public IEnumerator FadeOutIn(System.Action onFadeComplete)
    {

        yield return StartCoroutine(FadeTo(1));


        onFadeComplete?.Invoke();

        yield return new WaitForSeconds(blackScreenTime);

        yield return StartCoroutine(FadeTo(0));
    }

    private IEnumerator FadeTo(float targetAlpha)
    {
        float startAlpha = fadeImage.color.a;
        float time = 0f;

        while (time < fadeDuration)
        {
            float alpha = Mathf.Lerp(startAlpha, targetAlpha, time / fadeDuration);
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            time += Time.deltaTime;
            yield return null;
        }

        fadeImage.color = new Color(0f, 0f, 0f, targetAlpha);
    }
}





