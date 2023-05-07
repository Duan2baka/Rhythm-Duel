using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarController : MonoBehaviour{
    public Image healthPointImage;
    public Image healthPointEffect;
    public ManaManager hp;
    public Text text;
    private float hurtSpeed = 0.003f;

    private void Update(){
        healthPointImage.fillAmount = (1.0f * hp.currentMana) / (1.0f * hp.maxMana);
        if(healthPointEffect.fillAmount >= healthPointImage.fillAmount){
            healthPointEffect.fillAmount -= hurtSpeed;
        }
        else{
            healthPointEffect.fillAmount = healthPointImage.fillAmount;
        }
        text.text = "" + hp.currentMana;
    }
}
