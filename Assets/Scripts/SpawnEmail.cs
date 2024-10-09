using UnityEngine;

public class SpawnEmail : MonoBehaviour
{
    public Vector2 spawnpoint;

    public GameObject[] emails;
    RectTransform canvas;
    void Start()
    {
        canvas = FindObjectOfType<Canvas>().GetComponent<RectTransform>();
    }

    public void Spawn()
    {
        RectTransform email = Instantiate(emails[0]).GetComponent<RectTransform>();
        email.SetParent(canvas);
        email.anchoredPosition = spawnpoint;
        email.localScale = Vector3.one;
    }

}
