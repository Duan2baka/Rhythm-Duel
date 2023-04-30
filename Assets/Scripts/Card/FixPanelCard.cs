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
                for(int k = 0; k <= 1; k ++)
                    floorController.get(i, j, k == 1).GetComponent<FloorStatus>().fix(100);
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
