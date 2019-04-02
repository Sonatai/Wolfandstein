using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class arrow_flaming : Arrow {

    protected float dot;
    protected bool state = false;
    private bool isLighted = false;
    [SerializeField]
    private GameObject Flame;
    private bool collision = false;

    public arrow_flaming()
    {
        l_damage = 1;
        h_damage = 1;
        dot = 3;

    }

    private void OnTriggerEnter(Collider col)
    {

        //Look forfire source
        if (col.CompareTag ("FireSource")) {
            isLighted = true;
            Flame.SetActive (true);
        }

       

        //Look if Enemy Collider
        if (col.GetComponentInParent<EnemyStat> () && !collision)
        {   //If light,heavy immortal -> do different damage


            EnemyStat stat = col.GetComponentInParent<EnemyStat> ();
            //Initial Damage
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

            //Dot Damage

            if (stat.GetFlameOn() == false && !state && isLighted) {
                if (stat.tag == "light") {
                    StartCoroutine (DOT (dot, stat, 1));
                    state = true;
                } else if (stat.tag == "heavy") {
                    StartCoroutine (DOT (dot , stat, 2));
                    state = true;
                } else if (stat.tag == "immortal")
                    state = true;
                //Pfeile prallen ab.
            }

            float[] EnemyHealth = stat.getHealthValues();
            Healthbar_Manager EnemyHealthbar = GameObject.Find("Healthbar_Manager").GetComponent<Healthbar_Manager>();
            EnemyHealthbar.setValues(EnemyHealth[0], EnemyHealth[1]);
            //Ballistic script = GetComponent<Ballistic> ();
            //script.hitSomething = true;


        }
        collision = true;
    }

    IEnumerator DOT(float damage, EnemyStat stat, int waiting)
    {
        int damageCount = 0;
        stat.SetFlameOn(true);

        while (damageCount <= 10)
        {
            stat.Hit(damage);
            float[] EnemyHealth = stat.getHealthValues();
            Healthbar_Manager EnemyHealthbar = GameObject.Find("Healthbar_Manager").GetComponent<Healthbar_Manager>();
            EnemyHealthbar.setValues(EnemyHealth[0], EnemyHealth[1]);
            yield return new WaitForSeconds(waiting);
            damageCount++;
        }
        stat.SetFlameOn(false);
        gameObject.SetActive (true);
    }

}
