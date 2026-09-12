using UnityEngine;

public class FinishGameButton : MonoBehaviour
{
    public void FinishGame()
    {
        Debug.Log("Avslutter spillet...");
        Application.Quit();
        
        // Merk at Application.Quit() fungerer bare når spillet er bygget og kjørt som en applikasjon.
    }
}