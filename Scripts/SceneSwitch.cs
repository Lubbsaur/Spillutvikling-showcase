using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void LoadScene(string Main)
    {
        SceneManager.LoadScene(Main);
    }
}