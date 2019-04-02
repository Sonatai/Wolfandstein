using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Interact : MonoBehaviour {


    public string interactButton;

    public float interactDistance = 3f;
    public LayerMask interactLayer; //filter

    public Image interactIcon;

    public bool isInteracting;

    // Use this for initialization
    void Start()
    {
        if (interactIcon != null)
        {
            interactIcon.enabled = false;

            
        }
    }
	// Update is called once per frame
	void Update () {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactDistance, interactLayer))
        {
            if(isInteracting == false)
            {
                if (interactIcon != null)
                {
                    interactIcon.enabled = true;
                }
               

                    if(Input.GetButtonDown(interactButton))
                    {
                        if (hit.collider.CompareTag("Book"))
                        {
                            hit.collider.GetComponent<Book>().ShowBookImage();
                        } 
                            else if (hit.collider.CompareTag("Door"))
                            {
                                hit.collider.GetComponent<Door>().Swap();
                            }
                            else if (hit.collider.CompareTag("Stone"))
                            {
                        Debug.Log ("Hallo, das ist ein Stein");
                                ES_Arena script = GameObject.Find ("WaveSpawner").GetComponent<ES_Arena> ();
                                script.isStarting = true;
                            }
                            else if (hit.collider.CompareTag("Weapon"))
                            {
                                hit.collider.GetComponent<WeaponPickUp>().PickUp();
                            }
                            else if (hit.collider.CompareTag("Exit"))
                            {
                                hit.collider.GetComponent<Door>().Exit();
                            }
                    }
            } 
        }
	}
}
