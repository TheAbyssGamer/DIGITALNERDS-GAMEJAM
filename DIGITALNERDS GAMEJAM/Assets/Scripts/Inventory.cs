using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Inventory : MonoBehaviour
{
    public static GameObject holding;
    public RuntimeAnimatorController FireGunAnim;
    public RuntimeAnimatorController GUN_0Anim;
    public Animator animator;
    private bool haveGUN_0 = false;
    private bool haveFIREGUN = false;
    public GameObject BulletPrefab;

    public static int Ammo;
    public TextMeshProUGUI ammoCounter;
    public AudioSource audioSourceI;
    public AudioClip shootAudioI;

    void Start(){
        Ammo = 0;
    }
    void Update(){
        ammoCounter.text = Ammo.ToString();
        //Debug.Log("Acu tin "+holding);
        if(holding){
            if(holding.name == "GUN_0" && haveGUN_0 == false){
                animator.runtimeAnimatorController = GUN_0Anim as RuntimeAnimatorController;

                if(gameObject.GetComponent<Weapon>()==null){
                    gameObject.AddComponent<Weapon>();
                    Weapon weapon = gameObject.GetComponent<Weapon>();
                    weapon.bulletPrefab = BulletPrefab;
                    weapon.audioSource = audioSourceI;
                    weapon.shootAudio = shootAudioI;
                    weapon.playerObject = gameObject;
                }
                haveFIREGUN = false;
                haveGUN_0 = true;
            }else if(holding.name == "FIREGUN" && haveFIREGUN == false){
                animator.runtimeAnimatorController = FireGunAnim as RuntimeAnimatorController;
                if(gameObject.GetComponent<Weapon>()==null){
                    gameObject.AddComponent<Weapon>();
                    Weapon weapon = gameObject.GetComponent<Weapon>();
                    weapon.bulletPrefab = BulletPrefab;
                    weapon.audioSource = audioSourceI;
                    weapon.shootAudio = shootAudioI;
                    weapon.playerObject = gameObject;
                }
                haveGUN_0 = false;
                haveFIREGUN = true;
            }
        }
    }


}
