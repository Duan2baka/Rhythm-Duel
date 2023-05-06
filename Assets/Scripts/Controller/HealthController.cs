using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour{
    public int maxHP = 100;
    private int HP;
    void Start(){
        HP = maxHP;
    }
    void init(){
        HP = maxHP;
    }
    public int getHP(){
        return HP;
    }
    public void takeDamage(int k){
        HP = HP - k;
    }
    public void heal(int k){
        HP = HP + k;
        if(HP > maxHP) HP = maxHP;
    }
}
