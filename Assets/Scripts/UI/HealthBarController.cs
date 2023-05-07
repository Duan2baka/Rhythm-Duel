using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarController : MonoBehaviour{
    public Image healthPointImage;
    public Image healthPointEffect;
    public HealthController hp;
    private float hurtSpeed = 0.003f;

    private void Update(){
        healthPointImage.fillAmount = (1.0f * hp.HP) / (1.0f * hp.maxHP);
        if(healthPointEffect.fillAmount >= healthPointImage.fillAmount){
            healthPointEffect.fillAmount -= hurtSpeed;
        }
        else{
            healthPointEffect.fillAmount = healthPointImage.fillAmount;
        }
    }

}