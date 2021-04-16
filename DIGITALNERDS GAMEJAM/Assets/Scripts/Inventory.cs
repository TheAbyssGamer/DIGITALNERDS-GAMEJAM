using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static GameObject holding;

    void Update(){
        Debug.Log("Acu tin "+holding);
    }
}
