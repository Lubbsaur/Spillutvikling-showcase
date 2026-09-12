using UnityEngine;
using System.Collections;

public class FadeAndDisable : MonoBehaviour
{
    public float fadeDuration = 1f;     
    public float visibleTime = 2f;      

    private CanvasGroup canvasGroup;

    private void OnEnable()
    {
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.alpha = 1f;  
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(visibleTime);

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        canvasGroup.alpha = 0f;
        gameObject.SetActive(false);
    }
}

