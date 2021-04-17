using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 0.5f;
    private float nextFire = 0.0f;
    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire1") && Time.time > nextFire){
            nextFire = Time.time + fireRate;
            Shoot();
        }
    }

    void Shoot(){
        Instantiate(bulletPrefab,transform.position,Quaternion.identity);
    }
}
