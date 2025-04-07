using UnityEngine;

public class ClearSoundEffect : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    private void Start()
    {
        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();

        if (audioSource != null)
        {
            // Destroy the GameObject after the audio has finished
            Destroy(gameObject, audioSource.clip.length);
        }
        else
        {
            Debug.LogWarning("No AudioSource found on the GameObject!");
        }
    }
}
