using UnityEngine;

public class audiomanager1 : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip sfx;

    // Singleton instance
    private static audiomanager1 instance = null;

    public static audiomanager1 Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        // Singleton pattern
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
            return;
        }

        // Init audioSource with background music if it's not already set
        if (!audioSource.clip)
        {
            audioSource.clip = backgroundMusic;
        }

        // Play the music if it's not playing
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    // Method for playing sound effects
    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    // Method for changing background music
    public void ChangeBackgroundMusic(AudioClip newMusic)
    {
        if (audioSource.clip != newMusic)
        {
            audioSource.Stop();
            audioSource.clip = newMusic;
            audioSource.Play();
        }
    }
}
