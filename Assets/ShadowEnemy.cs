using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class ShadowEnemy : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float health = 3f;
    public float spawnDistance = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private bool isIlluminated;
    private float currentHealth;

    // Jumpscare
    [Header("Jumpscare")]
    private VideoPlayer jumpscarePlayer;
    private RawImage videoDisplay;
    private bool hasTriggered;
    private AudioSource videoAudio;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = health;

        // Find jumpscare components by exact names
        GameObject jumpscareCanvas = GameObject.Find("JumpscareCanvas");
        if (jumpscareCanvas != null)
        {
            jumpscarePlayer = jumpscareCanvas.transform.Find("Video Player").GetComponent<VideoPlayer>();
            videoDisplay = jumpscareCanvas.transform.Find("JumpscareDisplay").GetComponent<RawImage>();
        }

        if (jumpscarePlayer != null && videoDisplay != null)
        {
            videoDisplay.gameObject.SetActive(false);
            jumpscarePlayer.playOnAwake = false;
            videoAudio = jumpscarePlayer.GetComponent<AudioSource>();
            
            if (videoAudio != null)
            {
                videoAudio.playOnAwake = false;
                videoAudio.volume = 0f;
            }

            jumpscarePlayer.prepareCompleted += OnVideoPrepared;
            jumpscarePlayer.loopPointReached += EndJumpscare;
        }
        else
        {
            Debug.LogError("Jumpscare components not found in JumpscareCanvas!");
        }
    }

    void Update()
    {
        if (hasTriggered) return;

        if (isIlluminated)
        {
            currentHealth -= Time.deltaTime;
            if (currentHealth <= 0) Destroy(gameObject);
        }
        else
        {
            currentHealth = Mathf.Min(currentHealth + Time.deltaTime, health);
        }
    }

    void FixedUpdate()
    {
        if (hasTriggered) return;

        if (!isIlluminated)
        {
            Vector2 direction = (player.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }

    public void SetIlluminated(bool illuminated)
    {
        isIlluminated = illuminated;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !hasTriggered)
        {
            TriggerJumpscare();
        }
    }

void TriggerJumpscare()
{
    hasTriggered = true;
    rb.linearVelocity = Vector2.zero;

    // Stop and destroy game music
    if (GameMusicManager.Instance != null)
        GameMusicManager.Instance.StopMusicAndDestroy();

    Time.timeScale = 0.0001f;
    AudioListener.pause = true;
    videoDisplay.gameObject.SetActive(true);
    jumpscarePlayer.Prepare();
}


    void OnVideoPrepared(VideoPlayer vp)
    {
        if(videoAudio != null)
        {
            videoAudio.volume = 1f;
            videoAudio.Play();
        }
        vp.Play();
    }

    void EndJumpscare(VideoPlayer vp)
    {
        if(videoAudio != null)
        {
            videoAudio.volume = 0f;
            videoAudio.Stop();
        }

        Time.timeScale = 1f;
        AudioListener.pause = false;
        videoDisplay.gameObject.SetActive(false);
        SceneManager.LoadScene("main");
    }

    void OnDestroy()
    {
        if (jumpscarePlayer != null)
        {
            jumpscarePlayer.prepareCompleted -= OnVideoPrepared;
            jumpscarePlayer.loopPointReached -= EndJumpscare;
        }
    }
}
