using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class nomask : MonoBehaviour
{
    public int health = 100;
    public GameObject deathEffect;
    public Animator animator;
    public RuntimeAnimatorController withMaskAnim;
    public GameObject player;
    public nomaskAI aI;
    
    public AudioSource audioSource;
    public AudioClip hitAudio;

    public void TakeDamage(int damage){
        audioSource.clip = hitAudio;
        audioSource.Play();
        health -= damage;
        if(health <=0){
            Die();
        }
    }

    void Die(){
        //Instantiate(deathEffect,transform.position,Quaternion.identity);
        //Destroy(gameObject);
        gameObject.layer = 11;
        nomaskAI AI = gameObject.GetComponent<nomaskAI>();
        //nomaskAI.isAIenabled = false;
        AI.disableAI();
        animator.runtimeAnimatorController = withMaskAnim as RuntimeAnimatorController;
        BoxCollider2D boxcolider = gameObject.GetComponent<BoxCollider2D>();
        
        Physics2D.IgnoreCollision(boxcolider,player.GetComponent<BoxCollider2D>());
        
    }
}
