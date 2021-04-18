using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    public int dmgAmount = 30;
    public Rigidbody2D rb;

    public GameObject player{get;set;}

    // Start is called before the first frame update
    void Start()
    {
        BulletMove();
    }

    void BulletMove(){
        SpriteRenderer playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        if(playerSpriteRenderer.flipX == false){
            rb.velocity = transform.right * speed;
        }else{
            rb.velocity = -transform.right * speed;
        }
    }

    void OnTriggerEnter2D(Collider2D hit)
    {
        //Debug.Log(hit.gameObject.layer);
        nomask nomask = hit.GetComponent<nomask>();
        if(nomask != null){
            nomask.TakeDamage(dmgAmount);
        }
        if(hit.gameObject.tag!="Player"){
            if(hit.gameObject.layer != 11){
                Destroy(gameObject);

            }
        }
        
    }
}
