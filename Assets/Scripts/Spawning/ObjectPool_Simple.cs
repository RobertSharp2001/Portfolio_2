using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool_Simple : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject prefabs;

    [SerializeField]
    private Queue<GameObject> critterPool = new Queue<GameObject>();
    public GameObject game_area;

    [SerializeField]
    private int poolStartSize;
    public int enemyType;
    public float range;

    private void Start(){
        for (int i = 0; i < poolStartSize; i++) {
            GameObject newItem = spawn(prefabs);
            critterPool.Enqueue(newItem);
            newItem.SetActive(false);
        }
    }


    public GameObject GetCritter()  {
        if(critterPool.Count > 0 && critterPool!=null){
            GameObject critter = critterPool.Dequeue();
            critter.SetActive(true);
            Vector3 position = getTrans(false);
            critter.transform.position = position;
            return critter;
        } else {
            GameObject critter = spawn(prefabs);
            return critter;
        }
    }

    public void updateSpawnArea(GameObject go){
        game_area = go;
    }

    Vector3 getTrans(bool within_camera = false){
        /** Get a random spawn position, using a 2D circle around the game area. **/

        Vector3 position = Random.insideUnitCircle;

        if (within_camera == false){
            position = position.normalized;
        }

        position *= range;
        position += game_area.transform.position;

        return position;
    }

    public GameObject spawn(GameObject go){
        return Instantiate( PrefabManager.Instance.EnemyPrefabs[enemyType], transform.position, transform.rotation);
    }

    public  void returnCritter(GameObject critter) {
        Debug.Log("Scritter returned");
        critterPool.Enqueue(critter);
        critter.SetActive(false);
    }
}
