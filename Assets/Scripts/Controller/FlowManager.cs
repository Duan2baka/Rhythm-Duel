using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour{
    public bool exhausted;
    public bool punish;
    private ManaManager manaMng; 
    private Enemy enemy;
    void Start(){
        exhausted = false;
        manaMng = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManaManager>();
        punish = false;
        enemy = GameObject.FindGameObjectWithTag("MainEnemy").GetComponent<Enemy>();
    }

    void eventManager(){

    }
    void manaManager(){
        if(!punish) manaMng.increase(1);
        punish = false;
    }
    void LateUpdate(){
        if(exhausted){
            enemy.takeAction();
            eventManager();
            manaManager();
            exhausted = false;
        }
    }
}
