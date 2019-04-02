using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;

public class ES_Arena : MonoBehaviour
{
    private int waveCount = 1;
    public int score = 0;
    private double pause = 3f;
    public bool isStarting = false;
    private Transform spawnPoint1;
    private Transform spawnPoint2;
    private Transform spawnPoint3;
    public float msMultip = 1f;
    public float hpMultip = 1f;

    public enum SpawnSate {SPAWNING,WAITING,COUNTING};
    [System.Serializable]
    public class Wave {
        public string name;
        public Transform enemy1;
        public Transform enemy2;
        public int count;
    }

    public Wave [] waves;
    public SpawnSate state = SpawnSate.COUNTING;
    private void Start() {
        spawnPoint1 = GameObject.Find ("Spawnpoint1").transform;
        spawnPoint2 = GameObject.Find ("Spawnpoint2").transform;
        spawnPoint3 = GameObject.Find ("Spawnpoint3").transform;
    }

    [SerializeField]
    private Text scoreField;

    private void Update() {
        scoreField.text = "Wave: "+waveCount.ToString()+ "\nScore: "+score.ToString();
        if (isStarting) {
            
            if (state == SpawnSate.WAITING) {
                if (!EnemyIsAlive ()) {
                    pause = 3f;
                    hpMultip += 0.2f;
                    msMultip += 0.2f;
                    waves [0].count += 1;
                    waveCount += 1;
                    state = SpawnSate.COUNTING;
                }else {
                    return;
                }
            } 

            if(pause <= 0f) {
                if(state == SpawnSate.COUNTING) {
                    StartCoroutine (SpawnWave(waves[0],spawnPoint1));
                    StartCoroutine (SpawnWave (waves [0], spawnPoint2));
                    StartCoroutine (SpawnWave (waves [0], spawnPoint3));
                }
            } else {
                pause -= Time.deltaTime;
            }

        }
    }

    bool EnemyIsAlive() {
        if(GameObject.FindWithTag("heavy") == null && GameObject.FindWithTag("light") == null) {
            return false;
        }
        return true;
    }

   IEnumerator SpawnWave(Wave _wave,Transform spawnpoint) {

        state = SpawnSate.SPAWNING;

        for(int i = 0; i < _wave.count; i++) {
            SpawnEnemy (_wave.enemy1,spawnpoint);
        }

        for (int i = 0; i < _wave.count/2; i++) {
            SpawnEnemy (_wave.enemy2, spawnpoint);
        }

        state = SpawnSate.WAITING;

        yield break;
   }

    void SpawnEnemy(Transform _enemy, Transform spawnPoint) {
        Debug.Log ("Spawning Enemy: " + _enemy.name);
        Transform Enemy= Instantiate (_enemy, spawnPoint.position, spawnPoint.rotation) as Transform;             
        EnemyChase enemyChase = Enemy.GetComponent<EnemyChase> ();
        EnemyStat enemyStat = Enemy.GetComponent<EnemyStat> ();
        if (Enemy.CompareTag ("light")) {
            enemyChase.MS *= msMultip;
        } else if (Enemy.CompareTag ("heavy")) {
            enemyStat.maxHealth *= hpMultip;
        }
        enemyChase.setIsSearching (true);
    }

}
