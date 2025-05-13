using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenuPanel;
    public GameObject settingsPanel;

    void Start()
    {
        ShowMainMenu();
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public void ShowSettings()
    {
        mainMenuPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }
}
