using System.Collections;
using UnityEngine;

public class RectTransformMover : MonoBehaviour
{
    RectTransform rectTransform; // Reference to the RectTransform
    Vector3 moveDirection;
    public float moveSpeed = 50f; // Speed of the upward movement
    public float destroyAfterSeconds = 2f; // Time after which the RectTransform will be destroyed
    public float moveDuration = 1f; // How long the RectTransform moves upwards

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void StartMoveAndDestroy(Vector2 direction)
    {
        StartCoroutine(MoveAndDestroy(direction));
    }

    // Coroutine to move the RectTransform upward and destroy it after a period
    IEnumerator MoveAndDestroy(Vector2 direction)
    {
        float elapsedTime = 0f;

        // Move the RectTransform upwards over time
        while (elapsedTime < moveDuration)
        {
            rectTransform.anchoredPosition += direction * moveSpeed * Time.deltaTime; // Move upward
            elapsedTime += Time.deltaTime; // Increment elapsed time
            yield return null; // Wait until the next frame
        }

        // Wait for an additional period before destroying
        yield return new WaitForSeconds(destroyAfterSeconds);

        // Destroy the GameObject this script is attached to
        Destroy(gameObject);
    }

}

public enum Direction
{
    Left,
    Right,
    Up,
    Down
}