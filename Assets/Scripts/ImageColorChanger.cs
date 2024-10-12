using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageColorChanger : MonoBehaviour
{
    Image image; // Reference to the Image component
    public float duration = .5f; // Duration of the color transition

    void Start()
    {
        image = GetComponent<Image>();
    }

    public void StartChangeColor(Color color)
    {
        // Start the color transition Coroutine
        StartCoroutine(ChangeColorGradually(color));
    }

    // Coroutine to change the color of the image gradually
    IEnumerator ChangeColorGradually(Color targetColor)
    {
        float elapsedTime = 0f; // Track how much time has passed

        // Store the initial color of the image
        Color initialColor = image.color;

        // Loop over the duration, gradually changing the color
        while (elapsedTime < duration)
        {
            // Calculate the interpolation factor (goes from 0 to 1 over time)
            float t = elapsedTime / duration;

            // Lerp (linear interpolation) between the start and target color
            image.color = Color.Lerp(initialColor, targetColor, t);

            // Increment the elapsed time
            elapsedTime += Time.deltaTime;

            // Wait until the next frame
            yield return null;
        }

        // Ensure the image color is exactly the target color at the end
        image.color = targetColor;
    }
}
