using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyStat : MonoBehaviour {

    [SerializeField]
    private string name;
    [SerializeField]
    private GameObject Flame;
    public float maxHealth;
    private float curHealth;
    [SerializeField]
    private int deff;
    private Animator animator;
    public bool isDead = false;
    private bool onFlame = false;
    Scene currentScene ;
    string sceneName ;
    private double despawnTimer = 30f;


    public void Start()
    {
        animator = GetComponent<Animator>();
        curHealth = maxHealth;
        currentScene = SceneManager.GetActiveScene ();
        sceneName = currentScene.name;

    }

    private void Update() {
        if (isDead) {
            Debug.Log (despawnTimer);
            despawnTimer -= Time.deltaTime;
            if (despawnTimer <= 0f) {
                Destroy (gameObject);
            }
        }

        if (onFlame) {
            Flame.SetActive (true);
        }
    }

    //... HP CALCULATION ...//
    public void Hit(float damage) {
        if(isDead == false) { 
            
            curHealth -= damage;
            

            if (curHealth <= 0)
            {
                if (!isDead) {
                    animator.SetBool ("isDead", true);
                    isDead = true;
                    if (sceneName == "Arena") {
                        ES_Arena arena = GameObject.Find ("WaveSpawner").GetComponent<ES_Arena> ();
                        arena.score += 1;
                    }
                    
                }

            } else {
                //... tell him to search
                this.GetComponent<EnemyChase> ().setIsSearching (true);
            }
            
        }

        
    }

    //... Some Getter and Setter
    public float[] getHealthValues()
    {
        float[] Values = { maxHealth, curHealth };
        return Values;
    }

    public float getCurrentHealth() {
        return curHealth;
    }

    public string getName()
    {
        return name;
    }

    public void SetFlameOn(bool state)
    {
        onFlame = state;
    }

    public bool GetFlameOn()
    {
        return onFlame;
    }
    
}
