using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightMovement : MonoBehaviour, Movement{
    public int getX(){
        return gameObject.GetComponent<KnightController>().currentX;
    }
    public int getY(){
        return gameObject.GetComponent<KnightController>().currentY;
    }
    public bool getSide(){
        return gameObject.GetComponent<KnightController>().currentSide;
    }
}
