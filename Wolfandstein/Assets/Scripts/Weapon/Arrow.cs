using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arrow : MonoBehaviour {

    protected float l_damage;
    protected float h_damage;
    private float count = 0f;
    [SerializeField]
    private float despawnCount = 60f;
    protected float charge = 0f;
 
    private Text EnemyNameField;

    public void setCharge(float value) {
        charge = value;
    }

    private void Update()
    {
       count += Time.deltaTime;
        if (count > despawnCount)
        {
            Destroy(gameObject);
        }
    }

    //Look for Collider
    private void OnTriggerEnter(Collider col)
    {
        //Look if Enemy Collider
        //If light,heavy immortal -> do different damage
        

        if (col.GetComponentInParent<EnemyStat> ()) {
            EnemyStat stat = col.GetComponentInParent<EnemyStat> ();
            //Damaaaaggee
            if (col.CompareTag ("Head")) {
                Debug.Log ("Headshot");

                if (stat.tag == "light") {
                    stat.Hit (l_damage * (3 + charge / 100));

                } else if (stat.tag == "heavy") {
                    stat.Hit (h_damage * (3 + charge / 100));

                } else if (stat.tag == "immortal") ;
                //Pfeile prallen ab.

            } else if (col.CompareTag ("Body")){
                Debug.Log ("Bodyshot");
               
                if (stat.tag == "light") {
                    stat.Hit (l_damage * (1 + charge / 100));

                } else if (stat.tag == "heavy") {
                    stat.Hit (h_damage * (1 + charge / 100));

                } else if (stat.tag == "immortal") ;
                //Pfeile prallen ab.
      
            }

            EnemyNameField = GameObject.Find ("Enemy_Name_Text").GetComponent<Text> ();
            EnemyNameField.text = stat.getName ();
            float [] EnemyHealth = stat.getHealthValues ();
            Healthbar_Manager EnemyHealthbar = GameObject.Find ("Healthbar_Manager").GetComponent<Healthbar_Manager> ();
            EnemyHealthbar.setValues (EnemyHealth [0], EnemyHealth [1]);
        }

        
        gameObject.SetActive (true);

    }
     
    
}

