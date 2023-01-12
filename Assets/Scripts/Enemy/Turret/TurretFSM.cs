using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretFSM : FiniteStateMachine{
    [HideInInspector]
    public TurretIdle idleState;
    [HideInInspector]
    public TurretActive movingState;
    [HideInInspector]
    public DeadState deadState;
    public bool triggered;
    public bool gunEnabled;
    public float turnspeed;
    public GameObject firePoint;
    [SerializeField, Range(0,10)]
    public float cooldown;
    public bool cooldownOn = false;

    public List<GameObject> _inrange = new List<GameObject>();
    public GameObject bulletPrefab;

    private void Awake() {
        idleState = new TurretIdle(this);
        movingState = new TurretActive(this);
        deadState = new DeadState(this);
        firePoint = ((transform.Find("Turret_Enemy").Find("Enemy_Sprite")).Find("FirePoint")).gameObject;
    }

    protected override BaseState GetInitialState() {
        return idleState;
    }

    #region triggers
    private void OnTriggerEnter2D(Collider2D collision){
        //If a player or asteroid gets in gets in range.
        if(collision.CompareTag("Player") || collision.CompareTag("Asteroid")){
            //Debug.Log(collision.tag);
            //Add it to the list
            _inrange.Add(collision.gameObject);

            //Change this to change the state
            triggered = true;
        }

    }

    private void OnTriggerExit2D(Collider2D collision){
        //if a player leaves the list
        if (_inrange!=null && _inrange.Contains(collision.gameObject)){
            //take it out
            _inrange.Remove(collision.gameObject);

            if (_inrange.Count == 0){
                triggered = false;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision){
        if (collision.CompareTag("Player_Bullet") && Vector3.Distance(transform.position, collision.transform.position) < 0.5f && !iframe){
            //Debug.Log("Player Hit");
            Destroy(collision.gameObject);
            health.currentHealth--;
            iframe = true;
        }
    }
    #endregion

    public void Shoot(GameObject closest) {
        if (firePoint != null && closest!=null && !cooldownOn) {
            Rigidbody2D rb = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(new Vector2(0, 0))).GetComponent<Rigidbody2D>();
            sounds.playSound("shoot");
            rb.velocity = (closest.transform.position - rb.transform.position).normalized * 1;

            cooldownOn = true;
            Invoke(nameof(ResetCooldown),cooldown);
        }
    }

    private void ResetCooldown() {
        cooldownOn = false;
    }


    public bool CheckHealth(){
        return health.dead;
    }

    public override BaseState FindState(string name) {
        if(name == "Idle") {
            return idleState;
        } else if(name == "Active")  {
            return movingState;
        }

        return null;
    }
}
