using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCard : MonoBehaviour, Card{
    public int cost = 2;
    public Sprite img;
    public void Cast(ref int currentMana){
        currentMana -= cost;
        GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<HealthController>().takeDamage(10);
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
        return "Attack Card";
    }
    public string getDescription(){
        return "A attack card";
    }
}
