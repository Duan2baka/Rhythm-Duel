using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour{
    public GameObject emptyCard;
    void Start(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Card draw(){
        return emptyCard.GetComponent<Card>();
    }
}
