using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FiniteStateMachine : MonoBehaviour{
    BaseState currentState;
    public GameObject currentSprite;
    [SerializeField]
    public EnemyHealth health;
    public EnemySound sounds;
    public int pointValue;
    public KeepInRange keeps;

    public bool iframe = false;
    public bool oneItem = false;

    public float idleTimer = 1000f;

    [SerializeField]
    public int healDropChance = 1;
    [SerializeField]
    public int ammoDropChance = 1;

    void Start() {
        currentState = GetInitialState();
        health = GetComponent<EnemyHealth>();
        if (currentState != null)
            currentState.Enter();
    }

    private void OnEnable() {
        oneItem = true;
    }

    void Update(){
        if (currentState != null) {
            currentState.UpdateLogic();
        }

        if (iframe){
            StartCoroutine(GameManager.spriteEffects.InvisFlash(currentSprite));
            Invoke(nameof(ResetIframes), 1f);
        }
    }

    private void ResetIframes(){
        iframe = false;
    }

    void LateUpdate(){
        if (currentState != null) {
            currentState.UpdatePhysics();
            idleTimer -= Time.deltaTime;
            //Remove objects that do nothing for over thirty seconds
            if(idleTimer <= 0) {
                //Destroy(this.gameObject);
            }
       }
    }

    public void ChangeState(BaseState newState) {
        currentState.Exit();
        idleTimer = 15f;

        currentState = newState;
        currentState.Enter();
    }

    protected virtual BaseState GetInitialState() {
        return null;
    }

    protected virtual BaseState GetCurrentState(){
        return currentState;
    }

    public virtual void LoseHealth(){
        health.currentHealth--;
        sounds.playSound("hurt");
        idleTimer = 15f;
        iframe = true;
    }


    public void destory() {
        StartCoroutine(GameManager.spriteEffects.FadeTo(this.currentSprite,0, 2f));
        //Drops
        sounds.playSound("death");
        GameManager.UpdateScore(pointValue);
        DropItems();
        pointValue = 0;

        //origin.GetComponent<ObjectPool_Simple>().returnCritter(this.gameObject);
        Invoke(nameof(Disable), 3f);
        
        
        //gameObject.GetComponentInParent<Spawner>().returnObject(this.gameObject);
        //Destroy(transform.gameObject);

    }

    private void DropItems() {
        //If chance = 1 then 50% chance of a drop
        int x = UnityEngine.Random.Range(0, 2);
        if (oneItem){
            oneItem = false;
            if (x == 1){
                int chance = UnityEngine.Random.Range(0, healDropChance + 1);
                if (chance == 0){
                    Instantiate(PrefabManager.Instance.healthPickup, transform.position, transform.rotation);
                }
            }else{
                int chance = UnityEngine.Random.Range(0, ammoDropChance + 1);

                if (chance == 0){
                    Instantiate(PrefabManager.Instance.ammoPickup, transform.position, transform.rotation);
                }
            }
        }

    }

    private void Disable() {
        Transform parent = transform.parent;
        //The asteroid enemy has a parent that also needs to be turned off.        

        if (parent != null)  {
            this.transform.parent.gameObject.SetActive(false);
        } else{
            this.transform.gameObject.SetActive(false);
        }
    }

    public virtual BaseState FindState(String name){
        return currentState;
    }


#if UNITY_EDITOR
    private void OnGUI(){
        string content = currentState != null ? currentState.name : "(no current state)";
        GUILayout.Label($"<color='white'><size=10>{content}</size></color>");
    }
#endif
}
