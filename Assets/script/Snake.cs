using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider2D))]
public class Snake : MonoBehaviour
{
    public Vector3Int direction = Vector3Int.right;
    public Transform segmentPrefab;
    public Transform Prefab2;
    
    public float speed = 15f;
    public float speedMultiplier = 1f;
    public int initialSize = 1;
    public bool moveThroughWalls = false;

    public int gentil = 0;
    public int mechant = 0;

    public int ScoreGentil = 0;
    public int ScoreMehant = 0;

    public int FinalScore = 0;

    [SerializeField]
    private List<Transform> segments = new List<Transform>();
    private Vector2Int input;
    private float nextUpdate;

    private void Start()
    {
        gentil = 0;
        mechant = 0;
        ScoreGentil = 0;
        ScoreMehant = 0;
        
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
        direction = new Vector3Int(input.x, input.y, 0);
    }

    // Définir la position de chaque segment pour être la même que celui qu'il suit.
    // Nous devons faire cela dans l'ordre inverse afin que la position soit d��finie à la position précédente,
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
    // Vérifiez si la liste des segments est vide ou si la dernière position est valide
    if (segments.Count == 0 || (segments.Count > 0 && segments[segments.Count - 1]!= null))
    {
        gentil++;
        Debug.Log("segment count " + segments.Count);
        Transform segment = Instantiate(segmentPrefab);
        // Pour le premier segment, positionnez-le à la position actuelle du snake
        if (segments.Count == 0)
        {
            segment.position = transform.position;
        }
        else
        {
            // Sinon, positionnez le nouveau segment juste après le dernier
            // Convertir direction en Vector3 avant l'addition
            segment.position = segments[segments.Count - 1].position - direction;
        }
        segments.Add(segment);
        Debug.Log("gentil" + gentil);
    }
}

    public void Grow2()
    {
    // Vérifiez si la dernière position est valide avant d'ajouter un nouveau segment
    if (segments.Count > 0 && segments[segments.Count - 1]!= null)
    {
        mechant++;
        Transform segment = Instantiate(Prefab2);
        // Pour le premier segment, positionnez-le à la position actuelle du snake
        if (segments.Count == 0)
        {
            segment.position = transform.position;
        }
        else
        {
            // Sinon, positionnez le nouveau segment juste après le dernier
            // Convertir direction en Vector3 avant l'addition
            segment.position = segments[segments.Count - 1].position - direction;
        }
        segments.Add(segment);
        Debug.Log("mechant" + mechant);
    }
    }

    

    public void ResetState()
    {
    gentil = 0;
    mechant = 0;
    ScoreGentil = 0;
    ScoreMehant = 0;
    

    direction = new Vector3Int(1, 0, 0);
    transform.position = Vector3.zero;

    // Commencez par supprimer tous les segments sauf le premier
    for (int i = 1; i < segments.Count; i++)
    {
        Transform segment = segments[i];
        if (segment!= null) // Vérifiez si le segment existe avant de le détruire
        {
            Destroy(segment.gameObject);
        }
    }

    // Ajoutez le premier segment au centre
    if (segments.Count > 0) // Assurez-vous qu'il reste au moins un segment
    {
        segments[0].position = Vector3.zero;
    }

    // Remplissez la liste avec les segments initiaux
    segments.Clear();
    segments.Add(transform); // Ajoutez le corps principal

    for (int i = 0; i < initialSize - 1; i++)
    {
        Grow(); // Appelle Grow pour ajouter des segments supplémentaires
    }
    }



    public bool Occupies(int x, int y)
    {
    foreach (Transform segment in segments)
    {
        // Vérifiez si le segment existe avant d'y accéder
        if (segment!= null && Mathf.RoundToInt(segment.position.x) == x &&
            Mathf.RoundToInt(segment.position.y) == y)
        {
            return true;
        }
    }

    return false;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.gameObject.CompareTag("END")){
            if(FinalScore >= 6)
        {
            SceneManager.LoadScene(6);
        }else {
            Start();
        }
        }

        
        if (other.gameObject.CompareTag("CoterGentil")){
            
            if (mechant == 0 && gentil >= 10){
                ScoreGentil++;
                FinalScore += ScoreGentil;

                ResetState();
            }else{
                ResetState();
            }
            
            
        }

        if (other.gameObject.CompareTag("CoterMechan")){
            
            if (gentil == 0 && mechant >= 10){
                ScoreMehant++;
                FinalScore += ScoreMehant;
                ResetState();
            }else{
                ResetState();
            }
            
            
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
