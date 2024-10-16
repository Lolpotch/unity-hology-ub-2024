using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnEmail : MonoBehaviour
{
    public Sprite mailOpen, mailClose; // Sprites for open and closed mail
    public Vector2 spawnpoint;

    public GameObject[] emails; // Array of email prefabs
    public RectTransform parent; // Parent transform for spawned emails
    Image mailIcon; // Reference to the UI Image that shows the mail status

    RectTransform currentEmail; // Keep track of the currently spawned email
    Button button;
    bool firstSpawn = true; // Flag to track the first spawn
    public bool canSpawn = true;

    private List<int> availableIndices;  // List to store available email indices

    void Start()
    {
        mailIcon = GetComponent<Image>();
        button = GetComponent<Button>();

        canSpawn = true;

        // Initialize the list with all possible indices
        availableIndices = new List<int>();
        for (int i = 1; i < emails.Length; i++)
        {
            availableIndices.Add(i);
        }
    }

    void Update()
    {
        // Check if the current email has been destroyed and update the sprite
        if (currentEmail == null && canSpawn)
        {
            mailIcon.sprite = mailClose;
            button.interactable = true;
        }
    }

    // Function to spawn an email and set the mail icon to "mailOpen"
    public void Spawn()
    {
        print("EMAIL SPAWNED");

        // If it's the first spawn, spawn the tutorial email (index 0)
        if (firstSpawn)
        {
            currentEmail = Instantiate(emails[0]).GetComponent<RectTransform>();
            firstSpawn = false; // Mark that the first email has been spawned
        }
        else
        {
            if (availableIndices.Count == 0)
            {
                Debug.Log("All emails have been spawned.");
                return;
            }
            // Get a random index from availableIndices
            int randomIndex = Random.Range(1, availableIndices.Count);
            int emailIndex = availableIndices[randomIndex];
            currentEmail = Instantiate(emails[emailIndex]).GetComponent<RectTransform>();
            
            // Remove the selected index from availableIndices
            availableIndices.RemoveAt(randomIndex);
        }

        // Set the parent, position, and scale
        currentEmail.SetParent(parent);
        currentEmail.anchoredPosition = spawnpoint;
        currentEmail.localScale = Vector3.one;

        // Change the mail icon to mailOpen
        mailIcon.sprite = mailOpen;
        button.interactable = false;
    }
}
