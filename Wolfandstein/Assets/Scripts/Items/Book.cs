using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Book : MonoBehaviour {

    public Image bookImage;
    public GameObject CloseBook;

    public AudioClip pickupSound;
    public AudioClip PutAwaySound;

    public GameObject playerObject;
    // private PlayerMove playerMoveScript;
    //public CharacterController Controller;
    public bool BookIsRead;

	// Use this for initialization
	void Start () {
        playerObject = GameObject.FindWithTag("Player");
        bookImage.enabled = false;
        CloseBook.SetActive(false);
        BookIsRead = false;
    }
	
	public void ShowBookImage()
    {
        bookImage.enabled = true;
        FindObjectOfType<AudioManager>().Play("pickupBook");

        CloseBook.SetActive(true);
        //playerMoveScript.enabled = false;
        playerObject.SetActive(false);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void HideBookImage()
    {
        bookImage.enabled = false;
        FindObjectOfType<AudioManager>().Play("putAwayBook");

        CloseBook.SetActive(false);

        playerObject.SetActive(true);
        //playerObject.GetComponent<PlayerMove>().enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        BookIsRead = true;
    }
}
