using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSwitch : MonoBehaviour {

    private int weaponID;
    [SerializeField]
    private GameObject weapon1;
    [SerializeField]
    private GameObject weapon2;
    [SerializeField]
    private GameObject weapon3;
    [SerializeField]
    private Text Waffenart;

    private void Start()
    {
        weapon1.SetActive(true);
        weapon2.SetActive(false);
        weapon3.SetActive(false);
        weaponID = 0;
        Waffenart.text = "Standard Arrow";
       
    }
    private void Update()
    {
        if (Input.GetAxisRaw("Mouse ScrollWheel") > 0) {
            weaponID += 1;
        } else if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
        {
            weaponID -= 1;
        }
        if (weaponID % 3 == 0)
        {
            weapon1.SetActive(true);
            weapon2.SetActive(false);
            weapon3.SetActive(false);
            Waffenart.text = "Standard Arrow";
        }
        else if(weaponID % 3 == 1) {

            weapon1.SetActive(false);
            weapon2.SetActive(true);
            weapon3.SetActive(false);
            Waffenart.text = "Bodkin Arrow";
        }
        else if(weaponID % 3 == 2)
        {
            weapon1.SetActive(false);
            weapon2.SetActive(false);
            weapon3.SetActive(true);
            Waffenart.text = "Flaming Arrow";
        }
    }
}
