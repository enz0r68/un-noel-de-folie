using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audiomanager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource sfxSource;

    public AudioClip backgroundMusic;
    public AudioClip sfx;


    // Start is called before the first frame update
    private void Start()
    {
        audioSource.clip = backgroundMusic;
        audioSource.Play();
    }
}
