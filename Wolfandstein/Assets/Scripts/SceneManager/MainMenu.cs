using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI scoreText;

    
   
    private void Start() {
        scoreText = GameObject.Find ("Highscore").GetComponent<TextMeshProUGUI> ();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (PlayerPrefs.HasKey ("Highscore"))
            scoreText.text = "Highscore " + ((int) PlayerPrefs.GetInt ("Highscore")).ToString ();
        else
            scoreText.text = "Highscore 0";
    }
    public void PlayGame() {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().buildIndex + 1);
    }
   
}
