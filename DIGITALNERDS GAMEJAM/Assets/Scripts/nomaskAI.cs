using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomaskAI : MonoBehaviour
{
    public float speed = 5f;
    public float jumpHeight = 2f;
    public GameObject player;
    public LayerMask IgnoreMe;
    public SpriteRenderer spriteRenderer;
    bool isIdle = false;

    private float latestDirectionChangeTime;
    private float directionChangeTime = 2f;
    private Vector2 movementDirection;
    private Vector2 movementPerSecond;

    void Start(){
        latestDirectionChangeTime = 0f;
        calcuateNewMovementVector();
    }
    void Update(){
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
    }

    void Attack(RaycastHit2D hit){
        isIdle = false;
        transform.position += new Vector3((speed *3f) * Time.deltaTime*(-hit.normal.x),0f,0f);
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
        Debug.Log("rng" + rng);
        if(rng<5){
            movementDirection = new Vector2(Random.Range(-1.0f, 1.0f), 0f).normalized;
            if(movementDirection.x<0){
                spriteRenderer.flipX = true;
            }else if(movementDirection.x>0f){
                spriteRenderer.flipX = false;
            }
        }else if(rng>5){
            movementDirection = new Vector2(0f, 0f).normalized;
        }
        movementPerSecond = movementDirection * speed;
    }
   
}
