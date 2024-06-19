using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courroite : MonoBehaviour
{
    private Animator anim;
    public GameObject personage;
    public int vitesse = 1;
    public int positionDelay = 10; // Maximum number of positions to keep in the queue

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
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
            }
            else
            {
                anim.SetBool("courdroite", false);
            }

            if (Input.GetKey(KeyCode.LeftArrow) && !Input.GetKey(KeyCode.UpArrow) && !Input.GetKey(KeyCode.DownArrow))
            {
                anim.SetBool("courgauche", true);
                personage.transform.Translate(Vector3.left * vitesse * Time.deltaTime);
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
            }
            else
            {
                anim.SetBool("courdevant", false);
            }

            if (Input.GetKey(KeyCode.DownArrow) && !Input.GetKey(KeyCode.RightArrow) && !Input.GetKey(KeyCode.LeftArrow))
            {
                anim.SetBool("courderrier", true);
                personage.transform.Translate(Vector3.down * vitesse * Time.deltaTime);
            }
            else
            {
                anim.SetBool("courderrier", false);
            }
        }
    }
}