using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthController : MonoBehaviour{
    public int maxHP = 100;
    public int HP;
    private AudioSource aud;
    void Start(){
        HP = maxHP;
        aud = gameObject.GetComponent<AudioSource>();
    }
    void init(){
        HP = maxHP;
        aud = gameObject.GetComponent<AudioSource>();
    }
    public int getHP(){
        return HP;
    }
    public void takeDamage(int k){
        HP = HP - k;
        if(aud) aud.Play();
    }
    public void heal(int k){
        HP = HP + k;
        if(HP > maxHP) HP = maxHP;
    }
}
