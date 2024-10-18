using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    Image imageToFade;  // Reference to the Image component

    public enum FadeType
    {
        FadeIn,
        FadeOut
    }

    public FadeType fadeType = FadeType.FadeOut;  // Choose FadeIn or FadeOut

    public float fadeDuration = .8f;  // Duration of the fade
    public bool isRunOnStart;
    public int indexToLoad;
    public bool isLoadNextScene;

    private void Start()
    {
        imageToFade = GetComponent<Image>();
        if (isRunOnStart)
        {
            if (fadeType == FadeType.FadeOut)
            {
                StartCoroutine(FadeOut());
            }
            else
            {
                StartCoroutine(FadeIn());
            }
        }
        
    }

    private IEnumerator FadeOut()
    {
        Color imageColor = imageToFade.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(elapsedTime / fadeDuration);  // Increase alpha from 0 to 1
            imageToFade.color = imageColor;
            yield return null;
        }

        if (isLoadNextScene) {FindObjectOfType<GameManager>().LoadNextScene();}
        else {FindObjectOfType<GameManager>().LoadScene(indexToLoad);}
    }

    private IEnumerator FadeIn()
    {
        Color imageColor = imageToFade.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            imageColor.a = Mathf.Clamp01(1 - elapsedTime / fadeDuration);  // Decrease alpha from 1 to 0
            imageToFade.color = imageColor;
            yield return null;
        }
    }

    public void StartFadeIn()
    {
        StartCoroutine(FadeIn());
    }

    public void StartFadeOut()
    {
        StartCoroutine(FadeOut());
    }

}
