using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour{
    public Slider slider;
    public int maxHP = 100;
    private int HP;

    void Start(){
        HP = maxHP;
        slider.value = HP;
    }

    public int getHP(){
        return HP;
    }
    public void takeDamage(int k){
        HP = HP - k;
        slider.value = HP;
    }
    void checkHP(){

    }
    void Update(){
        slider.value = HP;
        checkHP();
    }
}
