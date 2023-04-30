using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class emptyCard : MonoBehaviour, Card{
    public int cost = 3;
    public Sprite img;
    public void Cast(ref int currentMana){
        currentMana -= cost;
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
    public void init(){

    }
    public string getChipName(){
        return "empty Card";
    }
    public string getDescription(){
        return "no effect";
    }
}
