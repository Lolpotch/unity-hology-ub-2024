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

    void Start()
    {
        mailIcon = GetComponent<Image>();
        button = GetComponent<Button>();

        canSpawn = true;
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
            // For subsequent spawns, choose a random email from the rest of the array (index 1 and onward)
            int randomIndex = Random.Range(1, emails.Length);
            currentEmail = Instantiate(emails[randomIndex]).GetComponent<RectTransform>();
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
