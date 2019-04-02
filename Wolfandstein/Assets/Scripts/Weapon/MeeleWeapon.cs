using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MeeleWeapon : MonoBehaviour {
    [SerializeField]
    private KeyCode attackButton;
    [SerializeField]
    private int damage;
    [SerializeField]
    private Slider healthbar;

    //TestiTest


    void OnTriggerStay(Collider col) {
        if (Input.GetKeyDown(attackButton)) {
            //Debug.Log("Test");
            if (healthbar.value <= 0)
            {
                return;
            }
            if (col.GetComponent<EnemyStat>()) {
                Debug.Log("Test");
                EnemyStat stat = col.GetComponent<EnemyStat>();
                stat.Hit(damage);
            }
        }
    }
}
