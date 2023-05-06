using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour{
    private GameObject[,,] floor;
    private GameObject player;
    private GameObject enemy;
    private GameObject[] tmpList;
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
        if(get(X, Y, isPlayer).GetComponent<FloorStatus>().getBlocked()) return false;
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
        if(player.GetComponent<Movement>().getX() == X &&player.GetComponent<Movement>().getY() == Y
        && player.GetComponent<Movement>().getSide() == isPlayer) return player;
        if(enemy.GetComponent<Movement>().getX() == X && enemy.GetComponent<Movement>().getY() == Y
        && enemy.GetComponent<Movement>().getSide() == isPlayer) return enemy;
        tmpList = GameObject.FindGameObjectsWithTag("Enemy");
        foreach(GameObject obj in tmpList){
            if(obj.GetComponent<Movement>().getX() == X && obj.GetComponent<Movement>().getY() == Y
            && obj.GetComponent<Movement>().getSide() == isPlayer) return obj;
        }
        /// need check traps
        return null;
    }
    public GameObject FindObjectOn_WithTag(int X, int Y, bool isPlayer, string tag){
        tmpList = GameObject.FindGameObjectsWithTag(tag);
        foreach(GameObject obj in tmpList){
            if(obj.GetComponent<Movement>() == null) continue;
            if(obj.GetComponent<Movement>().getX() == X && obj.GetComponent<Movement>().getY() == Y
            && obj.GetComponent<Movement>().getSide() == isPlayer) return obj;
        }
        return null;
    }
}
