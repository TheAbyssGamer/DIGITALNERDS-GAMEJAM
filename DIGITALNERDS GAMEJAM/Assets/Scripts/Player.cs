using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int health = 3;
    public AudioSource audioSource;
    public AudioClip hitAudio;
    public GameObject DeadScreen;

    void Start(){
        health = 3;
        Time.timeScale = 1f;
    }
    public void TakeDamage(int damage){
        audioSource.clip = hitAudio;
        audioSource.Play();
        health -= damage;
        if(health <=0){
            Die();
        }
    }

    void Die(){
        Debug.Log("PLAYER DEAD!!!!!!");
        DeadScreen.SetActive(true);
        Time.timeScale = 0f;
    }
}
