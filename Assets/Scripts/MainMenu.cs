using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        //scene game
        SceneManager.LoadScene("Game");
    }

    public void QuitGame()
    {
        //quit game
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
