using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour{
    private GameObject[,,] floor;
    private GameObject player;
    private GameObject enemy;
    void Start(){
        enemy = GameObject.FindGameObjectWithTag("MainEnemy").transform.parent.gameObject;
        player = GameObject.FindGameObjectWithTag("Player");

        floor = new GameObject[2,4,4];
        for(int i = 0; i <= 1; i ++)
            for(int j = 1; j <= 3; j ++)
                for(int k = 1; k <= 3; k ++){
                    floor[i, j, k] = GameObject.Find("Floor "+ i + j + k);
                    floor[i, j, k].GetComponent<FloorStatus>().setPosition(i == 1, j, k);
                    // Debug.Log(GameObject.Find("Floor "+ i + j + k) == null);
                }
    }

    public void refresh(){
        for(int i = 0; i <= 1; i ++)
            for(int j = 1; j <= 3; j ++)
                for(int k = 1; k <= 3; k ++)
                    floor[i, j, k].GetComponent<FloorStatus>().refresh();
    }

    void Update(){
        
    }
    public bool isAccessable(int X, int Y, bool isPlayer){
        /*if(isPlayer)*/ if(X < 1 || X > 3 || Y < 1 || Y > 3) return false;
        if(!get(X, Y, isPlayer).GetComponent<FloorStatus>().getStatus()) return false;
        return true;
    }
    
    public GameObject get(int X, int Y, bool isPlayer){
        // Debug.Log(""+X+Y+isPlayer);
        return floor[(isPlayer ? 1 : 0), X, Y];
    }    
    public Vector3 getPosition(int X, int Y, bool isPlayer){
        return floor[(isPlayer ? 1 : 0), X, Y].transform.Find("Position").position;
    }
    public GameObject FindObjectOn(int X, int Y, bool isPlayer){
        if(isPlayer){
            if(player.GetComponent<PlayerMovement>().getX() == X && player.GetComponent<PlayerMovement>().getY() == Y) return player;
            //return (player.GetComponent<PlayerMovement>().getX() == X && player.GetComponent<PlayerMovement>().getY() == Y)
            //? player : null;
        }
        else{
            if(enemy.GetComponent<EnemyMovement>().getX() == X && enemy.GetComponent<EnemyMovement>().getY() == Y) return enemy;

            //return (enemy.GetComponent<EnemyMovement>().getX() == X && player.GetComponent<EnemyMovement>().getY() == Y)
            //? enemy : null;
        }


        /// need check traps
        return null;
    }
}
