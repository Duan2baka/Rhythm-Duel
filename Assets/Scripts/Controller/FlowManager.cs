using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour{
    public bool exhausted;
    public bool punish;
    private ManaManager manaMng; 
    private Enemy enemy;
    private FloorController floorController;
    private int T;
    void Start(){
        T = 0;
        exhausted = false;
        manaMng = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManaManager>();
        floorController = GameObject.FindGameObjectWithTag("GameController").GetComponent<FloorController>();
        punish = false;
        enemy = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject.GetComponent<Enemy>();
    }

    void eventManager(){

    }
    void manaManager(){
        if(!punish) manaMng.increase(1);
        punish = false;
    }
    void floorManager(){
        floorController.refresh();
    }
    void LateUpdate(){
        if(exhausted){
            T ++;
            enemy.takeAction();
            eventManager();
            manaManager();
            floorManager();
            exhausted = false;
        }
    }
    public int getTimeStamp(){
        return T;
    }
}
