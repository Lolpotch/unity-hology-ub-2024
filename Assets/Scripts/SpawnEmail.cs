using UnityEngine;

public class SpawnEmail : MonoBehaviour
{
    public Vector2 spawnpoint;

    public GameObject[] emails;
    public RectTransform parent;

    public void Spawn()
    {
        print("EMAIL SPAWNED");
        RectTransform email = Instantiate(emails[0]).GetComponent<RectTransform>();
        email.SetParent(parent);
        email.anchoredPosition = spawnpoint;
        email.localScale = Vector3.one;
    }

}
