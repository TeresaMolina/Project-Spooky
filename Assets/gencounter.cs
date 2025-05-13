using UnityEngine;
using TMPro;

public class GeneratorCounter : MonoBehaviour
{
    [Header("References")]
    public Generator[] generators;
    public TMP_Text counterText;

    void Update()
    {
        int activatedCount = GetActivatedCount();
        string statusText = activatedCount >= generators.Length ? 
            $"Exit Opened!" : 
            $"Gen: {activatedCount}/{generators.Length}";
        
        counterText.text = statusText;
    }

    int GetActivatedCount()
    {
        int count = 0;
        foreach (Generator gen in generators)
        {
            if (gen.isActivated) count++;
        }
        return count;
    }
}
