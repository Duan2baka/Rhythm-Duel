using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHealthController : MonoBehaviour{
    void Update(){
        if(gameObject.GetComponent<HealthController>().getHP() <= 0)
            Destroy(gameObject);
    }
}
