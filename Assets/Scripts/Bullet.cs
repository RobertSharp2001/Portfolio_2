using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour{
    // Start is called before the first frame update
    public Rigidbody2D rb;
    public CircleCollider2D circle;
    [Range(0,10)]
    public float constantSpeed;

    void Start(){
        //Destory bullets rather than keep them in memory
        rb = GetComponent<Rigidbody2D>();
        circle = GetComponent<CircleCollider2D>();
        Invoke(nameof(DestroySelf),2f);
    }

    private void Update(){
        rb.velocity = constantSpeed * (rb.velocity.normalized);


    }

    private void OnCollisionEnter2D(Collision2D collision){

        
        if (collision != null) {
                #region player bullets
                if (collision.gameObject.tag == "Enemy" &&  this.gameObject.CompareTag("Player_Bullet")){
                    //Debug.Log("Enemy hit by player");
                    FiniteStateMachine fsm = collision.gameObject.GetComponent<FiniteStateMachine>();
                    if (fsm != null && !fsm.iframe) {
                        fsm.LoseHealth();
                        Invoke(nameof(DestroySelf), 0.2f);
                    }
                }
                #endregion

                if (this.gameObject.CompareTag("Enemy_Bullet") && collision.gameObject.CompareTag("Player") && !GameManager.IFrame) {
                    //Debug.Log("Player hit by enemy");

                    GameManager.lowerHealth();
                    Invoke(nameof(DestroySelf), 0.2f);
                }
   
        }
    }

    public void DestroySelf() {
        Destroy(this.gameObject);
    }

}
