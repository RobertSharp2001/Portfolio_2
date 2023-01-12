using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Pickup_Type{
    health = 0,
    ammo = 1
}

public class ItemPickup : MonoBehaviour{
    // Start is called before the first frame update
    [SerializeField]
    Pickup_Type type;

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Player")) {
            GameManager.CheckItemPickup(this.type);
            Destroy(this.gameObject);
        }
    }
}
