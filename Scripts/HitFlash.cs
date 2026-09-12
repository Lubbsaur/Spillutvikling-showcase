using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HitFlash : MonoBehaviour
{
    public Image flashImage;
    public float flashDuration = 0.2f;
    public Color flashColor = new Color(1, 0, 0, 0.1f);

    void Awake()
    {
        if (flashImage != null)
            flashImage.color = new Color(1, 0, 0, 0); 
    }

    public void Flash()
    {
        if (flashImage != null)
            StartCoroutine(FlashRoutine());
    }

    private IEnumerator FlashRoutine()
    {
        flashImage.color = flashColor;

        float timer = 0f;
        Color startColor = flashColor;
        Color endColor = new Color(flashColor.r, flashColor.g, flashColor.b, 0f);

        while (timer < flashDuration)
        {
            timer += Time.deltaTime;
            flashImage.color = Color.Lerp(startColor, endColor, timer / flashDuration);
            yield return null;
        }

        flashImage.color = endColor;
    }
}

