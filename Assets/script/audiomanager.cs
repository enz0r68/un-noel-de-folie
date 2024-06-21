using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource; // Source audio pour la musique de fond
    [SerializeField] private AudioSource sfxSource; // Source audio pour les effets sonores

    [SerializeField] private AudioClip backgroundMusic; // Musique de fond initiale
    [SerializeField] private AudioClip menuMusicClip; // Musique du menu

    // Start is called before the - first frame update
    private void Start()
    {
        audioSource.Stop();
        audioSource.clip = backgroundMusic; // la musique de fond initiale
        audioSource.Play(); // Joue la musique de fond
    }
}
