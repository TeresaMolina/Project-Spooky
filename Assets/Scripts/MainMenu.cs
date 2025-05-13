using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    // Remove the singleton pattern - let MenuManager handle UI
    public AudioSource backgroundMusic;

    void Start()
    {
        if (backgroundMusic != null && !backgroundMusic.isPlaying)
        {
            backgroundMusic.loop = true;
            backgroundMusic.Play();
        }
    }

    public void PlayGame()
    {
        if (backgroundMusic != null)
            backgroundMusic.Stop();
        
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
