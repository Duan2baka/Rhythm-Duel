using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStatus : MonoBehaviour{
    private bool status;
    private int health;
    private int maxHealth = 5;
    private int fixProcess;
    private int fixNeed = 10;
    void Start(){
        fixProcess = 0;
        health = maxHealth;
        status = true;
    }
    public bool getStatus(){
        return status;
    }
    public int getHealth(){
        return health;
    }
    public void destroy(){
        fixProcess = 0;
        health = 0;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        status = false;
    }
    public void fix(int value){
        if(status) return;
        fixProcess += value;
        if(fixProcess >= fixNeed){
            status = true;
            health = maxHealth;
            gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    public void takeDamage(int value){
        health -= value;
        if(health <= 0) destroy();
    }
}
