using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickUp : MonoBehaviour
{
    public GameObject playerWeapon;
    public GameObject groundWeapon;
    public bool BowPick;

    // Start is called before the first frame update
    void Start()
    {
        playerWeapon.SetActive(false);
        groundWeapon.SetActive(true);
        BowPick = false;
    }

    public void PickUp()
    {
        playerWeapon.SetActive(true);
        groundWeapon.SetActive(false);
        BowPick = true;
    }
}
