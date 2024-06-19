using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class suivre : MonoBehaviour
{
    // Ajoutez une référence publique au joueur, à assigner via l'inspecteur de Unity
    public Transform player;

    // Déplacez la déclaration de la queue ici si elle n'existe pas déjà
    private Queue<Vector3> playerPositions = new Queue<Vector3>();
    private float timeSinceLastPositionUpdate = 0f;
    public float positionDelay = 1.0f; // Temps en secondes entre chaque enregistrement de position
    public float followSpeed = 2.0f; // Vitesse de déplacement de l'objet suiveur

    void Start()
    {
        // Initialisez la queue si nécessaire
        playerPositions = new Queue<Vector3>();
    }

    void Update()
    {
        // Enregistrez la position du joueur à intervalles réguliers
        timeSinceLastPositionUpdate += Time.deltaTime;
        if (timeSinceLastPositionUpdate >= positionDelay)
        {
            if (player != null)
            {
                playerPositions.Enqueue(player.position);
                timeSinceLastPositionUpdate = 0f;
            }
        }
    
        // Suivez les positions précédentes du joueur
        if (playerPositions.Count > 0)
        {
            Vector3 nextPosition = playerPositions.Peek();
            if (Vector3.Distance(transform.position, nextPosition) > followSpeed * Time.deltaTime)
            {
                // Déplacez-vous vers la prochaine position enregistrée
                transform.position = Vector3.MoveTowards(transform.position, nextPosition, followSpeed * Time.deltaTime);
            }
            else
            {
                // Quand vous êtes assez proche de la prochaine position, considérez que vous l'avez atteinte et passez à la suivante
                playerPositions.Dequeue();
            }
        }
    }
}