using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    // Start is called before the first frame update
    Slider slider;
    public int maxHP = 100;
    private int HP;

    void Start(){
        HP = maxHP;
        slider = GameObject.Find("PlayerHPSlider").GetComponent<Slider>();
        slider.value = HP;
    }

    public int getHP(){
        return HP;
    }
    public void damage(int k){
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
