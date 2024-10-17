using System.Collections;
using UnityEngine;
using UnityEngine.UI;  // or using TMPro; if using TextMeshPro
using TMPro;
using Unity.VisualScripting;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; // Use this for TextMeshPro
    public float typeSpeed = 0.05f;

    private string currentText = "";
    private Coroutine typingCoroutine;
    private bool isTyping = false;
    public string[] dialogues;
    int dialogIndex;

    void Start()
    {
        dialogIndex = 0;
        OnClick();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    // Method to start the typewriter effect
    public void StartTyping(string fullText)
    {
        currentText = fullText;
        dialogueText.text = "";
        
        // Stop the current typing coroutine if it exists
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText());
    }

    // Coroutine for typing text one character at a time
    IEnumerator TypeText()
    {
        isTyping = true;
        foreach (char letter in currentText.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typeSpeed);
        }
        isTyping = false;
    }

    // Method to handle the user click
    public void OnClick()
    {
        // If still typing, complete the text immediately
        if (isTyping)
        {
            StopCoroutine(typingCoroutine);
            dialogueText.text = currentText;  // Show full text instantly
            isTyping = false;
        }
        // If typing is done, proceed to the next dialogue
        else
        {
            // Add logic here to load the next dialogue
            LoadNextDialogue();
        }
    }

    // Dummy method to load next dialogue (you can replace this with your actual logic)
    private void LoadNextDialogue()
    {
        StartTyping(dialogues[dialogIndex]);

        if (dialogIndex < dialogues.Length - 1)
        {
            dialogIndex++;
        }
        else
        {
            print("LAST DIALOG REACHED");
            //Load scene 2
        }
    }
}
