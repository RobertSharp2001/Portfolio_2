using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidSpawner : MonoBehaviour{
    // Start is called before the first frame update

    public GameObject gameArea;
    public GameObject asteroidPrefab;

    public int asteroid_count = 0;
    public int asteroid_limit = 50;
    public int ship_per_frame = 1;

    public float spawnCircleRadius = 150f;
    public float deathCircleRadius = 160f;
    [Range(1, 5)]
    public float fastSpeed = 7f;
    [Range(0,1)]
    public float slowSpeed = 1f;

    void Start(){
        InitialPopulation();
    }

    void Update(){
        MaintainPopulation();
    }

    void InitialPopulation() {
        /** To avoid having to wait for the ships to enter the screen at start up, create an
        initial set of ships for instant action. **/

        for (int i = 0; i < asteroid_limit; i++) {
            Vector3 position = GetRandomPosition(true);
            Asteroid_Ship ship_script = AddShip(position);
            ship_script.transform.Rotate(Vector3.forward * Random.Range(0.0f, 360.0f));
        }
    }

    void MaintainPopulation() {
        /** Create more ships as old ones are destroyed, while respecting the object limit. **/
        //Update the count in case onje was destroyed
        asteroid_count = GameObject.FindGameObjectsWithTag("Asteroid").Length;

        if (asteroid_count < asteroid_limit) {
            for (int i = 0; i < ship_per_frame; i++) {
                Vector3 position = GetRandomPosition(false);
                Asteroid_Ship ship_script = AddShip(position);
                ship_script.transform.Rotate(Vector3.forward * Random.Range(-45.0f, 45.0f));
            }
        }
    }


    Vector3 GetRandomPosition(bool within_camera){
        /** Get a random spawn position, using a 2D circle around the game area. **/

        Vector3 position = Random.insideUnitCircle;

        if (within_camera == false) {
            position = position.normalized;
        }

        position *= spawnCircleRadius;
        position += gameArea.transform.position;

        return position;
    }

    Asteroid_Ship AddShip(Vector3 position) {
        /**Add a new ship to the game and set the basic attributes. **/

        asteroid_count += 1;
        GameObject new_ship = Instantiate(
            asteroidPrefab,
            position,
            Quaternion.FromToRotation(Vector3.up, (gameArea.transform.position - position)),
            gameObject.transform
        );

        Asteroid_Ship ship_script = new_ship.GetComponent<Asteroid_Ship>();
        ship_script.asteroid_spawner = this;
        ship_script.game_area = gameArea;
        ship_script.speed = Random.Range(slowSpeed, fastSpeed);

        return ship_script;
    }

}
