using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomaskAI : MonoBehaviour
{
    [SerializeField]
    private bool isAIenabled = true; //very important
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
        Physics2D.IgnoreLayerCollision(9,9);
    }
    void Update(){
        //Debug.Log(gameObject.name + isAIenabled);
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
            RaycastHit2D hitBehind;
            if(spriteRenderer.flipX == false){
                hit = Physics2D.Raycast(transform.position,Vector2.right,4f,~IgnoreMe);
                //Debug.DrawRay(transform.position,Vector2.right*4f ,Color.green);
                hitBehind = Physics2D.Raycast(transform.position,Vector2.left,1f,~IgnoreMe);
                //Debug.DrawRay(transform.position,Vector2.left*1f,Color.red);
            }else{
                hit = Physics2D.Raycast(transform.position,Vector2.left,4f,~IgnoreMe);
                //Debug.DrawRay(transform.position,Vector2.left*4f,Color.green);
                hitBehind = Physics2D.Raycast(transform.position,Vector2.right,1f,~IgnoreMe);
                //Debug.DrawRay(transform.position,Vector2.right*1f,Color.red);
            }
            if(isIdle){
                Idle();
            }

            if(hit){
                if(hit.collider.gameObject.tag == "Player"){
                    //Debug.Log("ATACK!!!!!");
                    Attack(hit);
                }
            }else{
                isIdle = true;
            }

            if(hitBehind){
                if(hitBehind.collider.gameObject.tag == "Player"){
                    //Debug.Log("ATACK!!!!!");
                    //Attack(hitBehind);
                    spriteRenderer.flipX = !spriteRenderer.flipX;
                    //transform.Rotate(0f,180f,0f);
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
        //Debug.Log(hit.normal.x);
        animator.SetBool("isIdle",false);
        isIdle = false;
        transform.position += new Vector3((speed *3f) * Time.deltaTime*(-hit.normal.x),0f,0f);

        float distance = Vector2.Distance(player.transform.position,transform.position);
        if(distance<0.6f && Time.time > nextAttack){
            nextAttack = Time.time + attackRate;
            HitMe();
            StartCoroutine(wait());//wait a lil before start runing full speed after an attack (this way will prevent anoying following and make him look smarter)
            
        }
    }

    IEnumerator wait(){
        speed = 0.05f;
        yield return new WaitForSeconds(1.5f);
        speed = 0.35f;
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
            Destroy(ps,2f);

        }else{
            ParticleSystem ps = Instantiate(coughSystem,transform.position,Quaternion.Euler(-165f,270f,-90f));
            ps.Play();
            Destroy(ps,2f);
        }

        playerScript.TakeDamage(damageAmount);
    }



    //it is necesary because i need to call it from other script
    //if i were to change only the public static bool isAIenabled it will change all enemys in the level in other words if I "kill" an enemy
    //Ai will be deactivated for every enemy in the level 
   public void disableAI(){
       isAIenabled = false;
   }

}
