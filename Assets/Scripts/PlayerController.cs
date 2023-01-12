using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour{
    // Start is called before the first frame update

    [Header("Msic")]
    public Camera mainCam;

    [Header("Shooting"), Range(0,10)]
    public float bulletSpeed;
    public GameObject bulletPrefab;
    public GameObject firePoint;

    public Vector3 MousePosition;

    [Header("Moving")]
    public Rigidbody2D playerRb;
    private Vector2 movmentVector;
    [Range(0, 10)]
    public float movementSpeed;
    [Range(0, 1)]
    public float maxDistFromMouse;


    void Start(){
        if (mainCam == null){
            mainCam = Camera.main;
        }
        playerRb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update(){
        MousePosition = Input.mousePosition;
        MousePosition.z = 0.0f;
        //Rotate the player to look and shoot at the mous
        UpdateCam();
        if (!GameManager.IsPlayerDead()) {

            MouseLook();

            if (Input.GetMouseButtonDown(0)){
                HandleBullet();
            }

            GetMoveInput();
            //stop the player object from being dislocated by colliders
            GameObject.Find("Player_Sprite").transform.localPosition = new Vector3(0, 0, 0);
        }

    }

    private void GetMoveInput(){
        float x = Input.GetAxis("Vertical");
        float y = Input.GetAxis("Horizontal");
        //Debug.Log(x);
        if( x == 0.0f & y == 0.0f){
            playerRb.velocity = new Vector2(0,0);
        }

        Vector3 mouse = mainCam.ScreenToWorldPoint(MousePosition);
        mouse.z = 0.0f;
        float distance = Vector3.Distance(transform.position, mouse);
        //Debug.Log(distance);

        if(distance > maxDistFromMouse){
            transform.position = transform.position + (transform.up * x * movementSpeed * Time.deltaTime);
            transform.position = transform.position + (transform.right * y * movementSpeed * Time.deltaTime);
        }           
    }

    public void MouseLook(){
        Vector3 mouse = mainCam.ScreenToWorldPoint(MousePosition);
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(mouse.y - transform.position.y, mouse.x - transform.position.x) * Mathf.Rad2Deg - 90);
    }

    public void UpdateCam(){
        Vector3 temp = transform.position;
        temp.z = -10;
        mainCam.transform.position = temp;
    }

    public void HandleBullet(){
        if(GameManager.getAmmo() > 0) {
            MousePosition = mainCam.ScreenToWorldPoint(MousePosition);
            MousePosition = MousePosition - transform.position;
            //...instantiating the bullet
            GameManager.player_sounds.Playsound(2);

            Rigidbody2D bulletInstance = Instantiate(bulletPrefab, firePoint.transform.position, Quaternion.Euler(new Vector2(0, 0))).GetComponent<Rigidbody2D>();
            //Apply velkocitry
            bulletInstance.velocity = new Vector2(MousePosition.x * bulletSpeed, MousePosition.y * bulletSpeed);
            GameManager.lowerAmmo();
        }
        

    }
}


