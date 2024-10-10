using System.Collections;
using UnityEngine;

public class RectTransformMover : MonoBehaviour
{
    RectTransform rectTransform; // Reference to the RectTransform
    public float moveSpeed = 50f; // Speed of the upward movement
    public float destroyAfterSeconds = 2f; // Time after which the RectTransform will be destroyed
    public float moveDuration = 1f; // How long the RectTransform moves upwards

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartMoveAndDestroy()
    {
        StartCoroutine(MoveAndDestroy());
    }

    // Coroutine to move the RectTransform upward and destroy it after a period
    IEnumerator MoveAndDestroy()
    {
        float elapsedTime = 0f;

        // Move the RectTransform upwards over time
        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition += Vector2.up * moveSpeed * Time.deltaTime; // Move upward
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait until the next frame
        }

        // Wait for an additional period before destroying
        yield return new WaitForSeconds(destroyAfterSeconds);

        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }
}
