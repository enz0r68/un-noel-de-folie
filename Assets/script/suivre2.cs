using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suivre2 : MonoBehaviour
{
    [SerializeField] AudioSource followSoundSource;
    public AudioClip followSoundClip;
    public float followSpeed = 5f; // La vitesse à laquelle ce segment suit le segment précédent
    public float followDistance = 1f; // La distance que ce segment doit garder par rapport au segment précédent

    private Transform target; // L'objet que ce segment doit suivre
    private bool isFollowing = false; // Indique si ce segment suit une cible
    private static Transform lastFollower; // Le dernier suiveur de la chaîne

    public static int followerCount = 0; // Compteur statique pour le nombre d'objets qui suivent

    void Start()
    {
        followerCount = 0;
        if (followSoundSource != null)
        {
            followSoundSource.playOnAwake = false;
        }
    }

    void Update()
    {
        // Si ce segment suit un autre, ajuster sa position pour le suivre
        if (isFollowing && target != null)
        {
            float distance = Vector3.Distance(transform.position, target.position);

            if (distance > followDistance)
            {
                Vector3 newPosition = Vector3.MoveTowards(transform.position, target.position, followSpeed * Time.deltaTime);
                transform.position = newPosition;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Si ce segment entre en collision avec un autre objet avec le tag "Leader" ou "Follower", il doit suivre cet objet
        if ((other.CompareTag("Leader") || other.CompareTag("Follower")) && !isFollowing)
        {
            if (lastFollower == null)
            {
                target = other.transform;
            }
            else
            {
                target = lastFollower;
            }
            isFollowing = true;
            lastFollower = this.transform; // Mettre à jour le dernier suiveur

            // Incrémenter le compteur de suiveurs
            followerCount++;
            Debug.Log("Follower count: " + followerCount);

            // Jouez le son de suivi ici en utilisant PlayOneShot pour jouer l'AudioClip
            if (followSoundSource != null && followSoundClip != null)
            {
                followSoundSource.PlayOneShot(followSoundClip);
            }

            Debug.Log("Target assigned: " + target.name);
        }
    }
}
