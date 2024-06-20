using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Transform segmentPrefab;
    public Transform Prefab2;
    public Vector2Int direction = Vector2Int.right;
    public float speed = 15f;
    public float speedMultiplier = 1f;
    public int initialSize = 4;
    public bool moveThroughWalls = false;

    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;

    private void Start()
    {
        ResetState();
    }

    private void Update()
    {
        // Only allow turning up or down while moving in the x-axis
        if (direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) {
                input = Vector2Int.up;
            } else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) {
                input = Vector2Int.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) {
                input = Vector2Int.right;
            } else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) {
                input = Vector2Int.left;
            }
        }
    }

   private void FixedUpdate()
{
    // Vérifiez si il reste au moins un segment
    if (segments.Count <= 0)
    {
        return; // Sortir de la méthode si aucun segment n'existe
    }

    // Attendre jusqu'à la prochaine mise à jour avant de procéder
    if (Time.time < nextUpdate)
    {
        return;
    }

    // Définir la nouvelle direction basée sur l'entrée
    if (input!= Vector2Int.zero)
    {
        direction = input;
    }

    // Définir la position de chaque segment pour être la même que celui qu'il suit.
    // Nous devons faire cela dans l'ordre inverse afin que la position soit définie à la position précédente,
    // sinon ils seront tous empilés les uns sur les autres.
    for (int i = segments.Count - 1; i > 0; i--)
    {
        // Vérifiez si le segment suivant existe avant d'y accéder
        if (i < segments.Count && segments[i]!= null)
        {
            segments[i].position = segments[i - 1].position;
        }
    }

    // Déplacer la serpent dans la direction dans laquelle elle est orientée
    // Arrondir les valeurs pour s'assurer qu'elle s'aligne sur le réseau
    int x = Mathf.RoundToInt(transform.position.x) + direction.x;
    int y = Mathf.RoundToInt(transform.position.y) + direction.y;
    transform.position = new Vector2(x, y);

    // Définir la prochaine heure de mise à jour en fonction de la vitesse
    nextUpdate = Time.time + (1f / (speed * speedMultiplier));
}

    public void Grow()
    {
        Transform segment = Instantiate(segmentPrefab);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    public void Grow2()
    {
        Transform segment = Instantiate(Prefab2);
        segment.position = segments[segments.Count - 1].position;
        segments.Add(segment);
    }

    

    public void ResetState()
    {
        direction = Vector2Int.right;
        transform.position = Vector3.zero;

        // Start at 1 to skip destroying the head
        for (int i = 1; i < segments.Count; i++) {
            Destroy(segments[i].gameObject);
        }

        // Clear the list but add back this as the head
        segments.Clear();
        segments.Add(transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++) {
            Grow();
        }
    }

    public bool Occupies(int x, int y)
    {
        foreach (Transform segment in segments)
        {
            if (Mathf.RoundToInt(segment.position.x) == x &&
                Mathf.RoundToInt(segment.position.y) == y) {
                return true;
            }
        }

        return false;
    }

    public void DestroyeAll()
{
    // Commencez par supprimer tous les segments sauf le premier
    for (int i = 1; i < segments.Count; i++)
    {
        Destroy(segments[i].gameObject);
    }
    // Ramenez le premier segment au centre
     segments[0].position = Vector3.zero;
}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("CoterMechan")) 
        {
            DestroyeAll();
        }
        if (other.gameObject.CompareTag("GoodKid"))
        {
            Grow();
        }else if (other.gameObject.CompareTag("BadKid")) 
        {
            Grow2();
        }  
        else if (other.gameObject.CompareTag("Obstacle"))
        {
            ResetState();
        }
        else if (other.gameObject.CompareTag("Wall"))
        {
            if (moveThroughWalls) {
                Traverse(other.transform);
            } else {
                ResetState();
            }
        }
    }

    private void Traverse(Transform wall)
    {
        Vector3 position = transform.position;

        if (direction.x != 0f) {
            position.x = Mathf.RoundToInt(-wall.position.x + direction.x);
        } else if (direction.y != 0f) {
            position.y = Mathf.RoundToInt(-wall.position.y + direction.y);
        }

        transform.position = position;
    }

}
