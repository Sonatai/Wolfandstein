using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour {

  
    [SerializeField]
    private int damage;
    EnemyStat stat;

    private void Start() {
       stat = this.GetComponentInParent<EnemyStat>();
    }

    void OnTriggerEnter(Collider col) {
        
        if (col.GetComponent<PlayerStat>()) {
            
            //Check if is dead
            if (!stat.isDead) { 
                PlayerStat stats = col.gameObject.GetComponent<PlayerStat> ();
                stats.Hit (damage); //in Playerstat
            }
        } 

     }


}
