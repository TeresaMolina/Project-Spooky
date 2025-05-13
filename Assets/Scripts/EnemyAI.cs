using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 3f;
    private Rigidbody2D rb;
    private Transform player;
    private Aim playerFlashlight;

    [Header("Flashlight Detection")]
    public LayerMask obstacleLayers;
    public float detectionRange = 5f;
    public float fovAngle = 90f;
    private bool isIlluminated;

    [Header("Visibility")]
    public float revealDistance = 3f;
    private SpriteRenderer spriteRenderer;

    [Header("Jumpscare")]
    public VideoPlayer jumpscarePlayer;
    public RawImage videoDisplay;
    private bool hasTriggered;
    private AudioSource videoAudio;

    [Header("Footstep Sounds")]
    public AudioClip[] footstepSounds;
    public float footstepInterval = 1f;
    public float minFootstepDistance = 5f;
    public float maxFootstepDistance = 15f;
    private float footstepCooldown;
    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerFlashlight = player.GetComponentInChildren<Aim>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Collider setup
        CircleCollider2D collider = GetComponent<CircleCollider2D>() ?? gameObject.AddComponent<CircleCollider2D>();
        collider.isTrigger = true;
        collider.radius = 0.5f;

        // Video setup
        videoDisplay.gameObject.SetActive(false);
        jumpscarePlayer.playOnAwake = false;
        videoAudio = jumpscarePlayer.GetComponent<AudioSource>();
        
        if(videoAudio != null)
        {
            videoAudio.playOnAwake = false;
            videoAudio.volume = 0f;
        }

        jumpscarePlayer.prepareCompleted += OnVideoPrepared;
        jumpscarePlayer.loopPointReached += EndJumpscare;

        // Footstep audio setup
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.spatialBlend = 1f;
            audioSource.rolloffMode = AudioRolloffMode.Linear;
            audioSource.minDistance = minFootstepDistance;
            audioSource.maxDistance = maxFootstepDistance;
        }
    }

    void Update()
    {
        if (!hasTriggered)
        {
            CheckFlashlightIllumination();
            UpdateVisibility();
            HandleMovement();
            HandleFootsteps();
        }
    }

    void HandleFootsteps()
    {
        if (!spriteRenderer.enabled && rb.linearVelocity.magnitude > 0.1f)
        {
            footstepCooldown -= Time.deltaTime;
            if (footstepCooldown <= 0)
            {
                PlayFootstep();
                footstepCooldown = footstepInterval + Random.Range(-0.2f, 0.2f);
            }
        }
    }

    void PlayFootstep()
    {
        if (footstepSounds.Length > 0)
        {
            int randomIndex = Random.Range(0, footstepSounds.Length);
            audioSource.pitch = Random.Range(0.9f, 1.1f);
            audioSource.PlayOneShot(footstepSounds[randomIndex]);
        }
    }

    void CheckFlashlightIllumination()
    {
        Vector2 toEnemy = (transform.position - player.position).normalized;
        float distance = Vector2.Distance(player.position, transform.position);
        float angle = Vector2.Angle(playerFlashlight.lookDir, toEnemy);

        RaycastHit2D hit = Physics2D.Raycast(
            player.position,
            toEnemy,
            detectionRange,
            obstacleLayers
        );

        isIlluminated = (angle <= fovAngle/2) && 
                       (distance <= detectionRange) && 
                       (hit.collider == null || hit.collider.gameObject == gameObject);
    }

    void UpdateVisibility()
    {
        if(spriteRenderer == null) return;

        // Calculate distance to player
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        // Reveal if illuminated or within reveal distance
        bool shouldBeVisible = isIlluminated || distanceToPlayer <= revealDistance;
        spriteRenderer.enabled = shouldBeVisible;
    }

    void HandleMovement()
    { 
        if (isIlluminated)
        {
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Vector2 direction = ((Vector2)player.position - rb.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
        }
    }

    // Rest of the jumpscare code remains the same
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
        jumpscarePlayer.prepareCompleted -= OnVideoPrepared;
        jumpscarePlayer.loopPointReached -= EndJumpscare;
    }
}
