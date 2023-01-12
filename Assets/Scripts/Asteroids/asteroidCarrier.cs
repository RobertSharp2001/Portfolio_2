using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidCarrier : asteroidBase{
    // Start is called before the first frame update
    public GameObject child;

    void Start(){
        child = transform.Find("Turret_Enemy").gameObject;
    }

    // Update is called once per frame
    void Update(){
        if (child != null){
            /*
            Vector2 targetVelocity = GameManager.globalDir;
            Vector2 currentVelocity = rb.velocity;

            rb.AddForce((targetVelocity - currentVelocity) * strength * Time.deltaTime);
            //force the child to stay in the center of the asteroid
            */
            child.transform.localPosition = new Vector3(0, 0, 0);
        } else {
            StartCoroutine(GameManager.spriteEffects.FadeTo(gameObject, 0, 2f));
            Destroy(gameObject, 3f);
        }
    }
}
