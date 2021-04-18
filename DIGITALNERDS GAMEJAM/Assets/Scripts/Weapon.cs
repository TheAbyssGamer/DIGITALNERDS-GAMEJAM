using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    public AudioSource audioSource;
    public AudioClip shootAudio;
    public GameObject playerObject;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire && Inventory.Ammo >0){
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot(){
        audioSource.clip = shootAudio;
        audioSource.Play();
        var bl =Instantiate(bulletPrefab,transform.position,Quaternion.identity);
        bl.GetComponent<Bullet>().player = playerObject;
        Inventory.Ammo -= 1;
    }
}
