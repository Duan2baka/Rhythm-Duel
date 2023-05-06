using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicCircleController : MonoBehaviour{
    GameObject enemy;
    public void init(GameObject Enemy){
        enemy = Enemy;
    }
    void Update(){
        transform.position = enemy.transform.position - new Vector3(
                    0f, - enemy.GetComponent<BoxCollider2D>().size.y / 2f, 0f);
    }
}
