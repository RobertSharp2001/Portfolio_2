using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour{
    // Start is called before the first frame update

    public enum SpawnState{
        Spawning, Waiting, Counting
    }

    [System.Serializable]
    public class Wave {
        public string name;
        public List<EnemyList> list;
        public float rate;
    }

    [System.Serializable]
    public class EnemyList {
        public Spawner spawns;
        public int count;
    }

    public SpawnState state = SpawnState.Counting;

    public Wave[] waves;
    private int nextWave;

    public float timeBetweenWaves;
    public float waveCountdown;
    public float searchCountdown = 1f;

    public bool active;

    public void Start(){
        waveCountdown = timeBetweenWaves;
    }

    private void Update() {
        GameManager.wave = nextWave + 1;
        
        if (enabled){
            
            if(state == SpawnState.Waiting){
                if (!isEnemyAlive()) {
                    WaveCompleted();
                    GameManager.resetAmmo();
                } else {
                    return;
                }
            }
            
            
            if (waveCountdown <= 0) {
                
                if (state != SpawnState.Spawning) {
                    //Spawning code here
                    StartCoroutine(SpawnWave(waves[nextWave]));
                }
            } else {
                waveCountdown -= Time.deltaTime;
            }
        }

    }

    IEnumerator SpawnWave(Wave wave) {
        Debug.Log("Spawning wave: " + wave.name);
        
        state = SpawnState.Spawning;
        //For each item in the list

        foreach (var spawner in wave.list){
            Debug.Log(spawner.spawns);
            Debug.Log(spawner.count);
            for (int i = 0; i < spawner.count; i++) {
                Debug.Log("Spawning one: " + spawner.spawns);
                SpawnEnemy(spawner.spawns);
                yield return new WaitForSeconds(1f / wave.rate);
            }
        }


        state = SpawnState.Waiting;

        yield break;
    }

    void WaveCompleted(){
        Debug.Log("Wave finished");

        state = SpawnState.Counting;
        waveCountdown = timeBetweenWaves;

        if(nextWave + 1 > waves.Length - 1)  {
            nextWave = 0;
            ManagerScene.Instance.endGame();
        }

        nextWave++;
    }

    bool isEnemyAlive() {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0) {
            searchCountdown = 1f;
            Debug.Log(GameObject.FindGameObjectsWithTag("Enemy").Length);
            
            if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0){
                Debug.Log("Triggered");
                return false;
            }
        }
        return true;
    }

    void SpawnEnemy(Spawner spawn) {
        spawn.getObject();
    }
}
