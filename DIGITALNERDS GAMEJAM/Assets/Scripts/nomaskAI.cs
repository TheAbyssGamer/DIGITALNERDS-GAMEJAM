﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomaskAI : MonoBehaviour
{

    public static bool isAIenabled = true; //very important
    public float speed = 5f;
    public float jumpHeight = 2f;
    public int damageAmount = 25;
    public GameObject player;
    public LayerMask IgnoreMe;
    public SpriteRenderer spriteRenderer;
    bool isIdle = false;

    private float latestDirectionChangeTime;
    private float directionChangeTime = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    public Animator animator;
    public ParticleSystem coughSystem;

    public float attackRate =0.5f;
    private float nextAttack = 0f;

    void Start(){
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
    void Update(){
        //the complex Ai that will atack 
        if(isAIenabled)
        {
            //shooting a ray from the enemy eye (baiscly giving him eyes)
            //the ray is shot in the direction the enemy is facing
            //he is wondering in the scene (Idle function) untill the player comes in contact with his "eyesight"
            //if he sees the player he will chase him(Attack function) and if his close enough he will hit the player(HitMe function)
            //rest of the function calcultates rng numbers and radnom direction for the idle state
            directionChangeTime = Random.Range(2f,6f);
            RaycastHit2D hit;
            if(spriteRenderer.flipX == false){
                hit = Physics2D.Raycast(transform.position,Vector2.right,4f,~IgnoreMe);
                Debug.DrawRay(transform.position,Vector2.right*4f,Color.green);
            }else{
                hit = Physics2D.Raycast(transform.position,Vector2.left,4f,~IgnoreMe);
                Debug.DrawRay(transform.position,Vector2.left*4f,Color.green);
            }
            if(isIdle){
                Idle();
            }

            if(hit){
                if(hit.collider.gameObject.tag == "Player"){
                    Debug.Log("ATACK!!!!!");
                    Attack(hit);
                }
            }else{
                isIdle = true;
            }
            //less complex ai that will wonder in the scene
        }else{
            Idle();
        }
    }

    void Attack(RaycastHit2D hit){
        animator.SetBool("isIdle",false);
        isIdle = false;
        transform.position += new Vector3((speed *3f) * Time.deltaTime*(-hit.normal.x),0f,0f);

        float distance = Vector2.Distance(player.transform.position,transform.position);
        if(distance<0.6f && Time.time > nextAttack){
            nextAttack = Time.time + attackRate;
            HitMe();
        }
    }
    void Idle(){

        if (Time.time - latestDirectionChangeTime > directionChangeTime){
            latestDirectionChangeTime = Time.time;
            calcuateNewMovementVector();}

        transform.position = new Vector2(transform.position.x + (movementPerSecond.x * Time.deltaTime), 
        transform.position.y + (movementPerSecond.y * Time.deltaTime));
    }
    void calcuateNewMovementVector(){
        //create a random direction vector and a rng which calculate how much will it move or how much will it stay in one place
        int rng = Random.Range(0,10);
        //Debug.Log("rng" + rng);
        if(rng<5){
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0f).normalized;
            if(movementDirection.x<0){
                spriteRenderer.flipX = true;
            }else if(movementDirection.x>0f){
                spriteRenderer.flipX = false;
            }
            animator.SetBool("isIdle",false);
        }else if(rng>5){
            movementDirection = new Vector2(0f, 0f).normalized;
            animator.SetBool("isIdle",true);
        }
        movementPerSecond = movementDirection * speed;
    }

    void HitMe(){
        Player playerScript = player.GetComponent<Player>();

        if(spriteRenderer.flipX == true){
            ParticleSystem ps = Instantiate(coughSystem,transform.position,Quaternion.Euler(-165f,90f,-90f));
            ps.Play();
            Destroy(ps,4f);

        }else{
            ParticleSystem ps = Instantiate(coughSystem,transform.position,Quaternion.Euler(-165f,270f,-90f));
            ps.Play();
            Destroy(ps,4f);
        }

        playerScript.TakeDamage(damageAmount);
    }

   
}
