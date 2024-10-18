using TMPro;
using UnityEngine;

public class Ai : MonoBehaviour
{
    public TextMeshProUGUI txtAcceptHappy, txtAcceptMoney, txtAcceptSecurity, txtDenyHappy, txtDenyMoney, txtDenySecurity;
    Email email;

    void OnEnable()
    {
        email = FindObjectOfType<Email>();

        ShowEffectStats(txtAcceptHappy, email.happyEffectForward);
        ShowEffectStats(txtAcceptMoney, email.moneyEffectForward);
        ShowEffectStats(txtAcceptSecurity, email.securityEffectForward);

        ShowEffectStats(txtDenyHappy, email.happyEffectTrash);
        ShowEffectStats(txtDenyMoney, email.moneyEffectTrash);
        ShowEffectStats(txtDenySecurity, email.securityEffectTrash);
    }

    void ShowEffectStats(TextMeshProUGUI textMeshPro, int statEffect)
    {
        if (statEffect > 0)
        {
            textMeshPro.color = Color.green;
            textMeshPro.text = "+" + statEffect.ToString();
        }
        else if (statEffect < 0)
        {
            textMeshPro.color = Color.red;
            textMeshPro.text = statEffect.ToString();
        }
        else
        {
            textMeshPro.color = Color.black;
            textMeshPro.text = "0";
        }
    }
}
