using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour{
    private int startMana = 0;
    public int maxMana = 10;
    public int currentMana;
    void Start(){
        currentMana = startMana;
    }
    public void increase(int value){
        currentMana += value;
        if(currentMana > maxMana) currentMana = maxMana;
    }
}
