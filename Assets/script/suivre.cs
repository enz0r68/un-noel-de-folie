using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suivre : MonoBehaviour
{
    // Une référence publique au joueur, assignable via l'inspecteur de Unity
    // Le joueur est un objet de jeu qui nous intéressera pour le suivi
    [Header("Joueur")]
    public Transform player;

    // Une queue de positions pour stocker les positions du joueur
    // Cette queue sera utilisée pour suivre les positions précédentes du joueur
    [Header("Queue des positions")]
    private Queue<Vector3> playerPositions = new Queue<Vector3>();

    // Le temps depuis la dernière mise à jour de la position
    [Header("Timer")]
    private float timeSinceLastPositionUpdate = 0f;

    // Le délai entre chaque enregistrement de position en secondes
    [Header("Délai d'enregistrement")]
    public float positionDelay = 1.0f;

    // La vitesse de déplacement de l'objet suiveur
    [Header("Vitesse de déplacement")]
    public float followSpeed = 2.0f;

    // Cette fonction est appelée une seule fois au début
    void Start()
    {
        // Initialisez la queue si nécessaire
        playerPositions = new Queue<Vector3>();
    }

    // Cette fonction est appelée à chaque frame
    void Update()
    {
        // Mettez à jour le temps depuis la dernière mise à jour de la position
        timeSinceLastPositionUpdate += Time.deltaTime;

        // Si le temps depuis la dernière mise à jour de la position dépasse le délai d'enregistrement de position
        if (timeSinceLastPositionUpdate >= positionDelay)
        {
            // Si le joueur existe
            if (player != null)
            {
                // Enregistrez la position du joueur dans la queue
                playerPositions.Enqueue(player.position);

                // Réinitialisez le temps depuis la dernière mise à jour de la position
                timeSinceLastPositionUpdate = 0f;
            }
        }

        // Si la queue contient au moins une position
        if (playerPositions.Count > 0)
        {
            // Récupère la prochaine position enregistrée
            Vector3 nextPosition = playerPositions.Peek();

            // Si la distance entre la position actuelle et la prochaine position est supérieure à la vitesse de déplacement multipliée par le temps écoulé depuis la dernière mise à jour de la position
            if (Vector3.Distance(transform.position, nextPosition) > followSpeed * Time.deltaTime)
            {
                // Déplace vers la prochaine position enregistrée à la vitesse de déplacement
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                // Si  assez proche de la prochaine position, considère atteinte et passe à la suivante
                playerPositions.Dequeue();
            }
        }
    }
}

