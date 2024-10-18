using UnityEngine;

public class AiOpen : MonoBehaviour
{
    public int usageStart = 2;
    int usageLeft;
    public GameObject aiNotOpenWindow, aiOpenAskWindow;

    void Start()
    {
        usageLeft = usageStart;
    }

    public void OnPressAiButton()
    {
        Email email = FindObjectOfType<Email>();
        
        if (email == null)
        {
            aiNotOpenWindow.SetActive(true);
        }
        else
        {
            aiOpenAskWindow.SetActive(true);
        }
    }
}
