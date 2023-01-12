using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]
    public int maxSpawns;
    public int currentSpawns;
    /*
    public float spawnCircleRadius = 20f;
    public float outerCircleRadius = 30f;
    */
    public GameObject gameArea;
    [SerializeField, Range(0,10)]
    private float timeToSpawn = 5f;
    private float timeSinceSpawn;

    public ObjectPool_Simple objectPool;

    private void Start() {
        //objectPool = FindObjectOfType<ObjectPool_Simple>();
    }

    private void Update() {
        /*
        timeSinceSpawn += Time.deltaTime;
        

        if(timeSinceSpawn >= timeToSpawn) {
            
            if(currentSpawns < maxSpawns){
                Debug.Log("active");

                currentSpawns++;

                GameObject newCritter = objectPool.GetCritter();
                KeepInRange keeps = newCritter.GetComponent<KeepInRange>();
                keeps.gameArea = gameArea;
                keeps.objectPool = objectPool;

            }

            timeSinceSpawn = 0f;
        }
        */
    }

    public void returnObject(GameObject go) {
        objectPool.returnCritter(go);
        currentSpawns--;
    }

    public void setGameArea(GameObject go) {
        gameArea = go;
    }

    public void getObject(){
        currentSpawns++;

        GameObject newCritter = objectPool.GetCritter();
        KeepInRange keeps = newCritter.GetComponent<KeepInRange>();
        keeps.gameArea = gameArea;
        keeps.objectPool = objectPool;
    }
}
