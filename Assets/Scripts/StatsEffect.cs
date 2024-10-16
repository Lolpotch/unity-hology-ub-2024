using UnityEngine;
using TMPro;

public class StatsEffect : MonoBehaviour
{
    public int happyStart, moneyStart, securityStart;
    int happy, money, security;
    public TextMeshProUGUI txtHappy, txtMoney, txtSecurity;
    public RectTransform parHappy, parMoney, parSecurity;
    public GameObject effectParticle;

    void Start()
    {
        happy = happyStart;
        money = moneyStart;
        security = securityStart;

        txtHappy.text = happy.ToString() + "%";
        txtMoney.text = money.ToString() + "%";
        txtSecurity.text = security.ToString() + "%";

        //EffectHappy(-10);
    }

    /*/
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            EffectHappy(10);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            EffectMoney(-10);
        }
    }
    /*/

    public void EffectHappy(int amount)
    {
        // Update the txtHappy text with the new amount
        happy += amount;
        if (happy > 100){happy = 100;}
        else if (happy < 0){happy = 0;}
        txtHappy.text = happy.ToString() + "%";

        // Instantiate the particle effect at the position of parHappy
        GameObject particleInstance = Instantiate(effectParticle);

        // Optionally, make the particle effect a child of parHappy to follow it
        particleInstance.transform.SetParent(parHappy, false);
        particleInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ParticleColorControl(amount, particleInstance);
    }

    public void EffectMoney(int amount)
    {
        // Update the txtHappy text with the new amount
        money += amount;
        if (money > 100){money = 100;}
        else if (money < 0){money = 0;}
        txtMoney.text = money.ToString() + "%";

        // Instantiate the particle effect at the position of parHappy
        GameObject particleInstance = Instantiate(effectParticle);

        // Optionally, make the particle effect a child of parHappy to follow it
        particleInstance.transform.SetParent(parMoney, false);
        particleInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ParticleColorControl(amount, particleInstance);
    }

    public void EffectSecurity(int amount)
    {
        // Update the txtHappy text with the new amount
        security += amount;
        if (security > 100){security = 100;}
        else if (security < 0){security = 0;}
        txtSecurity.text = security.ToString() + "%";

        // Instantiate the particle effect at the position of parHappy
        GameObject particleInstance = Instantiate(effectParticle);

        // Optionally, make the particle effect a child of parHappy to follow it
        particleInstance.transform.SetParent(parSecurity, false);
        particleInstance.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        ParticleColorControl(amount, particleInstance);
    }


    void ParticleColorControl(int amount, GameObject particleInstance)
    {
        if (amount > 0)
        {
            particleInstance.GetComponent<TextMeshProUGUI>().text = "+" + amount.ToString();  // Use "+" for positive values
            Color green = new Color(0, 1, 0, 1f); // Green with full opacity (1 transparency)
            particleInstance.GetComponent<TextMeshProUGUI>().color = green; // Set text color to green
        }
        else
        {
            particleInstance.GetComponent<TextMeshProUGUI>().text = amount.ToString();  // Automatically includes "-" for negative values
            Color red = new Color(1, 0, 0, 1f); // Green with full opacity (1 transparency)
            particleInstance.GetComponent<TextMeshProUGUI>().color = red; // Set text color to red
        }

        // Destroy the particle effect after a short duration
        Destroy(particleInstance, 1.0f); // Adjust the time to match your particle's lifetime
    }

}
