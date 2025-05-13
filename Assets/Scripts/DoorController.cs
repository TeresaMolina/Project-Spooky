using UnityEngine;

public class DoorController : MonoBehaviour
{
    [Header("Border Object")]
    public GameObject borderWall; // Assign your ExitDoorBlocker here

    [Header("Collision")]
    public Collider2D doorCollider; // Assign the BoxCollider2D on ExitDoorBlocker

public void OpenDoor()
{
    Debug.Log("DoorController.OpenDoor() called");

    if (borderWall == null)
    {
        Debug.LogError("BorderWall reference is null!");
        return;
    }

    if (doorCollider == null)
    {
        Debug.LogError("DoorCollider reference is null!");
        return;
    }

    borderWall.SetActive(false);
    doorCollider.enabled = false;
    Debug.Log("Door opened successfully!");
}

}
