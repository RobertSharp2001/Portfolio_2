using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStock : MonoBehaviour{
    // Start is called before the first frame update
    public int maxHealth;
    public int maxAmmo;
    internal int currentealth;
    internal int currentAmmo;
    [Header("Healtherbar Images")]
    public Image healthbarHolder;
    public Sprite[] barSprites;
    [Header("Ammo Images")]
    public Image ammobarHolder;
    public Sprite[] ammobarSprites;
    public SpriteRenderer playerSprite;
    public Sprite deadSprite;

    void Start() {
        currentAmmo = maxAmmo;
        currentealth = maxHealth;
        playerSprite = GameObject.Find("Player_Sprite").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update(){
        calculatePercentages();
    }

    void calculatePercentages(){

        switch (currentealth){
            case 5:
                healthbarHolder.sprite = barSprites[0];
                break;
            case 4:
                healthbarHolder.sprite = barSprites[1];
                break;
            case 3:
                healthbarHolder.sprite = barSprites[2];
                break;
            case 2:
                healthbarHolder.sprite = barSprites[3];
                break;
            case 1:
                healthbarHolder.sprite = barSprites[4];
                break;
            default:
                healthbarHolder.sprite = barSprites[5];
                //Debug.Log("dead");
                break;
        }

        switch (currentAmmo){
            case 9:
                ammobarHolder.sprite = ammobarSprites[0];
                break;
            case 8:
                ammobarHolder.sprite = ammobarSprites[1];
                break;
            case 7:
                ammobarHolder.sprite = ammobarSprites[2];
                break;
            case 6:
                ammobarHolder.sprite = ammobarSprites[3];
                break;
            case 5:
                ammobarHolder.sprite = ammobarSprites[4];
                break;
            case 4:
                ammobarHolder.sprite = ammobarSprites[5];
                break;
            case 3:
                ammobarHolder.sprite = ammobarSprites[6];
                break;
            case 2:
                ammobarHolder.sprite = ammobarSprites[7];
                break;
            case 1:
                ammobarHolder.sprite = ammobarSprites[8];
                break;
            default:
                ammobarHolder.sprite = ammobarSprites[9];
                break;
        }
    }
}
