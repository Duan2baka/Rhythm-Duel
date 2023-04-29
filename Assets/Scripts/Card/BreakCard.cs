using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakCard : MonoBehaviour, Card{
    public int cost = 5;
    public Sprite img;
    private PlayerMovement playerMovement;
    private EnemyMovement enemyMovement;
    private FloorController floorController;
    
    public void init(){
        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        enemyMovement = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyMovement>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
    }
    public void Cast(ref int currentMana){
        int x = playerMovement.getX(), y = playerMovement.getY();
        int ex = enemyMovement.getX(), ey = enemyMovement.getY();
        if(y < 3)
            floorController.get(x, y+1, true).GetComponent<FloorStatus>().takeDamage(100);
        else
            floorController.get(x, 1, false).GetComponent<FloorStatus>().takeDamage(100);
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