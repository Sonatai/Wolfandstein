using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : MonoBehaviour {

    [SerializeField]
    private float health;
    [SerializeField]
    private int deff;
    [SerializeField]
    private Slider healthbar;
    bool alive = true;

    public void Start()
    {
        if (healthbar != null)
        {
            healthbar.maxValue = health;
            healthbar.value = health;
        }
    }

    //... HP CALCULATION ...//
    public void Hit(int damage)
    {
        healthbar.value -= damage;
        health -= damage;
        if (health <= 0) {
            alive = false;
        }
    }

    public bool GetAlive() {
        return alive;
    }
}
