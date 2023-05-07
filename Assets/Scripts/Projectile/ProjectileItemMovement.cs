using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileItemMovement : MonoBehaviour, Movement{
    public int getX(){
        return gameObject.GetComponent<ProjectileItem>().currentX;
    }
    public int getY(){
        return gameObject.GetComponent<ProjectileItem>().currentY;
    }
    public bool getSide(){
        return gameObject.GetComponent<ProjectileItem>().currentSide;
    }
}
