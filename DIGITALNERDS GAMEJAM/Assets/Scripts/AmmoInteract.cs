using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoInteract : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            Inventory.Ammo += 3;
            Destroy(gameObject);
        }
    }
}
