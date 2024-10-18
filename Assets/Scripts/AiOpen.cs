using TMPro;
using UnityEngine;

public class AiOpen : MonoBehaviour
{
    public int usageStart = 2;
    public int usageLeft;
    public GameObject aiNotOpenWindow, aiOpenAskWindow, aiEmpty;
    public TextMeshProUGUI aiOpenAskMessage;

    void Start()
    {
        usageLeft = usageStart;
    }

    public void OnPressAiButton()
    {
        if (usageLeft > 0)
        {
            Email email = FindObjectOfType<Email>();
        
            if (email == null)
            {
                aiNotOpenWindow.SetActive(true);
            }
            else
            {
                aiOpenAskMessage.text = "Apakah anda yakin ingin menggunakan fitur AI? (sisa penggunaan : " + usageLeft.ToString() + ")";
                aiOpenAskWindow.SetActive(true);
            }
        }
        else
        {
            aiEmpty.SetActive(true);
        }
        
    }
}
