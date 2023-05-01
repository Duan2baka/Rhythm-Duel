using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour{
    GameObject player;
    public void init(GameObject Player){
        player = Player;
    }
    void Update(){
        transform.position = player.transform.position - new Vector3(
                    player.GetComponent<BoxCollider2D>().size.x / 2f,
                    -player.GetComponent<BoxCollider2D>().size.y / 2f, 0f);
    }
}
