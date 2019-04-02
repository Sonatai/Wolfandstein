using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.PostProcessing;

public class ES_Level1 : MonoBehaviour {
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private GameObject WeaponSwitch;
    private int load = 1;
    private PlayerStat stat;
    private int state = 1;
    private float count = 0;
    [SerializeField]
    private PostProcessingProfile blur;
    [SerializeField]
    private PostProcessingBehaviour CameraBehavior;
    [SerializeField]
    private GameObject UI;
    [SerializeField]
    private Healthbar_Manager script;
    [SerializeField]
    private ES_Arena arenaScript;
    Scene currentScene;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        stat = GameObject.Find("PlayerStat").GetComponent<PlayerStat>();
        currentScene = SceneManager.GetActiveScene ();
    }
    private void Update() {
        if(!stat.GetAlive ()) {
            PlayerDied ();
        }
    }
    private void BestScore() {
        if (!PlayerPrefs.HasKey ("Highscore"))
            PlayerPrefs.SetInt ("Highscore", arenaScript.score);
        else {
            if(PlayerPrefs.GetInt("Highscore") < arenaScript.score) 
                PlayerPrefs.SetInt ("Highscore", arenaScript.score);     
        }

    }

    public void PlayerDied() {

        if (currentScene.name == "Arena")
            BestScore ();
        script.enabled = false;
        Player.GetComponent<PlayerMove> ().enabled = false;
        GameObject.Find("PlayerCamera").GetComponent<PlayerLook> ().enabled = false;
        WeaponSwitch.SetActive(false);
        CameraBehavior.profile = blur;
        UI.SetActive (false);

        if (load == 1) {
            StartCoroutine (KnockOut ());
        } else if (load == 2) {
            StartCoroutine (DoFade ());
        }
        else if (load == 3) {
            SceneManager.LoadScene ("Start");
        }
       
        
    }
    IEnumerator KnockOut() {
        count += Time.deltaTime;
        if (state == 1) {
            Player.transform.eulerAngles = new Vector3 (Player.transform.eulerAngles.x, Player.transform.eulerAngles.y, 2);
            if ( count >= 2f) {
                count = 0;
                state = 2;
            }
        } else if (state == 2) {
            Player.transform.eulerAngles = new Vector3 (Player.transform.eulerAngles.x, Player.transform.eulerAngles.y, -3);
            if (count >= 2f) {
                count = 0;
                state = 3;
            }
        } else if (state == 3) {
            Player.transform.eulerAngles = new Vector3 (Player.transform.eulerAngles.x, Player.transform.eulerAngles.y, 5);
            if (count >= 2f) {
                count = 0;
                state = 4;
            }
        } else if (state == 4) {
            Player.transform.eulerAngles = new Vector3 (Player.transform.eulerAngles.x, Player.transform.eulerAngles.y, -10);
            if (count >= 2f) {
                count = 0;
                state = 5;
            }
        } else if (state == 5) {
            Player.transform.eulerAngles = new Vector3 (Player.transform.eulerAngles.x, Player.transform.eulerAngles.y, 90);
            if (count >= 2f) {
                count = 0;
                state = 6;
            }
        } else {
            load = 2;
            yield return null;
        }

    }
    IEnumerator DoFade() {
        CanvasGroup Black = GameObject.Find("Black_Image").GetComponent<CanvasGroup>();
        while(Black.alpha < 1) {
            Black.alpha += Time.deltaTime / 200;
            yield return null;
        }
        load = 3;
        yield return null;
        
    }
}
