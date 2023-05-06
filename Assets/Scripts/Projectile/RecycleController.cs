using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RecycleController : MonoBehaviour{
    GameObject player;
    public void init(GameObject Player){
        player = Player;
    }
    void Update(){
        transform.position = player.transform.position - new Vector3(
                    0f, - player.GetComponent<BoxCollider2D>().size.y, 0f);
    }
}
