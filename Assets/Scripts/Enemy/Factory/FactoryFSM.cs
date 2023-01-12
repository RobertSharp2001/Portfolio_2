using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryFSM : FiniteStateMachine{
    [HideInInspector]
    public FactoryActive movingState;
    [HideInInspector]
    public FactoryIdle idleState;
    [HideInInspector]
    public DeadState deadState;

    public Spawner spawns;
    public bool triggered = false;

    private void Awake() {
        
        movingState = new FactoryActive(this);
        idleState = new FactoryIdle(this);
        deadState = new DeadState(this);

        spawns.objectPool = ReferenceManager.Instance.dronePool.GetComponent<ObjectPool_Simple>();

        //firePoint = (transform.Find("Enemy_Sprite").Find("FirePoint")).gameObject;
    }

    protected override BaseState GetInitialState(){
        return idleState;
    }

    public bool CheckHealth(){
        return health.dead;
    }

    public void doSpawns()   {
        if (triggered) {
            if (spawns.currentSpawns < spawns.maxSpawns) {
                spawns.objectPool.updateSpawnArea(this.gameObject);
                spawns.getObject();
            }
            GameObject[] drones = GameObject.FindGameObjectsWithTag("Drone");
            if (drones != null) {
                if (drones.Length < spawns.currentSpawns)
                {
                    spawns.currentSpawns = drones.Length;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision){
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player")){
            //Debug.Log(collision.tag);
            //Add it to the list
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player")){
            //Debug.Log(collision.tag);

            //Change this to change the state
            triggered = false;
        }
    }


    private void OnTriggerStay2D(Collider2D collision) {
        if (collision.CompareTag("Player_Bullet") && Vector3.Distance(transform.position, collision.transform.position) < 0.5f && !iframe){
            //Debug.Log("Player Hit");
            Destroy(collision.gameObject);
            health.currentHealth--;
            iframe = true;
        }
    }

    public override BaseState FindState(string name) {
        if (name == "Idle"){
            return idleState;
        } else if (name == "Active") {
            return movingState;
        }

        return null;
    }
}
