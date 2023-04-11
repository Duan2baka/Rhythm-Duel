using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeckController : MonoBehaviour{
    public GameObject emptyCard;
    void Start(){
        
    }
    public Card peak(){
        /// to be implemented
        return emptyCard.GetComponent<Card>();
    }
    public Card draw(){
        /// to be implemented
        return emptyCard.GetComponent<Card>();
    }
}
