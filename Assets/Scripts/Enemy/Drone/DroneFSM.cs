using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneFSM : FiniteStateMachine{
    // Start is called before the first frame update

    [HideInInspector]
    public DroneIdle idleState;
    [HideInInspector]
    public DroneActive movingState;
    [HideInInspector]
    public DeadState deadState;

    public bool triggered;
    public GameObject _target;
    public float turnspeed;
    public float targetVelocity = 10.0f;

    public bool canDamage = true;
    private void Awake() {
        idleState = new DroneIdle(this);
        movingState = new DroneActive(this);
        deadState = new DeadState(this);
    }

    public bool CheckHealth(){
        return health.dead;
    }

    protected override BaseState GetInitialState(){
        return idleState;
    }

    public void OnEnable(){
        canDamage = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player")) {
            //Debug.Log(collision.tag);
            //Add it to the list
            _target = collision.gameObject;

            //Change this to change the state
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player")) {
            //Debug.Log(collision.tag);
            //Add it to the list
            _target = null;

            //Change this to change the state
            triggered = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision) {
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player") && canDamage && !GameManager.IFrame && Vector3.Distance(transform.position, collision.transform.position) < 1f) {
            Debug.Log("Should be a hit");
            canDamage = false;
            GameManager.lowerHealth();
            health.currentHealth = 0;
            Invoke(nameof(destory),0.1f);
        }
        if (collision.CompareTag("Asteroid")){
            health.currentHealth = 0;
            Invoke(nameof(destory), 0.1f);
        }
    }

    public void MoveToPlayer(){
        if (_target == null) {
            return;
        }

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);

        float angle = Quaternion.Angle(transform.rotation, rotation);

        //look at the player
        transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, turnspeed * Time.deltaTime);
        //move toward the player
        transform.localPosition = Vector3.Lerp(transform.position, _target.transform.position, targetVelocity * Time.deltaTime);
    }
}
