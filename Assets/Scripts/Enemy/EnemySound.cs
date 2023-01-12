using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour{
    // Start is called before the first frame update
    public bool deathOnlyOnce = false;
    public AudioSource source;

    public AudioClip hurtSound;
    public AudioClip deathSound;
    public AudioClip shootSound;

    private void OnEnable(){
        deathOnlyOnce = false;
    }

    public void playSound(string key){
        switch (key) {
            case "hurt":
                source.PlayOneShot(hurtSound);
                break;
            case "shoot":
                source.PlayOneShot(shootSound);
                break;
            case "death":
                if (!deathOnlyOnce) { 
                    source.PlayOneShot(deathSound);
                    deathOnlyOnce = true;
                }
                
                break;
        }
    }

}
