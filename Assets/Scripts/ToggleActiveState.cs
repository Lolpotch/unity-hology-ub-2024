using UnityEngine;

public class ToggleActiveState : MonoBehaviour
{
    public GameObject targetObject; // The GameObject to toggle

    // This method will be called when the button is pressed
    public void ToggleObject()
    {
        // Check if the target object is currently active
        bool isActive = targetObject.activeSelf;

        // Toggle the active state
        targetObject.SetActive(!isActive);
    }
}