using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMusicManager : MonoBehaviour
{
    public static GameMusicManager Instance { get; private set; }

    [Header("Music Tracks")]
    [SerializeField] private AudioSource mainTheme;
    [SerializeField] private AudioSource escapeTheme;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            PlayMainTheme();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayMainTheme()
    {
        if (escapeTheme.isPlaying) escapeTheme.Stop();
        if (!mainTheme.isPlaying) mainTheme.Play();
    }

    public void PlayEscapeMusic()
    {
        if (mainTheme.isPlaying) mainTheme.Stop();
        if (!escapeTheme.isPlaying) escapeTheme.Play();
    }

    public void StopAllMusic()
    {
        mainTheme.Stop();
        escapeTheme.Stop();
    }

    public void StopMusicAndDestroy(){
        
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "main") Destroy(gameObject);
    }

    void OnEnable() => SceneManager.sceneLoaded += OnSceneLoaded;
    void OnDisable() => SceneManager.sceneLoaded -= OnSceneLoaded;
}
