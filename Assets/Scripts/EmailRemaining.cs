using UnityEngine;
using TMPro;

public class EmailRemaining : MonoBehaviour
{
    int emailRemaining;
    SpawnEmail spawnEmail;
    public TextMeshProUGUI emailText; // Reference to TextMeshPro component

    // Start is called before the first frame update
    void Start()
    {
        emailText = GetComponent<TextMeshProUGUI>();
        spawnEmail = FindObjectOfType<SpawnEmail>();

        emailRemaining = 16;
        UpdateEmailText();
    }

    void Update()
    {
        if (emailRemaining == 0)
        {
            OnEmailEmpty();
        }
    }

    public void DecreaseRemaining(int amount)
    {
        emailRemaining -= amount;
        UpdateEmailText(); // Update the text whenever the number changes
    }

    void UpdateEmailText()
    {
        emailText.text = "Sisa " + emailRemaining + " email lagi";
    }

    public void OnEmailEmpty()
    {
        spawnEmail.canSpawn = false;
    }
}
