using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedrawCard : MonoBehaviour, Card{
    public int cost = 4;
    public Sprite img;
    private CardPanelController cardPanelController;
    public void init(){
        cardPanelController = GameObject.FindGameObjectWithTag("CardPanel").GetComponent<CardPanelController>();
    }
    public void Cast(ref int currentMana){
        cardPanelController.reDraw();
        currentMana -= cost;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
    public string getChipName(){
        return "Redraw Card";
    }
    public string getDescription(){
        return "Discard all your chips and draw new ones.";
    }
}
