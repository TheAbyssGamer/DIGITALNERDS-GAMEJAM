using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portals : MonoBehaviour
{
    public GameObject destination;

    void OnTriggerEnter2D(Collider2D colider){
        colider.transform.position = destination.transform.position;
    }
}
