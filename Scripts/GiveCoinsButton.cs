using UnityEngine;

public class GiveCoinsButton : MonoBehaviour
{
    public CoinCollector coinCollector;
    public int cost = 50;

    public GameObject nextDialogue;         
    public AudioSource voiceSource;         
    public AudioClip newVoiceLine;          

    private bool hasGiven = false;

    public void GiveCoins()
    {
        if (hasGiven || coinCollector == null) return;

        if (coinCollector.coinCount >= cost)
        {
            coinCollector.coinCount -= cost;
            coinCollector.UpdateCoinUI();
            hasGiven = true;

            Debug.Log("Du ga myntene");


            if (voiceSource != null && newVoiceLine != null)
            {
                voiceSource.Stop();
                voiceSource.clip = newVoiceLine;
                voiceSource.Play();
            }


            if (nextDialogue != null)
                nextDialogue.SetActive(true);
        }
        else
        {
            Debug.Log("Ikke nok mynter");
        }
    }
}