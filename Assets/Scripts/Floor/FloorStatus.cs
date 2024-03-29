using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorStatus : MonoBehaviour{
    private FloorController floorController;
    private bool status;
    private int health;
    private int maxHealth = 5;
    private int fixProcess;
    private int fixNeed = 10;
    private int positionX, positionY;
    private GameObject tmpObject;
    private bool side;
    private int blocked;
    void Start(){
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        fixProcess = 0;
        health = maxHealth;
        status = true;
        blocked = 0;
    }
    public void changeBlocked(int val){
        blocked += val;
        if(blocked < 0) blocked = 0;
    }
    public bool getStatus(){
        return status;
    }
    public bool getBlocked(){
        return (blocked != 0);
    }
    public int getHealth(){
        return health;
    }
    public void setPosition(bool _side, int x, int y){
        positionX = x;
        positionY = y;
        side = _side;
    }
    public GameObject GetObject(){
        return floorController.FindObjectOn(positionX, positionY, side);
    }
    public GameObject GetObject_WithTag(string tag){
        return floorController.FindObjectOn_WithTag(positionX, positionY, side, tag);
    }
    public void refresh(){
        gameObject.GetComponent<SpriteRenderer>().enabled = status;
        fix(1);
        // takeDamage(1);
        // Debug.Log("" + status);
        if(status){
            //Debug.Log("" + ratio);
            float ratio = 1f * health / maxHealth;
            gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, ratio);
        }
    }
    public void destroy(){
        fixProcess = 0;
        health = 0;
        status = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = status;
    }
    public void fix(int value){
        if(status) return;
        fixProcess += value;
        if(fixProcess >= fixNeed){
            status = true;
            gameObject.GetComponent<SpriteRenderer>().enabled = status;
            health = maxHealth;
        }
    }
    public void takeDamage(int value, string target){
        if(!status) return;
        if(tmpObject = floorController.FindObjectOn_WithTag(positionX, positionY, side, target)){
            tmpObject.GetComponent<HealthController>().takeDamage(5);
            return;
        }
        health -= value;
        if(health <= 0) destroy();
    }
}
