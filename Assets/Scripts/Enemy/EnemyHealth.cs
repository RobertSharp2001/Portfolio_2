using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour{
    public int currentHealth;
    public int maxHealth;
    public SpriteRenderer currentSprite;
    public Sprite deadSprite;
    public Sprite defaultSprite;
    internal bool dead = false;
    // Start is called before the first frame update
    void Start() {
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update(){
        if(currentHealth <= 0){
            dead = true;
            currentSprite.sprite = deadSprite;
        }
    }

    public void OnEnable(){
        dead = false;
        currentHealth = maxHealth;
        Color col = new Color(1,1,1,1);
        currentSprite.material.color = col;
        currentSprite.sprite = defaultSprite;
    }
}
