using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EscapeCard : MonoBehaviour, Card{
    public int cost = 5;
    public Sprite img;
    private PlayerMovement playerMovement;
    private FloorController floorController;
    private int cnt, x, y;
    public void init(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }

    public void Cast(ref int currentMana){
        x = playerMovement.getX();
        y = playerMovement.getY();
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++)
                if(floorController.isAccessable(i, j, true)) cnt ++;
        cnt --;
        int rnd = Random.Range(1, cnt);
        cnt = 0;
        for(int i = 1; i <= 3; i ++)
            for(int j = 1; j <= 3; j ++){
                if(!floorController.isAccessable(i, j, true)) continue;
                if(i == x && j == y) continue;
                cnt ++;
                if(cnt == rnd) playerMovement.MoveTo(i, j);
            }
        currentMana -= cost;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
    public string getChipName(){
        return "Escape Card";
    }
    public string getDescription(){
        return "Ramdomly move to another valid floor(if exists)";
    }
}
