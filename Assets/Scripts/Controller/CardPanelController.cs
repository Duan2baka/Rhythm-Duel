using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPanelController : MonoBehaviour{
    Card[] cards;
    CardSlotController[] controllers;
    DeckController deckController; 
    // ManaManager manaManager;
    bool initialized = false;
    void Start(){
        initialized = false;
        deckController = GameObject.FindGameObjectWithTag("GameController").GetComponent<DeckController>();
        // manaManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManaManager>();
        cards = new Card[4];
        controllers = new CardSlotController[4];
        for(int i = 0; i < 4; i ++){
            controllers[i] = transform.Find("CardSlot" + (i + 1)).GetComponent<CardSlotController>();
            switch(i){
                case 0: controllers[i].key = "L"; break;
                case 1: controllers[i].key = "R"; break;
                case 2: controllers[i].key = "Q"; break;
                case 3: controllers[i].key = "E"; break;
            }
        }
    }
    public bool playCard(int index, ref int currentMana){
        if(cards[index].getCost() > currentMana) return false;
        cards[index].Cast(ref currentMana);
        deckController.discard(cards[index]);
        // Debug.Log("discard");
        cards[index] = deckController.draw();
        controllers[index].set(cards[index].getSprite(), cards[index].getCost());
        return true;
    }
    public void reDraw(){
        for(int i = 0; i < 4; i ++){
            deckController.discard(cards[i]);
            cards[i] = deckController.draw();
            controllers[i].set(cards[i].getSprite(), cards[i].getCost());
        }
    }
    public void shuffleHand(){
        cards.Shuffle();
        for(int i = 0; i < 4; i ++)
            controllers[i].set(cards[i].getSprite(), cards[i].getCost());
    }
    void Update(){
        if(!initialized) init();
    }
    void init(){
        initialized = true;
        for(int i = 0; i < 4; i ++){
            cards[i] = deckController.draw();
            if(cards[i] != null){
                controllers[i].set_activite(true);
                controllers[i].set(cards[i].getSprite(), cards[i].getCost());
            }
        }
    }
}
