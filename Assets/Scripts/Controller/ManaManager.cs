using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    private int startMana = 0;
    private int maxMana = 10;
    private int currentMana;
    private Slider manaBar;
    private Text text;
    void Start(){
        currentMana = startMana;
        manaBar = GameObject.Find("ManaSlider").GetComponent<Slider>();
        manaBar.maxValue = maxMana;
        manaBar.value = startMana;
        text = GameObject.Find("ManaSlider").transform.Find("Text").GetComponent<Text>();
    }

    void Update(){
        if(manaBar.value != currentMana) manaBar.value = currentMana;
        text.text = "Mana: " + currentMana;
    }
    public void increase(int value){
        currentMana += value;
        if(currentMana > maxMana) currentMana = maxMana;
    }
    
    public int getMana(){
        return currentMana;
    }
}
