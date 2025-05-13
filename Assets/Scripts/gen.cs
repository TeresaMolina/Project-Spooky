using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;

public class Generator : MonoBehaviour
{
    [Header("UI")]
    public GameObject interactPrompt;

    [Header("Settings")]
    public bool isActivated = false;
    public UnityEvent onActivated;

    [Header("Visuals")]
    public Light2D generatorLight;
    public ParticleSystem activationParticles;
    public Color activatedColor = Color.green;

    [Header("Connected Systems")]
    public Light2D[] controlledLights;
    public GameObject[] statuesToDisable;

    [Header("Sound")]
    public AudioClip activationSound;
    private AudioSource audioSource;

    private bool isPlayerInRange = false;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    void Update()
    {
        if (isPlayerInRange && !isActivated && Input.GetKeyDown(KeyCode.F))
        {
            ActivateGenerator();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = true;
            if (interactPrompt != null) interactPrompt.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInRange = false;
            if (interactPrompt != null) interactPrompt.SetActive(false);
        }
    }

    void ActivateGenerator()
    {
        isActivated = true;

        // Play activation sound
        if (activationSound != null)
        {
            audioSource.PlayOneShot(activationSound);
        }

        // Visual feedback
        generatorLight.color = activatedColor;
        activationParticles.Play();

        // Enable connected lights
        foreach (Light2D light in controlledLights)
        {
            light.enabled = true;
        }

        // Disable statues
        foreach (GameObject statue in statuesToDisable)
        {
            statue.GetComponent<EnemyAI>().enabled = false;
        }

        onActivated.Invoke();
    }
}
