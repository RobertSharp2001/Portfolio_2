using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour {
    public GameObject player;
    public GameObject[] Enemies;
    public ParticleSystem particles;
    public int screenSize;
    public static Vector2 globalDir;
    [SerializeField]
    private static PlayerStock pStock;
    public static GameObject Deathmenu;
    public TMP_Text Time_tmp;
    public TMP_Text Score_tmp;

    [SerializeField, Header("Tools")]
    public static SpriteEffects spriteEffects;
    public static WaveManager waveManager;

    private static int score;
    private static int timeScore = 0;
    private static int highscore = 0;

    public GameObject healthPrefab;
    public GameObject ammoPrefab;

    public GameObject temp;

    public Compass compass;

    [Header("Wave mechanics")] 
    public TMP_Text WaveNum_tmp;
    public TMP_Text maxEnem_tmp;
    public TMP_Text finScore_tmp;
    public TMP_Text hiScore_tmp;
    public static int wave = 0;
    public int maxEnem = 3;
    public static bool enemCheck = true;

    public static Sound_Player player_sounds;

    DateTime startTime;
    public TimeSpan timeElapsed { get; private set; }
    public static bool IFrame { get; private set; }

    public static bool itemSoundCheck = false;

    public void Start() {
        wave = 1;
        maxEnem = 3;
        particles.Simulate(1);
        startTime = DateTime.Now;

        pStock = FindObjectOfType<PlayerStock>();
        player_sounds = FindObjectOfType<Sound_Player>();
        Deathmenu = GameObject.FindGameObjectWithTag("Menus");

        particles.Play();
        //Player starts with 3 second of invincibility to make it fairer
        IFrame = true;
        spriteEffects = GetComponentInChildren<SpriteEffects>();
        waveManager = GetComponentInChildren<WaveManager>();
    }


    public void Update() {
        //Debug.Log(IFrame);
        //Allow the user to quit the game in builds
        if (Input.GetKeyDown(KeyCode.Escape)){
            Application.Quit();
        }

        //Make it so this changes every so often
        WaveNum_tmp.text = wave.ToString();
        maxEnem_tmp.text = "Enemies:" + maxEnem.ToString();

        if (!IsPlayerDead()){
            Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            compass.pointTo = GetClosestEnemy();

            UpdateHighscore();

            if (Enemies.Length <= 0 && enemCheck) {
                //Trigger the new wave

            } else {
                Enemies = GameObject.FindGameObjectsWithTag("Enemy");
            }

            if (IFrame) {
                StartCoroutine(GameManager.spriteEffects.InvisFlash(player.transform.Find("Player_Sprite").gameObject));
                Invoke(nameof(ResetIframes),3f);
            }

            if (itemSoundCheck) {
                Invoke(nameof(GameManager.resetItemSound), 0.3f);
            }

            this.timeElapsed = DateTime.Now - startTime;
            Time_tmp.text = "Time: \t" + TimetoStr(timeElapsed);

            Score_tmp.text = "Score: \t" + (score + timeScore);
        }

        finScore_tmp.text = "" + (score + timeScore);

        globalDir = new Vector2(1, 1);
        //KeepPlayerinBounds();
    }

    void UpdateHighscore() {
        if (score + timeScore > highscore){
            highscore = score + timeScore;

            PlayerPrefs.SetInt("Highscore", highscore);
        }

        Debug.Log("Highscore" + PlayerPrefs.GetInt("Highscore"));
        hiScore_tmp.text = "" + PlayerPrefs.GetInt("Highscore");
    }

    GameObject GetClosestEnemy(){
        float distanceToClosestEnemy = Mathf.Infinity;
        GameObject closest = null;

        GameObject[] allEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (allEnemies != null) {
            foreach (GameObject enemy in allEnemies){
                float distanceToEnemt = (enemy.transform.position - player.transform.position).sqrMagnitude;
                if (distanceToEnemt < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemt;
                    closest = enemy;
                }
            }
        }

        if (closest != null) {
            Debug.DrawLine(player.transform.position, closest.transform.position);
            return closest;
        }

        return null;
    }

    private void ResetEnem() {
        enemCheck = true;
    }

    private string TimetoStr(TimeSpan t) {

        timeScore = (int)(t.TotalSeconds);
        return t.Hours + ":" + t.Minutes + ":" + t.Seconds + "." + (t.Milliseconds).ToString("000");
    }

    public static void CheckItemPickup(Pickup_Type type){
        if (!itemSoundCheck) {
            itemSoundCheck = true;
            player_sounds.Playsound(3);    
        }

        if (type == Pickup_Type.health){
            resetHealth();
        } else{
            resetAmmo();
        }

        
    }

    private void resetItemSound(){
        itemSoundCheck = false;
    }

    public static void UpdateScore(int value){
        //player_sounds.Playsound(4);
        score += value;
    }

    #region health
    public static void resetHealth(){
        pStock.currentealth = pStock.maxHealth;
    }

    public static void lowerHealth(){
        player_sounds.Playsound(1);

        if (!IFrame) {
            IFrame = true;
            pStock.currentealth--;
        }
        
    }

    private void ResetIframes() {           
        IFrame = false;
    }

    public static bool IsPlayerDead() {
        if (pStock == null) {
            //if pstock nulls itself out, then the player is deifnetly dead
            return true;
        }
        bool value = pStock.currentealth <= 0;
        if (value){
            enableMenus();
            pStock.playerSprite.sprite = pStock.deadSprite;
        }

        return value;
    }

    private static void enableMenus() {
        if(Deathmenu.transform.GetChild(0).gameObject.activeSelf == false)  {
            Deathmenu.transform.GetChild(0).gameObject.SetActive(true);
            player_sounds.Playsound(0);
        }

    }

    #endregion

    #region ammo
    public static void lowerAmmo() {
        pStock.currentAmmo--;
    }

    public static void resetAmmo(){
        pStock.currentAmmo = pStock.maxAmmo;
    }
    public static int getAmmo(){
        return pStock.currentAmmo;
    }

    #endregion

    public static Vector2 RandomVector2(float angle, float angleMin){
        float random = UnityEngine.Random.value * angle + angleMin;
        return new Vector2(Mathf.Cos(random), Mathf.Sin(random));
    }

    public void KeepPlayerinBounds(){
        Vector3 pos = player.transform.position;
        /*
        Debug.Log(screenSize);
        Debug.Log(-screenSize);
        */

        if(player.transform.position.x < -screenSize){
            pos.x = -screenSize;
        }
        else if (player.transform.position.x > screenSize){
            pos.x = screenSize;
        }

        if (player.transform.position.y < -screenSize){
            pos.y = -screenSize;
        }
        else if (player.transform.position.y > screenSize){
            pos.y = screenSize;
        }


        player.transform.position = pos;

    }

}
