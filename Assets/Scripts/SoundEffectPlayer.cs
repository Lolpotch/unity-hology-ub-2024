using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    public AudioSource click, paper, swipe, notification;  // Reference to the sound effect AudioClip

    private void Start()
    {
    }

    // Method to play the sound effect
    public void PlayClick()
    {
        click.Play();
    }

    public void PlayPaper()
    {
        paper.Play();
    }

    public void PlaySwipe()
    {
        swipe.Play();
    }

    public void PlayNotification()
    {
        notification.Play();
    }
}
