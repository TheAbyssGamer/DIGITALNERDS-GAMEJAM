using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunInteract : MonoBehaviour
{
    public bool isInteracted = false;
    void Update(){
        if(Input.GetKeyDown(KeyCode.F)){
            
            isInteracted = true;
        }
        if(Input.GetKeyUp(KeyCode.F)){
            isInteracted = false;
        }
    }
    void OnTriggerStay2D(Collider2D other)
    {
        Debug.Log(isInteracted);
        if(other.gameObject.tag == "Player" && isInteracted){
            Debug.Log("A luat "+gameObject.name);
            
            if(Inventory.holding){
                Inventory.holding.transform.position = other.transform.position + new Vector3(0f,-0.35f,0f);
            }
            Inventory.holding = gameObject;
            transform.position = new Vector3(0f,300f,0f);
        }
        
    }
}
