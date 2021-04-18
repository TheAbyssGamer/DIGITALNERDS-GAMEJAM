using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 2f;
    public float rightLimit = 2.5f;
    public float leftLimit = 1.0f;
    private int dir = 1;
    
    void Start(){
        leftLimit = transform.position.x;//setting leftLimit to initial pos
    }
    void Update()
    {
        //Move the platform to right for "rightLimit" distance" then come back to original position
        if (transform.position.x >= rightLimit) {
            dir = -1;
            
        }
        else if (transform.position.x <= leftLimit) {
            dir = 1;
        }
        transform.Translate(Vector3.right*dir*speed*Time.deltaTime);
    }

}
