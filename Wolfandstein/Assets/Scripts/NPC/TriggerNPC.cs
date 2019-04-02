using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerNPC : MonoBehaviour {

    private GameObject triggeringNPC;
    private bool triggering;

    public GameObject npcText;

    void Update()
    {
        if (triggering)
        {
            npcText.SetActive(true);


            if (Input.GetKeyDown(KeyCode.E))
            {
                FindObjectOfType<AudioManager>().Play("Test");
            }
        }else
        {
            npcText.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "NPC")
        {
            triggering = true;
            triggeringNPC = other.gameObject;
        } 
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            triggering = false;
            triggeringNPC = null;
        }
    }
}
