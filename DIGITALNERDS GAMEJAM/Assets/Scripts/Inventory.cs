using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static GameObject holding;
    public RuntimeAnimatorController FireGunAnim;
    public RuntimeAnimatorController GUN_0Anim;
    public Animator animator;
    void Update(){
        Debug.Log("Acu tin "+holding);
        if(holding){
            if(holding.name == "GUN_0"){
                animator.runtimeAnimatorController = GUN_0Anim as RuntimeAnimatorController;
            }else if(holding.name == "FIREGUN"){
                
                animator.runtimeAnimatorController = FireGunAnim as RuntimeAnimatorController;
            }
        }
    }
}
