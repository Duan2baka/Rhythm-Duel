using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackCard : MonoBehaviour, Card{
    public int cost = 2;
    public Sprite img;
    public void Cast(ref int currentMana){
        currentMana -= cost;
        GameObject.Find("Enemy").GetComponent<HealthController>().takeDamage(10);
        return;
    }
    public int getCost(){
        return cost;
    }
    public Sprite getSprite(){
        return img;
    }
}
