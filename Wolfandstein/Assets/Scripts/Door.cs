using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

    public Animator doorAnimator;
    public Book bs;
    public WeaponPickUp BowB;
    // Start is called before the first frame update
    void Start()
    {
    }


    public void Swap()
    {
        if (bs.BookIsRead == true) {
            if (doorAnimator.GetBool("Open") == false)
            {
                doorAnimator.SetBool("Open", true);
            }
            else if (doorAnimator.GetBool("Open") == true)
            {
                doorAnimator.SetBool("Open", false);
            }
        }else
        {
            FindObjectOfType<AudioManager>().Play("ICant");
        }
    }
    public void Exit()
    {
        if (BowB.BowPick == true)
        {
            if (doorAnimator.GetBool("Open") == false)
            {
                doorAnimator.SetBool("Open", true);
            }
            else if (doorAnimator.GetBool("Open") == true)
            {
                doorAnimator.SetBool("Open", false);
            }
        }
        else
        {
            FindObjectOfType<AudioManager>().Play("FindWeapon");
        }
    }
}
