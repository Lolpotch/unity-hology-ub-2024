using UnityEngine;

public class SpawnEmail : MonoBehaviour
{
    public Vector2 spawnpoint;

    public RectTransform[] emails;
    RectTransform canvas;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
    }

    public void Spawn()
    {
        RectTransform email = Instantiate(emails[0]);
        email.SetParent(canvas);
        email.anchoredPosition = spawnpoint;
    }

}
