using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeepInRange : MonoBehaviour{
    public GameObject gameArea;
    public ObjectPool_Simple objectPool;
    public float range;
    public bool dummy = false;

    public CircleCollider2D[] colliders;

    public float rSpeeed;
    void Update(){
        Move();
    }

    public void OnEnable()  {
        //Hopefully fix the trigger bug
        gameArea = ReferenceManager.Instance.followPlayer;
        colliders = GetComponents<CircleCollider2D>();

        foreach (var collider in colliders){
            collider.enabled = false;
            collider.enabled = true;
        }
    }

    public void OnDisable(){
        if (objectPool != null) {
            objectPool.returnCritter(this.gameObject);
        }
    }

    void Move(){
        /** Move this ship forward per frame, if it gets too far from the game area, destroy it **/

        float distance = Vector3.Distance(transform.position, gameArea.transform.position);
        if (distance > range && !dummy) {
            transform.position = Vector3.Lerp(transform.position, gameArea.transform.position, rSpeeed * Time.deltaTime);
        }
    }
}
