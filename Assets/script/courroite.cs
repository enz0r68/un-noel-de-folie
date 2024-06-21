using System.Collections;
using System.Collections.Generic;
using Unity.Profiling;
using UnityEngine;
using UnityEngine.SceneManagement;

public class courroite : MonoBehaviour
{
    [SerializeField] AudioSource sfxSource;
    [SerializeField] AudioSource sfxSource1;
    public AudioClip sfx;
    public AudioClip sfx1;

    private Animator anim;
    public GameObject personage;
    public float vitesse = 1.5f;
    private bool isPlayingFootstepSound = false;
    private bool playerIsCurrentlyMoving = false;

    // Compteur requis pour terminer le jeu
    public string nextSceneName; // Nom de la scène suivante à charger

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        sfxSource.clip = sfx;
        sfxSource.loop = false; // Le son de pas ne devrait pas être en boucle
    }

    // Update is called once per frame
    void Update()
    {
        if (anim != null)
        {
            playerIsCurrentlyMoving = false;

            // Check for horizontal movement
            if (Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("courdroite", true);
                personage.transform.Translate(Vector3.right * vitesse * Time.deltaTime);
                playerIsCurrentlyMoving = true;
            }
            else
            {
                anim.SetBool("courdroite", false);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("courgauche", true);
                personage.transform.Translate(Vector3.left * vitesse * Time.deltaTime);
                playerIsCurrentlyMoving = true;
            }
            else
            {
                anim.SetBool("courgauche", false);
            }

            // Check for vertical movement
            if (Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("courdevant", true);
                personage.transform.Translate(Vector3.up * vitesse * Time.deltaTime);
                playerIsCurrentlyMoving = true;
            }
            else
            {
                anim.SetBool("courdevant", false);
            }

            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("courderrier", true);
                personage.transform.Translate(Vector3.down * vitesse * Time.deltaTime);
                playerIsCurrentlyMoving = true;
            }
            else
            {
                anim.SetBool("courderrier", false);
            }

            // Play or stop the footstep sound based on movement
            if (playerIsCurrentlyMoving)
            {
                PlayFootstepSound();
            }
            else
            {
                StopFootstepSound();
            }
        }
    }

    void PlayFootstepSound()
    {
        if (!isPlayingFootstepSound)
        {
            sfxSource.Play();
            isPlayingFootstepSound = true;
        }
    }

    void StopFootstepSound()
    {
        if (isPlayingFootstepSound)
        {
            sfxSource.Stop();
            isPlayingFootstepSound = false;
        }
    }

    void LateUpdate()
    {
        if (playerIsCurrentlyMoving && !sfxSource.isPlaying)
        {
            sfxSource.Play();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("mort"))
        {
            RestartScene();
        }

        // Vérifiez la condition de victoire
        if (other.gameObject.CompareTag("fin") && (suivre2.followerCount == 8))
        {
            Debug.Log("VICTOIRE");
            SceneManager.LoadScene(4);
        }
    }

    void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
