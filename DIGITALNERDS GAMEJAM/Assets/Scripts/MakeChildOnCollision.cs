using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeChildOnCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            collider.transform.SetParent(transform);
        }
    }

    void OnTriggerExit2D(Collider2D collider){
        if(collider.gameObject.tag == "Player"){
            collider.transform.SetParent(null);
        }
    }
}
