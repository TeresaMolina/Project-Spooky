using UnityEngine;

public class EscapeTrigger : MonoBehaviour
{
    [Header("References")]
    public EscapedUI escapeUI; // Assign EscapedUIManager GameObject here

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            escapeUI.ShowEscaped();
        }
    }
}
