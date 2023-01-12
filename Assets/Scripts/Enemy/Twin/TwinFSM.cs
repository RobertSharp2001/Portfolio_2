using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwinFSM : FiniteStateMachine{
    [HideInInspector]
    public TwinIdle idleState;
    [HideInInspector]
    public TwinActive movingState;
    [HideInInspector]
    public DeadState deadState;
    public bool triggered;
    [Header("Gun")]
    public bool gunEnabled;
    [Range(0, 10)]
    public float speedMod;
    public float turnspeed;
    public List<GameObject> firePoints;
    [Header("Cooldown")]
    [SerializeField, Range(0, 10)]
    public float cooldown;
    public bool cooldownOn = false;

    [Header("Evasion")]
    public int numberOfRays = 16;
    public float targetVelocity = 10.0f;
    public float angle = 90f;
    public float rayRange = 2f;



    public GameObject _target;
    public GameObject bulletPrefab;

    private void Awake(){
        idleState = new TwinIdle(this);
        movingState = new TwinActive(this);
        deadState = new DeadState(this);
    }

    protected override BaseState GetInitialState() {
        return idleState;
    }

    #region triggers
    private void OnTriggerEnter2D(Collider2D collision){
        //If a player or asteroid gets in gets in range.
        if (collision.CompareTag("Player")){
            //Debug.Log(collision.tag);
            //Add it to the list
            _target = collision.gameObject;

            //Change this to change the state
            triggered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)  {
        //if a player leaves the list
        if (_target != null && collision.gameObject == _target) {
            //take it out
            _target = null;
            triggered = false;
        }
    }
    /*

    private void OnTriggerStay2D(Collider2D collision){
        //Debug.Log("Collsion detected");

        //Debug.Log(Vector3.Distance(transform.position, collision.transform.position));

        if (collision.CompareTag("Player_Bullet") && Vector3.Distance(transform.position, collision.transform.position) < 0.5f && !iframe){
            Destroy(collision.gameObject);
            health.currentHealth--;
            iframe = true;
        }
    }
    */

    #endregion

    #region moveDirection

    public void AvoidObstacles(float evadeWeight){
        Vector3 deltaPosition = Vector2.zero;
        //RaycastHit2D hit = new RaycastHit2D();
        Vector3 RandomDir = GameManager.RandomVector2(-360, 360);

        for (int i = 0; i < numberOfRays; i++){
            Quaternion rotation = transform.rotation;

            Quaternion rotationMod = Quaternion.AngleAxis((i / (float)numberOfRays - 1) * angle * 2 - angle * -1, transform.forward);

            Vector3 direction = rotation * rotationMod * Vector3.up;

            //Gizmos.DrawRay(this.transform.position, direction);

            Ray ray = new Ray(transform.position, direction);


            if (Physics2D.Raycast(transform.position, direction, rayRange))   {
                deltaPosition -= (1.0f / numberOfRays) * targetVelocity * direction;
            }else{
                deltaPosition += (1.0f / numberOfRays) * targetVelocity * direction;
            }

            //Debug.Log(hit.collider);

        }

        this.transform.position += deltaPosition * evadeWeight * Time.deltaTime;

    }

    public void TurnToMoveDirection() {
        Vector3 moveDirection = gameObject.transform.position - this.transform.forward;
        if (moveDirection != Vector3.zero){
            Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg);
            transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, turnspeed * Time.deltaTime);
        }
    }

    public void MoveToPlayer() {
        if (_target == null){
            return;
        }

        Quaternion rotation = Quaternion.Euler(0, 0, Mathf.Atan2(_target.transform.position.y - transform.position.y, _target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90);

        float angle = Quaternion.Angle(transform.rotation, rotation);
        if (angle < 10 && _target != null) {
            gunEnabled = true;
        } else {
            gunEnabled = false;
        }

        //look at the player
        transform.localRotation = Quaternion.Slerp(transform.rotation, rotation, turnspeed * Time.deltaTime);
        //move toward the player
        transform.localPosition = Vector3.Lerp(transform.position, _target.transform.position, targetVelocity * Time.deltaTime);
    }

    #endregion

    public void Shoot() {
        if (!cooldownOn) {
            for (int i = 0; i < firePoints.Count; i++) {
                if (firePoints[i] != null){
                    Rigidbody2D rb = Instantiate(bulletPrefab, firePoints[i].transform.position, Quaternion.Euler(new Vector2(0, 0))).GetComponent<Rigidbody2D>();
                    sounds.playSound("shoot");
                    rb.velocity = (_target.transform.position - rb.transform.position).normalized * 1;
                }
            }
            cooldownOn = true;
            Invoke(nameof(ResetCooldown), cooldown);
        }
    }

    private void ResetCooldown(){
        cooldownOn = false;
    }

    public bool CheckHealth(){
        return health.dead;
    }

    public override BaseState FindState(string name) {
        if (name == "Idle") {
            return idleState;
        } else if (name == "Active"){
            return movingState;
        }

        return null;
    }

    private void OnDrawGizmos()
    {
        /* //Old stuff
        Gizmos.DrawWireSphere(firePoint.transform.position, 0.1f);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(currentSprite.transform.position, firePoint.transform.position);
        */



    }
}
