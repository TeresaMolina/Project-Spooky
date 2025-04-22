using UnityEngine;
using UnityEngine.Video;

public class JumpscareManager : MonoBehaviour
{
    public GameObject jumpscareCanvas;
    public VideoPlayer videoPlayer;

    void Start()
    {
        jumpscareCanvas.SetActive(false); // Hide canvas at start
    }

    public void TriggerJumpscare()
    {
        jumpscareCanvas.SetActive(true);
        videoPlayer.Play();
        Time.timeScale = 0f; // Freeze the game if you want (delete if not needed)
    }
}
