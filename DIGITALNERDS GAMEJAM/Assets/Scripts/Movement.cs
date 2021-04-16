using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float speed = 10f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    bool isGrounded = false;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {   
        //JUMPING
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundDistance,groundMask);//check for ground first
        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded){
            Debug.Log("Jump");
            rb.AddForce(new Vector2(0,jumpHeight),ForceMode2D.Impulse);
        }
        //

        //MOVEMENT
        Vector3 hDir = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0,0);
        transform.Translate(hDir);
        //
        
    }
}
