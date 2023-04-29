using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Recovery10Card : MonoBehaviour, Card{
    public int cost = 5;
    public Sprite img;
    private HealthController playerHealth;
    public void init(){
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<HealthController>();
    }
    public void Cast(ref int currentMana){
        playerHealth.health(10);
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
