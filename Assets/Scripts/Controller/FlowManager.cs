using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowManager : MonoBehaviour{
    public bool exhausted;
    public bool punish;
    private ManaManager manaMng; 
    void Start(){
        exhausted = false;
        manaMng = GameObject.FindGameObjectWithTag("GameController").GetComponent<ManaManager>();
        punish = false;
    }

    void trapManager(){

    }
    void manaManager(){
        if(!punish) manaMng.increase(1);
        punish = false;
    }
    void enemyManager(){

    }
    void LateUpdate(){
        if(exhausted){
            enemyManager();
            trapManager();
            manaManager();
            exhausted = false;
        }
    }
}
