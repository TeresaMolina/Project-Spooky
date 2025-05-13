using UnityEngine;

public class door : MonoBehaviour
{
    [Header("Door Settings")]
    public Generator[] requiredGenerators;
    public Collider2D doorCollider;
    public ShadowSpawner shadowSpawner;

    void Update()
    {
        if (AllGeneratorsActivated())
        {
            doorCollider.enabled = false;
            shadowSpawner.StartSpawning();
            
            if (GameMusicManager.Instance != null)
                GameMusicManager.Instance.PlayEscapeMusic();
            
            enabled = false;
        }
    }

    bool AllGeneratorsActivated()
    {
        foreach (Generator gen in requiredGenerators)
            if (!gen.isActivated) return false;
        return true;
    }
}
