using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    
    [Header("Door System")]
    public DoorController exitDoor;

    [Header("Generator System")]
    public Generator[] generators; // Must have 4 elements in Inspector
    public int activatedGeneratorCount = 0;

    [Header("UI")]
    public TMP_Text generatorCounterText; // Assign in Inspector

    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeUI();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void InitializeUI()
    {
        Debug.Log($"UI Initialized. Total generators: {generators.Length}");
        UpdateGeneratorUI();
    }

public void CheckGenerators()
{
    activatedGeneratorCount = 0;
    
    foreach(Generator gen in generators)
    {
        Debug.Log($"Checking: {gen.gameObject.name} | Activated: {gen.isActivated}");
        if(gen.isActivated) activatedGeneratorCount++;
    }

    Debug.Log($"Total Activated: {activatedGeneratorCount}/{generators.Length}");
    
    UpdateGeneratorUI();
    
    // Add this debug log to confirm the check
    Debug.Log($"Checking if {activatedGeneratorCount} >= {generators.Length}");
    
    if(activatedGeneratorCount >= generators.Length)
    {
        Debug.Log("=== ALL GENERATORS ACTIVE! OPENING DOOR ===");
        OpenExitDoor();
    }
}






    void UpdateGeneratorUI()
    {
        if(generatorCounterText != null)
        {
            generatorCounterText.text = $"Generators: {activatedGeneratorCount}/{generators.Length}";
            Debug.Log($"UI Updated: {generatorCounterText.text}");
        }
        else
        {
            Debug.LogError("Generator counter text is not assigned!");
        }
    }
        void OpenExitDoor()
    {
        if(exitDoor != null)
        {
            exitDoor.OpenDoor();
            Debug.Log("All generators activated! Door opened!");
        }
    }
}
