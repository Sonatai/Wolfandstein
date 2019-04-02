using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyChase : MonoBehaviour {


    //"Public" Variabeln
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private AudioClip [] footsounds;
    [SerializeField]
    private AudioSource Rawrr;
    private GameObject Player;

    public float MS;
    [SerializeField]
    private float sprintSpeed;
    [SerializeField]
    private float animationSpeed;
    [SerializeField]
    private float animationSprintSpeed;
    [SerializeField]
    private float sichtfeld;
    [SerializeField]
    private float sichtweite;
    [SerializeField]
    private float angriffsreichweite;
    [SerializeField]
    private float patient;

    [SerializeField]
    private Vector3 [] waypoints;

    //Private Variabeln
    private AudioSource sound;
    private EnemyStat stat;
    private float[] health;
    private NavMeshAgent nav;
    int waypointCount = 0;
    private float isIdleCount = 0f;
    RaycastHit rayHit;
    private bool hadRawr = false;
    private bool isSearching = false;
    private bool lookAround = false;
    private float isLookAround = 3f;
    private float curPatient = 0f;
    private Vector3 randomPos;
    private NavMeshHit navHit;
 

    void Start () {
        animator = GetComponent<Animator>();
        stat = GetComponent<EnemyStat>();
        nav = GetComponent<NavMeshAgent> ();
        sound = GetComponent<AudioSource> ();
        nav.stoppingDistance = angriffsreichweite;
        nav.speed = MS / 10f;
        Player = GameObject.FindWithTag ("Player");
       // animator.speed = animationSpeed;
    }

    //... Footstep - Sound
    public void footstep(int _num) {
        sound.clip = footsounds [_num];
        sound.Play ();
    }

    //... Set the isSearching - variable
    public void setIsSearching(bool value) {
        isSearching = value;
    }

    //... Check if enemy reached the destination
    public bool CheckDestinationReached(Vector3 destination) {

        Vector3 mydes = new Vector3 (destination.x, 0, destination.z);
        Vector3 mypos = new Vector3 (transform.position.x, 0, transform.position.z);
        float distanceToTarget = Vector3.Distance (mypos, mydes);
        if (distanceToTarget < nav.stoppingDistance) {
            return true;
        }else{
            return false;
        }
    }

    void Update () {

        //... Check if dead
        health = stat.getHealthValues();
        if (health[1] <= 0) {
            return;
        }

        //... calculate the distance to the player
        Vector3 direction = Player.transform.position - this.transform.position; 

        //... calculate the angle  betweend enemy and player
        float angle = Vector3.Angle(direction, this.transform.forward);
        
        //... Sichtweite Debug
        //Vector3 target = new Vector3 (transform.position.x, transform.position.y, (transform.position.z + sichtweite));
       // Debug.DrawLine (transform.position,target,Color.green);

        //... CHECK IF PLAYER IN VIEW FIELD ...//
        if (Vector3.Distance (Player.transform.position, this.transform.position) <= sichtweite && angle <= sichtfeld && Physics.Linecast (this.transform.position, Player.transform.position, out rayHit) && rayHit.collider.tag == "Player") {

            //... controll if it should search the player
            isSearching = true;

            //... make a strange noise to say "Hey I know u are here ;)"
            if (!hadRawr) {
                hadRawr = true;
                Rawrr.Play ();
            }
            navHit.position = this.transform.position;
            direction.y = 0f;
            //NPC Look to the player in a fluid movement
            this.transform.rotation = Quaternion.Slerp (this.transform.rotation, Quaternion.LookRotation (direction), 0.1f);

            animator.SetBool ("isIdle", false); 

            //... CHECK IF THE DISTANCE IS HIGHER THAN HIS ATTACKRANGE ...//
            if (direction.magnitude > angriffsreichweite) {

                nav.isStopped = false;
                nav.SetDestination (Player.transform.position);
                animator.SetBool ("isRun", true);
                animator.SetBool ("isAttack", false);

            } else {
                nav.isStopped = true;
                animator.SetBool ("isRun", false);
                animator.SetBool ("isAttack", true);
            }
        } //... SEARCH THE PLAYER ...//
        else if (isSearching) {

            //... make a strange noise to say "Hey I know u are here ;)"
            if (!hadRawr) {
                hadRawr = true;
                Rawrr.Play ();
            }

            //... patient determant how long the enemy search
            if (curPatient < patient) {

                curPatient += Time.deltaTime;
                /*
                 1. Look around
                 2. Find a new random path around the player
                 3. start by 1
                 * the range depends on how long enemy is searching
                 * radius get higher how longer enemy is searching!
                 */
                
                if(curPatient >= 15f && !lookAround) {
                    if (CheckDestinationReached (navHit.position )) {
                         randomPos = Random.insideUnitSphere * 20f;
                        NavMesh.SamplePosition (Player.transform.position + randomPos, out navHit, 20f, NavMesh.AllAreas);
                        lookAround = true;
                    }

                    nav.isStopped = false;
                    animator.SetBool ("isIdle", false);
                    animator.SetBool ("isRun", true);
                    animator.SetBool ("isAttack", false);
                    nav.SetDestination (navHit.position);
  
                } else if(curPatient >= 10f && !lookAround) {
                    if (CheckDestinationReached (navHit.position)) {
                         randomPos = Random.insideUnitSphere * 15f;
                        NavMesh.SamplePosition (Player.transform.position + randomPos, out navHit, 15f, NavMesh.AllAreas);
                        lookAround = true;
                    }

                    nav.isStopped = false;
                    animator.SetBool ("isIdle", false);
                    animator.SetBool ("isRun", true);
                    animator.SetBool ("isAttack", false);
                    nav.SetDestination (navHit.position);
                    

                } else if(curPatient >= 0f && !lookAround) {
                    if (CheckDestinationReached (navHit.position)) {
                         randomPos = Random.insideUnitSphere * 10f;
                        NavMesh.SamplePosition (Player.transform.position + randomPos, out navHit, 10f, NavMesh.AllAreas);
                        lookAround = true;
                    }

                    nav.isStopped = false;
                    animator.SetBool ("isIdle", false);
                    animator.SetBool ("isRun", true);
                    animator.SetBool ("isAttack", false);
                    nav.SetDestination (randomPos);
                    
                }

                if((isLookAround > 0f && lookAround)){
                    isLookAround -= Time.deltaTime;
                    nav.isStopped = true;
                    transform.Rotate (0f, 120f * Time.deltaTime/4, 0f);
                    animator.SetBool ("isIdle", true);
                    animator.SetBool ("isRun", false);
                    animator.SetBool ("isAttack", false);
                    
                } else {
                    lookAround = false;
                    isLookAround = 3f;
                }
    
            }else {

                //... reset
                curPatient = 0;
                isSearching = false;
                nav.isStopped = false;
            }
        } //... PATHFINDING/PATROL ...//
        else {
            if (waypoints.Length != 0) {

                hadRawr = false;

                //... if enemy reach the destination, set new destination
                if (CheckDestinationReached (waypoints[waypointCount])) {
                    waypointCount++;
                }

                //... Reset if reach end of array
                if (waypointCount >= waypoints.Length) {
                    waypointCount = 0;
                }

                //... wait for 1 sec
                if (waypoints [waypointCount].x == 90000) {
                    animator.SetBool ("isIdle", true);
                    animator.SetBool ("isRun", false);
                    animator.SetBool ("isAttack", false);
                    isIdleCount += Time.deltaTime;
                    if (isIdleCount >= 1f) {
                        isIdleCount = 0;
                        waypointCount++;
                    }
                } //... wait for 10sec
                else if (waypoints [waypointCount].x == 100000) {
                    animator.SetBool ("isIdle", true);
                    animator.SetBool ("isRun", false);
                    animator.SetBool ("isAttack", false);
                    isIdleCount += Time.deltaTime;
                    if (isIdleCount >= 10f) {
                        isIdleCount = 0;
                        waypointCount++;
                    }
                } //... wait for 5sec
                else if (waypoints [waypointCount].x == 150000) {
                    animator.SetBool ("isIdle", true);
                    animator.SetBool ("isRun", false);
                    animator.SetBool ("isAttack", false);
                    isIdleCount += Time.deltaTime;
                    if (isIdleCount >= 5f) {
                        isIdleCount = 0;
                        waypointCount++;
                    }
                } //... switch to idle (for ever)
                else if (waypoints [waypointCount].x == 200000) {
                    animator.SetBool ("isIdle", true);
                    animator.SetBool ("isRun", false);
                    animator.SetBool ("isAttack", false);
                } else if (waypoints [waypointCount].x == 9999) {
                    isSearching = true;
                }//... go to the destination
                else {

                    nav.SetDestination (waypoints [waypointCount]);
                    animator.SetBool ("isIdle", false);
                    animator.SetBool ("isRun", true);
                    animator.SetBool ("isAttack", false);
                }



            } //... IF IT HAS NO PATH ...//
            else {

                hadRawr = false;
                animator.SetBool ("isIdle", true);
                animator.SetBool ("isRun", false);
                animator.SetBool ("isAttack", false);

            }


        }
	}

}
