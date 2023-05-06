using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobsHealthController : MonoBehaviour{
    public int HP;
    public void takeDamage(int k){
        HP = HP - k;
        if(HP <= 0) Destroy(gameObject);
    }
}
