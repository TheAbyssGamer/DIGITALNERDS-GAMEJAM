using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public Animator animator;
    public float speed = 10f;
    public float jumpHeight = 5f;

    public Transform groundCheck;
    public float groundDistance;
    public LayerMask groundMask;

    bool isGrounded = false;
    public Rigidbody2D rb;

    public SpriteRenderer spriteRenderer;

    // Update is called once per frame
    void Update()
    {   
        
        if(Input.GetAxis("Horizontal")<0){
            spriteRenderer.flipX = true;
        }else if(Input.GetAxis("Horizontal")>0){
            spriteRenderer.flipX = false;
        }
        //JUMPING
        isGrounded = Physics2D.OverlapCircle(groundCheck.position,groundDistance,groundMask);//check for ground first
        if(Input.GetKeyDown(KeyCode.Space)&& isGrounded){
            Debug.Log("Jump");
            animator.SetBool("isJumping",true);
            rb.AddForce(new Vector2(0,jumpHeight),ForceMode2D.Impulse);
        }

        if(!isGrounded){
            animator.SetBool("isJumping",false);
        }

        animator.SetBool("isGrounded",isGrounded);
        //

        //MOVEMENT
        Vector3 hDir = new Vector3(Input.GetAxis("Horizontal")*speed*Time.deltaTime,0,0);
        transform.Translate(hDir);
        animator.SetFloat("Speed",Mathf.Abs(hDir.x));
        //
        
    }
}
