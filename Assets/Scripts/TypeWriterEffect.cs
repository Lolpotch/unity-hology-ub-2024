using System.Collections;
using UnityEngine;
using TMPro;

public class TypewriterEffect : MonoBehaviour
{
    public TMP_Text tmpText; // Reference to the TextMeshProUGUI component
    public float typingSpeed = 0.05f; // Speed of typing in seconds

    private string fullText; // Store the full text
    private string currentText = ""; // Text currently being displayed

    void Start()
    {
        //StartTyping(fullText);
    }

    public void StartTyping(string text)
    {
        fullText = text;
        tmpText.text = ""; // Clear the text to start with an empty string
        StartCoroutine(ShowText()); // Start the typewriter effect
    }

    IEnumerator ShowText()
    {
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText = fullText.Substring(0, i + 1); // Reveal one more letter
            tmpText.text = currentText; // Set the displayed text
            yield return new WaitForSeconds(typingSpeed); // Wait before showing the next letter
        }
    }
}