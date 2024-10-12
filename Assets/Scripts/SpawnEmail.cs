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

    void Start()
    {
        mailIcon = GetComponent<Image>();
    }

    void Update()
    {
        // Check if the current email has been destroyed and update the sprite
        if (currentEmail == null)
        {
            mailIcon.sprite = mailClose;
        }
    }

    // Function to spawn an email and set the mail icon to "mailOpen"
    public void Spawn()
    {
        print("EMAIL SPAWNED");

        // Instantiate the email and get its RectTransform
        currentEmail = Instantiate(emails[0]).GetComponent<RectTransform>();

        // Set the parent, position, and scale
        currentEmail.SetParent(parent);
        currentEmail.anchoredPosition = spawnpoint;
        currentEmail.localScale = Vector3.one;

        // Change the mail icon to mailOpen
        mailIcon.sprite = mailOpen;
    }
}
