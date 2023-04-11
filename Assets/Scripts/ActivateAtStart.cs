using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateAtStart : MonoBehaviour{
    void Start(){
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }
}
