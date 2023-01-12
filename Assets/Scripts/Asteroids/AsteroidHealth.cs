using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHealth : MonoBehaviour{
    // Start is called before the first frame update
    Asteroid_Ship ship;
    public bool canDamage = true;

    public  void Start() {
        ship = GetComponentInParent<Asteroid_Ship>();
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.CompareTag("Player_Bullet") || collision.CompareTag("Enemy_Bullet")){
            canDamage = false;
            StartCoroutine(GameManager.spriteEffects.FadeTo(gameObject, 0, 1f));
            //This destorys the game objects so like theres no need to reset the damage
            Invoke(nameof(kill),1.5f);
            Destroy(collision.gameObject);
        } else if (collision.CompareTag("Player") && !GameManager.IFrame && canDamage) {
            canDamage = false;
            //Allow the player to hit the same asteroid again after 1 second;
            Invoke(nameof(resetDamage),1f);
            GameManager.lowerHealth();
        }
    }

    private void resetDamage(){
        canDamage = true;
    }

    public void kill() {
        ship.RemoveShip();
    }

    IEnumerator FadeTo(float aValue, float aTime) {
        SpriteRenderer sr = transform.GetComponent<SpriteRenderer>();
        float alpha = sr.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime) {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, aValue, t));
            sr.material.color = newColor;
            yield return null;
        }
    }
}
