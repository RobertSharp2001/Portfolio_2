using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickSprite : MonoBehaviour{

    public Sprite[] asteroids;

    public void PickSprite() {
        int x = Random.Range(0,asteroids.Length -1);

        GetComponent<SpriteRenderer>().sprite = asteroids[x];
    }
    // Start is called before the first frame update
    void Start() {
        PickSprite();   
    }
}
