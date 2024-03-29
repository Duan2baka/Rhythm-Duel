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
        playerHealth.heal(10);
        currentMana -= cost;
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
    public string getChipName(){
        return "Recovery 10 Card";
    }
    public string getDescription(){
        return "Get 10 hP.";
    }
}
