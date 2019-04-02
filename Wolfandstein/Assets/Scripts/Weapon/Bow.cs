using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bow : MonoBehaviour
{

    float _charge = 10; //the mind. charge for the bow

    [SerializeField]
    private float chargeMax;  //the max value for the charge
    [SerializeField]
    private float chargeRate; //how fast the charge is

    [SerializeField]
    private KeyCode fireButton;

    [SerializeField]
    private Transform spawn;  //location to spawn
    private Transform correctSpawn;
    [SerializeField]
    private Rigidbody arrowObj; //its the arrow that spawn
    [SerializeField]
    private Slider healthbar;
    [SerializeField]
    private float attackSpeed;
    private float currAttackSpeed;

    private void Start() {
        currAttackSpeed = 0f;
     
    }

    private void Update()
    {

        if (healthbar.value <= 0)
        {
            return;
        }

        currAttackSpeed -= Time.deltaTime;

        if (Input.GetKey(fireButton) && _charge < chargeMax && currAttackSpeed <= 0f)
        {
            _charge += Time.deltaTime * chargeRate; //Time.deltaTime for the real chargeRate!
        }

        if (Input.GetKeyUp(fireButton) && currAttackSpeed <= 0f)
        {
           
            FindObjectOfType<AudioManager>().Play("bow");
            Rigidbody arrow = Instantiate(arrowObj, spawn.position, Quaternion.identity) as Rigidbody; //A arrow spawn on the spawn location
                                                                                                       //Arrow need the right rotation
            arrow.transform.Rotate (spawn.eulerAngles.x + Camera.main.transform.rotation.x, spawn.eulerAngles.y + Camera.main.transform.rotation.y, spawn.eulerAngles.z + Camera.main.transform.rotation.z);
            Arrow script = arrow.GetComponentInChildren<Arrow> ();
            script.setCharge (_charge);
            arrow.AddForce(spawn.forward * _charge, ForceMode.Impulse); //give the arrow the impulse to fly
            _charge = 10; //reset _charge
            currAttackSpeed = attackSpeed;
            
        }
    }
}
