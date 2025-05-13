using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EscapedUI : MonoBehaviour
{
    [Header("UI Elements")]
    public Image escapedImage; // Assign the "EscapedImage" UI element
    public float fadeDuration = 1f;
    public float displayTime = 2f;
    public string mainMenuScene = "MainMenu";

    void Start()
    {
        // Ensure image is hidden at start
        if (escapedImage != null)
        {
            escapedImage.gameObject.SetActive(false);
            Color c = escapedImage.color;
            c.a = 0f;
            escapedImage.color = c;
        }
    }

    public void ShowEscaped()
    {
        StartCoroutine(EscapeSequence());
    }

    IEnumerator EscapeSequence()
    {
        if (escapedImage != null)
        {
            escapedImage.gameObject.SetActive(true);
            
            // Fade in
            float elapsed = 0f;
            Color c = escapedImage.color;
            
            while (elapsed < fadeDuration)
            {
                c.a = Mathf.Lerp(0, 1, elapsed / fadeDuration);
                escapedImage.color = c;
                elapsed += Time.unscaledDeltaTime;
                yield return null;
            }
            c.a = 1;
            escapedImage.color = c;
        }

        yield return new WaitForSecondsRealtime(displayTime);
        SceneManager.LoadScene(mainMenuScene);
    }
}
