using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorController : MonoBehaviour{
    private GameObject[,,] floor;
    void Start(){
        floor = new GameObject[2,4,4];
        for(int i = 0; i <= 1; i ++)
            for(int j = 1; j <= 3; j ++)
                for(int k = 1; k <= 3; k ++){
                    floor[i, j, k] = GameObject.Find("Floor "+ i + j + k);
                    // Debug.Log(GameObject.Find("Floor "+ i + j + k) == null);
                }
    }

    void Update(){
        
    }
    public bool isAccessable(int X, int Y, bool isPlayer){
        if(isPlayer){
            if(X < 1 || X > 3 || Y < 1 || Y > 3) return false;
            return true;
        }
        return true;
    }
    
    public GameObject get(int X, int Y, bool isPlayer){
        // Debug.Log(""+X+Y+isPlayer);
        return floor[(isPlayer ? 1 : 0), X, Y];
    }
}
