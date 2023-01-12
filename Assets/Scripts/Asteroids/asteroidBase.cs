using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class asteroidBase : MonoBehaviour {
    public float strength = 1f;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {

        Vector2 targetVelocity = GameManager.globalDir;
        Vector2 currentVelocity = rb.velocity;

        rb.AddForce((targetVelocity - currentVelocity) * strength *  Time.deltaTime);
    }

}
