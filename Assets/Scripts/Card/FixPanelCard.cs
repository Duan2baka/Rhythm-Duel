using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixPanelCard : MonoBehaviour, Card{
    public int cost = 9;
    public Sprite img;
    private FloorController floorController;
    
    public void init(){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    public void Cast(ref int currentMana){
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++)
                floorController.get(i, j, true).GetComponent<FloorStatus>().fix(100);
        currentMana -= cost;
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
}
