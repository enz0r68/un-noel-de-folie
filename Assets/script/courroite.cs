using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class courroite : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;

    public AudioClip sfx;
    private Animator anim;
    public GameObject personage;
    public float vitesse = 1.5f;
    private bool isRunning = false;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfxSource.clip = sfx;
        sfxSource.loop = true; // Assurez-vous que le son est configuré pour se répéter
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            // Check for horizontal movement
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("courdroite", true);
                personage.transform.Translate(Vector3.right * vitesse * Time.deltaTime);
                PlayRunningSound();
            }
            else
            {
                anim.SetBool("courdroite", false);
                StopRunningSound();
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("courgauche", true);
                personage.transform.Translate(Vector3.left * vitesse * Time.deltaTime);
                PlayRunningSound();
            }
            else
            {
                anim.SetBool("courgauche", false);
                StopRunningSound();
            }

            // Check for vertical movement
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("courdevant", true);
                personage.transform.Translate(Vector3.up * vitesse * Time.deltaTime);
                PlayRunningSound();
            }
            else
            {
                anim.SetBool("courdevant", false);
                StopRunningSound();
            }

            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("courderrier", true);
                personage.transform.Translate(Vector3.down * vitesse * Time.deltaTime);
                PlayRunningSound();
            }
            else
            {
                anim.SetBool("courderrier", false);
                StopRunningSound();
            }
        }
    }

    void PlayRunningSound()
    {
        if (!isRunning)
        {
            sfxSource.Play();
            isRunning = true;
        }
    }

    void StopRunningSound()
    {
        if (isRunning)
        {
            sfxSource.Stop();
            isRunning = false;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("mort"))
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
