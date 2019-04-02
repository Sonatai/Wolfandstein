using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar_Manager : MonoBehaviour {

    [SerializeField]
    private Slider View;
    [SerializeField]
    private GameObject Healthbar;
    private bool functionActiv;
    private float count;

    private void Start()
    {
        Healthbar.SetActive(false);
        View.maxValue = 200;
        View.value = 200;
        count = 0;
        functionActiv = false;
    }

    private void Update()
    {
        
        count += Time.deltaTime;
    
        if (count > 10)
        {
            functionActiv = false;
        }

        if (functionActiv == true)
        {
            Healthbar.SetActive(true);
            
        }else if (functionActiv == false)
        {
            Healthbar.SetActive(false);
            Text EnemyNameField = GameObject.Find ("Enemy_Name_Text").GetComponent<Text> ();
            EnemyNameField.enabled = false;
            count = 0f;
        }

    }

    //... In Arrow Script does set values
    public void setValues(float max, float value)
    {
        View.maxValue = max;
        View.value = value;
        functionActiv = true;


    }

   
   
	
	
}
